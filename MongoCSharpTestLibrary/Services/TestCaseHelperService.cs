using MongoDB.Bson;
using MongoDB.Driver;
using MongoTestDatabaseLibrary.DAL;
using MongoTestDatabaseLibrary.IRepository;
using MongoTestDatabaseLibrary.Models;
using NextGenTestLibrary.Attributes;
using NextGenTestLibrary.Enums;
using NextGenTestLibrary.Loggers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NextGenTestLibrary.Services
{
    public partial class TestCaseService
    {
        /// <summary>
        /// Executable data driven testcases
        /// </summary>
        /// <param name="testClassIds"></param>
        /// <returns></returns>
        private IList<Tuple<string, string, int>> GetExecutableDataDrivenTestCases(IList<ObjectId> testClassIds)
        {
            List<Tuple<string, string, int>> tuples = new List<Tuple<string, string, int>>();
            foreach (ObjectId testClass in testClassIds)
            {
                string testClassName = mongoRepository.GetTestClassRepository.GetById(testClass).TestClassName;
                IQueryable<TestCaseModel> testCasesByModule = mongoRepository.GetTestCaseRepository.GetAll()
                                                            .Where(a => a.TestClass_id == testClass && a.IsActive == true);
                foreach (TestCaseModel testCase in testCasesByModule)
                {
                    IQueryable<TestDataModel> testDataIds = mongoRepository.GetTestDataRepository.GetAll()
                                            .Where(tc => tc.TestCase_Id == testCase._id && tc.IsActive == true);
                    if (testDataIds.Count() != 0)
                    {
                        foreach (TestDataModel testDataId in testDataIds)
                        {
                            tuples.Add(Tuple.Create(testClassName, testCase.TestCaseName, testDataId.Order));
                        }
                    }
                    else
                    {
                        tuples.Add(Tuple.Create(testClassName, testCase.TestCaseName, 1));
                    }

                }
            }

            return tuples;

        }

        /// <summary>
        /// Inactivate testcases
        /// </summary>
        /// <param name="testCaseIds"></param>
        private void InActivateTestCases(List<ObjectId> testCaseIds)
        {
            List<TestCaseModel> testCases = mongoRepository.GetTestCaseRepository.GetAll().ToList();
            IEnumerable<TestCaseModel> selectTestCases = from testCase in testCases
                                                         join testCaseId in testCaseIds on testCase._id equals testCaseId
                                                         select testCase;
            try
            {
                foreach (TestCaseModel selectTestCase in selectTestCases)
                {
                    selectTestCase.IsActive = false;
                    UpdateResult result = mongoRepository.GetTestCaseRepository.Update(selectTestCase);
                    if (result.IsAcknowledged)
                    {
                        Logger.log.Debug("TestCase is Inactivated Successfully:" + selectTestCase.TestCaseName);
                    }
                    else
                    {
                        throw new MongoException("Inactivate of testCase failed:" + selectTestCase.TestCaseName);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.log.Error(ex.Message);
            }

        }

        /// <summary>
        /// Activate testcases
        /// </summary>
        /// <param name="testCaseIds"></param>
        private void ActivateTestCases(List<ObjectId> testCaseIds)
        {

            List<TestCaseModel> testCases = mongoRepository.GetTestCaseRepository.GetAll().ToList();
            IEnumerable<TestCaseModel> selectTestCases = from testCase in testCases
                                                         join testCaseId in testCaseIds on testCase._id equals testCaseId
                                                         select testCase;
            try
            {
                foreach (TestCaseModel selectTestCase in selectTestCases)
                {
                    selectTestCase.IsActive = true;
                    UpdateResult result = mongoRepository.GetTestCaseRepository.Update(selectTestCase);
                    if (result.IsAcknowledged)
                    {
                        Logger.log.Debug("TestCase is activated Successfully:" + selectTestCase.TestCaseName);
                    }
                    else
                    {
                        throw new MongoException("Activating of testCase failed:" + selectTestCase.TestCaseName);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.log.Error(ex.Message);
            }

        }

        /// <summary>
        /// Add testcases
        /// </summary>
        /// <param name="testCaseList"></param>
        private void AddTestCases(object testCaseList)
        {
            IList<TestCaseModel> testCases = (IList<TestCaseModel>)testCaseList;
            List<TestCaseModel> listOfTestCases = testCases.Select(a => new TestCaseModel()
            {
                TestClass_id = a.TestClass_id,
                TestCaseName = a.TestCaseName,
                TestCaseType = a.TestCaseType,
                TestCasePriority = a.TestCasePriority,
                TestCategory = a.TestCategory,
                Version = a.Version,
                DependentTestCaseName = a.DependentTestCaseName,
                Order = a.Order
            }).ToList();

            foreach (var testcase in listOfTestCases)
            {
                mongoRepository.GetTestCaseRepository.Create(testcase);
            }
        }

        /// <summary>
        /// Get available attributes on test method
        /// </summary>
        /// <param name="methodInfo"></param>
        /// <returns></returns>
        private IList<Type> GetAvailableAttributes(MethodInfo methodInfo)
        {
            IList<Type> attributeTypes = new List<Type>();

            IEnumerable<Attribute> attributes = CustomAttributeExtensions.GetCustomAttributes(methodInfo, false);
            if (attributes.Count() != 0)
            {
                foreach (Attribute attribute in attributes)
                {
                    Type attributeType = attribute.GetType();
                    if (attributeType == typeof(TestInfoAttribute))
                    {
                        attributeTypes.Add(attributeType);
                    }
                    else if (attributeType == typeof(ExecuteAttribute))
                    {
                        attributeTypes.Add(attributeType);
                    }
                    else if (attributeType == typeof(TestOrderAttribute))
                    {
                        attributeTypes.Add(attributeType);
                    }
                    else if (attributeType == typeof(DependentAttribute))
                    {
                        attributeTypes.Add(attributeType);
                    }
                }
            }

            return attributeTypes;
        }

        /// <summary>
        /// Is dependent testcase is executed
        /// </summary>
        /// <param name="testCaseName"></param>
        /// <returns></returns>
        private bool IsDependentTestCaseExecuted(string testCaseName)
        {
            bool isDependencyTestCaseExecuted = false;

            ITestCaseRepository<TestCaseModel> testCaseRepository = mongoRepository.GetTestCaseRepository;
            TestCaseModel testCaseModel = testCaseRepository.GetByName(testCaseName);
            if (testCaseModel.DependentTestCaseName != null)
            {
                ObjectId objectId = testCaseRepository.GetId(testCaseModel.DependentTestCaseName);
                if (objectId != null)
                {
                    TestResultModel testResultModel = mongoRepository.GetTestResultRepository
                                                                     .GetTestResultsByTestCaseId(objectId)
                                                                     .FirstOrDefault();
                    if (testResultModel.Result.Equals(TestResult.Passed.ToString()))
                    {
                        isDependencyTestCaseExecuted = true;
                    }

                }
            }
            else
            {
                isDependencyTestCaseExecuted = true;
            }

            return isDependencyTestCaseExecuted;
        }

        /// <summary>
        /// Update testcases
        /// </summary>
        /// <param name="testRepository"></param>
        /// <param name="testCaseList"></param>
        private void UpdateTestCases
            (ITestCaseRepository<TestCaseModel> testRepository, IList<TestCaseModel> testCaseList)
        {
            foreach (TestCaseModel testCaseModel in testCaseList)
            {
                TestCaseModel caseModel = testRepository.GetByName(testCaseModel.TestCaseName);
                if (caseModel != null)
                {
                    if (testCaseModel.TestCategory != caseModel.TestCategory ||
                        testCaseModel.TestCaseDescription != caseModel.TestCaseDescription ||
                        testCaseModel.TestCasePriority != caseModel.TestCasePriority ||
                        testCaseModel.DependentTestCaseName != caseModel.DependentTestCaseName ||
                        testCaseModel.Order != caseModel.Order ||
                        testCaseModel.IsActive != caseModel.IsActive)
                    {
                        testRepository.GetId(testCaseModel.TestCaseName);
                        UpdateResult result = testRepository.Update(testCaseModel);
                        if (result.IsAcknowledged)
                        {
                            Logger.log.Debug("TestCase is updataed Successfully:" + testCaseModel.TestCaseName);
                        }
                        else
                        {
                            throw new MongoException("Updating of testCase failed:" + testCaseModel.TestCaseName);
                        }
                    }
                }
            }
        }
    }
}
