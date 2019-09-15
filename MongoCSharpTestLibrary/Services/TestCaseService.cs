using NextGenTestLibrary.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NextGenTestLibrary.Utilities;
using MongoDB.Bson;
using MongoTestDatabaseLibrary.DAL;
using System.Diagnostics;
using NextGenTestLibrary.Attributes;
using NextGenTestLibrary.Enums;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using MongoDB.Driver;
using NextGenTestLibrary.Loggers;
using MongoTestDatabaseLibrary.Models;
using static NUnit.Framework.TestContext;
using NUnit.Framework.Internal;
using TestResult = NextGenTestLibrary.Enums.TestResult;

namespace NextGenTestLibrary.Services
{
    public partial class TestCaseService : ITestCaseService
    {
        private readonly MongoRepository mongoRepository;
        private readonly DataFillService dataFillService;
        private readonly ITestModuleService testModuleService;
        private readonly ITestClassService testClassService;
        private readonly TestDataService testDataService;
        private readonly ITestResultService testResultService;
        
        //static variables
        private static string moduleName;
        private static string className;
        private static string testCaseName;
        private static Stopwatch watch;
        private static string timeElapsed;

        /// <summary>
        /// Object is used for Re execution of testcase
        /// </summary>
        public static readonly object lockReTestExecutionObj = new object();

        public TestCaseService()
        {
            mongoRepository = new MongoRepository();
            dataFillService = new DataFillService();
            testModuleService = new TestModuleService();
            testClassService = new TestClassService();
            testDataService = new TestDataService();
            testResultService = new TestResultService();
        }
        /// <summary>
        /// Get list of testcases
        /// </summary>
        /// <param name="sender"></param>
        /// <returns></returns>
        public IList<Tuple<string,string,int>> GetTestCases(Assembly sender)
        {
            List<ObjectId> modules;
            List<ObjectId> testClasses;
            IList<Tuple<string, string, int>> testCasesByDriven;
            dataFillService.RefreshMasterDatabase(sender);
            modules = testModuleService.GetExecutableModules();
            testClasses = testClassService.GetExecutableTestClasses(modules);
            testCasesByDriven = GetExecutableDataDrivenTestCases(testClasses);

            return testCasesByDriven;
        }
        /// <summary>
        /// Execute test cases
        /// </summary>
        /// <param name="className"></param>
        /// <param name="testCaseName"></param>
        /// <param name="testDataOrder"></param>
        public void ExecuteTestCase(string className, string testCaseName,int testDataOrder)
        {
            object[] endTestCaseParameterArray;

            //Get ModulName
            ObjectId moduleId = mongoRepository.GetTestClassRepository.GetByName(className).Module_id;
            moduleName = mongoRepository.GetTestModuleRepository.GetById(moduleId).ModuleType;

            Type myClass = Assembly.GetCallingAssembly().GetType(className);
            MethodInfo ExecuteMethod = myClass.GetMethod(testCaseName);
            MethodInfo EndOfTestCaseExecution = myClass.GetMethod("EndOfTestCaseExecution");
            TestBaseClassService myObj = (TestBaseClassService) Activator.CreateInstance(myClass);
            try
            {
                /* Initialize the className and testCaseName to static variable 
                  for access out side of the method */
                TestCaseService.className = className;
                TestCaseService.testCaseName = testCaseName;

                // Get Global TestData for the project
                testDataService.FillGlobalTestData(mongoRepository.GetTestProjectRepository.GetId(Initialize.ProjectName));
                // Get test data for the current testCase
                testDataService.FillTestData(mongoRepository.GetTestCaseRepository.GetId(testCaseName),testDataOrder);

                //Is dependent testcase is executed and if is null ? true : false
                bool isDependentTestCaseSuccess = true;

                if (Initialize.IsDependencyCheckEnabled)
                {
                    isDependentTestCaseSuccess = IsDependentTestCaseExecuted(testCaseName);
                }

                if (isDependentTestCaseSuccess)
                {
                    //Logging before testCase Execute
                    ContextLogger.LogBeforeTestCase(moduleName, className, testCaseName, testDataService.TestData);

                    //Execution Timein Logger Condition
                    if (Initialize.DisplayExecutionTimeInLogger)
                    {
                        watch = Stopwatch.StartNew();
                    }

                    ExecuteMethod.Invoke(myObj, null);
                    TestStatus result = TestExecutionContext.CurrentContext.CurrentResult.ResultState.Status;
                }
                else
                {
                    Assert.Ignore("TestCase is skipped:"+testCaseName+",due to parent testcase is failed/not executed");
                }

            }
            catch (TargetInvocationException exception) when (exception.InnerException is Exception)
            {
                ContextLogger.LogIfException
                    (moduleName, className, testCaseName, exception.Message);
                if (Initialize.RetryOnException)
                {
                    IList<Type> attributes = GetAvailableAttributes(ExecuteMethod);
                    bool isAvailable = attributes.Any(attribute => attribute.Name.Equals("RetryByAttribute"));
                    if (isAvailable)
                    {
                        if (Initialize.DisplayExecutionTimeInLogger)
                        {
                            watch.Stop();
                            timeElapsed = watch.ElapsedMilliseconds.ToString();
                            ContextLogger.LogAfterTestCaseFails
                                (moduleName, className, testCaseName, testDataService.TestData, timeElapsed, "Failed On Exception");
                        }

                        //Execute end of testcase execution if any object needs to be disposed
                        endTestCaseParameterArray = new object[] { moduleName, className, testCaseName, false };
                        EndOfTestCaseExecution.Invoke(myObj, endTestCaseParameterArray);

                        //Retry attribute object
                        RetryByAttribute retryAttribute = ExecuteMethod.GetRetryByAttribute();
                        lock (lockReTestExecutionObj)
                        {
                            //Re-Execution of Exception TestCase
                            ReExecutionOfTestCase
                                (moduleName, className, testCaseName, testDataOrder, retryAttribute._tryCount, myClass, myObj);
                        }

                    }
                    else
                    {
                        throw exception;
                    }
                }
                else
                {
                    throw exception;
                }

            }
            catch (TargetInvocationException exception) when (exception.InnerException is AssertionException)
            {
                AssertionException assertException = exception.InnerException as AssertionException;
              
                ContextLogger.LogIfException(moduleName, className, testCaseName, exception.Message);
                if (Initialize.RetryOnException)
                {
                    IList<Type> attributes = GetAvailableAttributes(ExecuteMethod);
                    bool isAvailable = attributes.Any(attribute => attribute.Name.Equals("RetryByAttribute"));
                    if (isAvailable)
                    {
                        if (Initialize.DisplayExecutionTimeInLogger)
                        {
                            watch.Stop();
                            timeElapsed = watch.ElapsedMilliseconds.ToString();
                            ContextLogger.LogAfterTestCaseFails
                                (moduleName, className, testCaseName, testDataService.TestData, timeElapsed,assertException.ResultState.Status);
                        }
                        //Execute end of testcase execution if any object needs to be disposed
                        endTestCaseParameterArray = new object[] { moduleName, className, testCaseName, false };
                        EndOfTestCaseExecution.Invoke(myObj, endTestCaseParameterArray);

                        //Retry attribute object
                        RetryByAttribute retryAttribute = ExecuteMethod.GetRetryByAttribute();
                        lock (lockReTestExecutionObj)
                        {
                            //Re-Execution of Exception TestCase
                            ReExecutionOfTestCase
                                (moduleName, className, testCaseName, testDataOrder, retryAttribute._tryCount, myClass, myObj);
                        }

                    }
                    else
                    {
                        throw exception;
                    }
                }
                else
                {
                    throw exception;
                }

            }

        }
        /// <summary>
        /// TestCase results
        /// </summary>
        /// <param name="testStatus"></param>
        public void TestCaseResults(TestContext testContext)
        {
            TestStatus testStatus = testContext.Result.Outcome.Status;
            ResultAdapter resultAdapter = testContext.Result;

            object[] endTestCaseParameterArray;
            List<string> testResults = new List<string>();
            TestResult testResult = TestResult.Inconclusive;

            Type myClass = Assembly.GetCallingAssembly().GetType(className);
            MethodInfo EndOfTestCaseExecution = myClass.GetMethod("EndOfTestCaseExecution");
            TestBaseClassService myObj = (TestBaseClassService)Activator.CreateInstance(myClass);
            try
            {
                if (testStatus == TestStatus.Passed)
                {
                    if (Initialize.DisplayExecutionTimeInLogger)
                    {
                        watch.Stop();
                        timeElapsed = watch.ElapsedMilliseconds.ToString();
                    }

                    //Logging testcase details after pass
                    ContextLogger.LogAfterTestCasePass(moduleName, className, testCaseName, timeElapsed, testStatus.ToString());
                    endTestCaseParameterArray = new object[] { moduleName, className, testCaseName, true };
                    EndOfTestCaseExecution.Invoke(myObj, endTestCaseParameterArray);
                    testResult = TestResult.Passed;
                }

                else if (testStatus == TestStatus.Failed)
                {
                    //Logging if test case fails
                    if (Initialize.DisplayExecutionTimeInLogger)
                    {
                        watch.Stop();
                        timeElapsed = watch.ElapsedMilliseconds.ToString();
                    }

                    //Logging testCase details after fails
                    ContextLogger.LogAfterTestCaseFails(moduleName, className, testCaseName, testDataService.TestData, timeElapsed, testStatus.ToString());
                    endTestCaseParameterArray = new object[] { moduleName, className, testCaseName, false };
                    EndOfTestCaseExecution.Invoke(myObj, endTestCaseParameterArray);
                    testResult = TestResult.Failed;
                    testResults.Add(resultAdapter.Message);
                    testResults.Add(resultAdapter.StackTrace);

                }
                else if (testStatus == TestStatus.Skipped)
                {
                    //Logging if test case fails
                    if (Initialize.DisplayExecutionTimeInLogger)
                    {
                        watch.Stop();
                        timeElapsed = watch.ElapsedMilliseconds.ToString();
                    }

                    //Logging testCase details after fails
                    ContextLogger.LogAfterTestCaseSkipped(moduleName, className, testCaseName,timeElapsed, testStatus.ToString());
                    endTestCaseParameterArray = new object[] { moduleName, className, testCaseName, false };
                    EndOfTestCaseExecution.Invoke(myObj, endTestCaseParameterArray);
                    testResult = TestResult.Skipped;
                    testResults.Add(resultAdapter.Message);

                }
                else if (testStatus == TestStatus.Inconclusive)
                {
                    if (Initialize.DisplayExecutionTimeInLogger)
                    {
                        watch.Stop();
                        timeElapsed = watch.ElapsedMilliseconds.ToString();
                    }

                    //Logging testCase details after fails
                    ContextLogger.LogAfterTestCaseInConclusive(moduleName, className, testCaseName, timeElapsed, testStatus.ToString());
                    endTestCaseParameterArray = new object[] { moduleName, className, testCaseName, false };
                    EndOfTestCaseExecution.Invoke(myObj, endTestCaseParameterArray);
                    testResult = TestResult.Inconclusive;
                }
            }
            catch (IgnoreException ex)
            {
                //Logging testCase details after fails
                ContextLogger.LogAfterTestCaseSkipped(moduleName, className, testCaseName, timeElapsed,"Skipped"+ex.Message);
                endTestCaseParameterArray = new object[] { moduleName, className, testCaseName, false };
                EndOfTestCaseExecution.Invoke(myObj, endTestCaseParameterArray);
                testResult = TestResult.Skipped;
            }
            catch (Exception ex)
            {
                ContextLogger.LogIfException(moduleName, className, testCaseName, ex.Message);
                endTestCaseParameterArray = new object[] { moduleName, className, testCaseName, false };
                EndOfTestCaseExecution.Invoke(myObj, endTestCaseParameterArray);
                testResult = TestResult.Failed;
                if (testResults.Count == 0)
                    testResults.Add("Test is executed with:" + testResult.ToString() + "status");

                testResultService.InsertTestResults(testCaseName, testResult, testResults.ToArray());
            }
            finally
            {
                if (testResults.Count == 0)
                {
                    testResults.Add("Test is executed with:" + testResult.ToString()+ "status");
                }

                testResultService.InsertTestResults(testCaseName, testResult, testResults.ToArray());
            }
            
        }
        /// <summary>
        /// Refres testcases
        /// </summary>
        /// <param name="testCases"></param>
        /// <param name="sender"></param>
        public void RefreshTestCases(object testCases,Assembly sender)
        {
            IList<TestCaseModel> testCaseList = (IList<TestCaseModel>) testCases;

            List<string> testCaseNames = testCaseList.Select(a => a.TestCaseName).ToList();

            var testRepository = mongoRepository.GetTestCaseRepository;

            Dictionary<ObjectId, string> activeTestCases = testRepository.GetAll()
                                                                .Where(a => a.IsActive == true)
                                                                .ToDictionary(a => a._id, a => a.TestCaseName);

            Dictionary<ObjectId, string> inactiveTestCases = testRepository.GetAll()
                                                                            .Where(a => a.IsActive == false)
                                                                            .ToDictionary(a => a._id, a => a.TestCaseName);

            //Inactivate testcases which are not executable
            List<string> nonExecutableTestCases = testCaseList.Where(a => a.IsActive == false)
                                                                .Select(a => a.TestCaseName).ToList();

            List<ObjectId> inActivateTestCases = activeTestCases.Where(a => nonExecutableTestCases.Contains(a.Value))
                                                                              .Select(a => a.Key).ToList();
            if (inActivateTestCases.Count != 0)
            {
                InActivateTestCases(inActivateTestCases);
            }
            

            //Add below testcases

            List<TestCaseModel> newTestCases = testCaseList.Where(a => !activeTestCases.Values.Contains(a.TestCaseName) &&
                                                   !inactiveTestCases.Values.Contains(a.TestCaseName))
                                                   .ToList();
            if (newTestCases.Count != 0)
            {
                AddTestCases(newTestCases);
            }
            

            //Activate below testcases
            List<string> executableTestCases = testCaseList.Where(a => a.IsActive == true)
                                                            .Select(a => a.TestCaseName).ToList();
            List<ObjectId> activateTestCases = inactiveTestCases.Where(a => executableTestCases.Contains(a.Value))
                                                                .Select(a => a.Key).ToList();
            if (activateTestCases.Count != 0)
            {
                ActivateTestCases(activateTestCases);
            }

            //Update testcases
            if (testCaseList.Count != 0)
            {
                UpdateTestCases(testRepository, testCaseList);
            }
            
        }
        /// <summary>
        /// Get testcase list
        /// </summary>
        /// <param name="testClasses"></param>
        /// <param name="sender"></param>
        /// <returns></returns>
        public dynamic GetTestCaseList(List<string> testClasses,Assembly sender)
        {
            IList<TestCaseModel> testCaseList = new List<TestCaseModel>();

            foreach (string testClass in testClasses)
            {
                //string moduleType = TestModuleService.GetModuleType(testClass, sender);
                //ObjectId objectId = mongoRepository.GetTestModuleRepository.GetId(moduleType);
                ObjectId objectId = mongoRepository.GetTestClassRepository.GetId(testClass);
                //Get test methods
                Type type = sender.GetType(testClass);
                IEnumerable<MethodInfo> methods = type.GetMethods()
                                                    .Where(t => t.IsDefined(typeof(TestInfoAttribute), false)
                                                          && t.IsDefined(typeof(ExecuteAttribute), false));
                foreach (MethodInfo method in methods)
                {
                    IList<Type> attributes = GetAvailableAttributes(method);
                    TestInfoAttribute testAttrInfo = null;
                    TestOrderAttribute orderAttrInfo = null;
                    DependentAttribute dependentAttribute = null;

                    bool isExecute = false;
                    if (attributes.Count != 0)
                    {
                        foreach (Type attribute in attributes)
                        {
                            if (attribute.Name.Equals("ExecuteAttribute") ||
                                attribute.Name.Equals("Execute"))
                            {
                                ExecuteAttribute executeTestCaseAttribute = method.GetExecuteAttribute();
                                isExecute = executeTestCaseAttribute.ExecuteType == ExecuteType.Execute ? true : false;                 
                            }
                            else if (attribute.Name.Equals("TestInfoAttribute") ||
                                     attribute.Name.Equals("TestInfo"))
                            {
                                testAttrInfo = method.GetTestInfoAttribute();
                                    
                            }
                            else if (attribute.Name.Equals("TestOrderAttribute") ||
                                     attribute.Name.Equals("TestOrder"))
                            {
                                orderAttrInfo = method.GetTestOrderAttribute();
                            }
                            else if (attribute.Name.Equals("DependentAttribute") ||
                                     attribute.Name.Equals("Dependent"))
                            {
                                dependentAttribute = method.GetDependentAttribute();
                            }
                        }

                        testCaseList.Add(new TestCaseModel()
                        {
                            TestClass_id = objectId,
                            TestCaseName = method.Name,
                            TestCaseDescription = method.Name,
                            TestCaseType = testAttrInfo.TestCaseType.ToString(),
                            //CreatedDTM = DateTime.UtcNow,
                            TestCasePriority = testAttrInfo.Priority.ToString(),
                            TestCategory = testAttrInfo.TestCategory.ToString(),
                            Version = testAttrInfo.Version.ToString(),
                            DependentTestCaseName = dependentAttribute?.TestMethodName,
                            Order = orderAttrInfo.Order,
                            IsActive = isExecute
                         
                        });
                    }

                }

            }

            return testCaseList;
        }

    }
}
