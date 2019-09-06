using NextGenTestLibrary.Attributes;
using NextGenTestLibrary.Enums;
using NextGenTestLibrary.IServices;
using NextGenTestLibrary.Utilities;
using MongoDB.Bson;
using MongoTestDatabaseLibrary.Models;
using MongoTestDatabaseLibrary.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using MongoDB.Driver;
using NextGenTestLibrary.Loggers;

namespace NextGenTestLibrary.Services
{
    public class TestClassService : ITestClassService
    {
        private readonly MongoRepository mongoRepository;
        
        public TestClassService()
        {
            mongoRepository = new MongoRepository();
        }

        /// <summary>
        /// Add test Classes
        /// </summary>
        /// <param name="testClasses"></param>
        public void AddTestClasses(dynamic testClasses)
        {
            IList<Tuple<ObjectId, string, int,bool>> classes = (List<Tuple<ObjectId, string, int,bool>>)testClasses;
            IEnumerable<TestClassModel> newTestClasses = classes.Select(tc => new TestClassModel()
                                                                        {
                                                                            Module_id = tc.Item1,
                                                                            TestClassName = tc.Item2,
                                                                            Order = tc.Item3,
                                                                            IsActive = tc.Item4
                                                                         });
            foreach (TestClassModel testClass in newTestClasses)
            {
                mongoRepository.GetTestClassRepository.Create(testClass);
            }
        }
        /// <summary>
        /// Get executable test classes
        /// </summary>
        /// <param name="moduleIds"></param>
        /// <returns></returns>
        public dynamic GetExecutableTestClasses(dynamic moduleIds)
        {
            List<ObjectId> testClasses = new List<ObjectId>();

            IList<ObjectId> testModules = (List<ObjectId>) moduleIds;

            foreach (ObjectId testModule in testModules)
            {
                IQueryable<TestClassModel> testClassesByModule = mongoRepository.GetTestClassRepository.GetAll()
                                                                    .Where(a => a.IsActive == true);
                foreach (var testClassByModule in testClassesByModule)
                {
                    if (testClassByModule.Module_id.Equals(testModule))
                    {
                        testClasses.Add(testClassByModule._id);
                    }
                }

            }

            return testClasses;
        }
        /// <summary>
        /// Get testClasses
        /// </summary>
        /// <param name="sender"></param>
        /// <returns></returns>
        public dynamic GetTestClasses(Assembly sender)
        {
            return sender.GetTypes()
                .Where(t => t.IsPublic && !t.IsAbstract && !t.IsSealed && t.IsClass)
                .Where(t => t.IsDefined(typeof(ModuleAttribute), false)
                            && t.IsDefined(typeof(ExecuteAttribute), false))
                .GetOrder();                
        }
        /// <summary>
        /// Refresh testclasses
        /// </summary>
        /// <param name="sender"></param>
        public void RefreshTestClasses(Assembly sender)
        {
            IList<Tuple<ObjectId, string, int,bool>> testClasses = GetTestClassesData(sender);

            int count = mongoRepository.GetTestClassRepository.GetAll().Count();
            
            //Add all test classes if count in collection is zero
            if (count == 0)
            {
                //Add new test classes
                AddTestClasses(testClasses);
            }
            else if (count !=0)
            {
                //Update the test classes
                IEnumerable<TestClassModel> updateTestClasses = testClasses.
                                                                Select(tc => new TestClassModel()
                                                                                    {
                                                                                        Module_id = tc.Item1,
                                                                                        TestClassName = tc.Item2,
                                                                                        Order = tc.Item3,
                                                                                        IsActive = tc.Item4,
                    
                                                                                     });
                try
                {
                    foreach (var testClass in updateTestClasses)
                    {
                        TestClassModel testclass = mongoRepository.GetTestClassRepository.GetByName(testClass.TestClassName);
                        if (testclass != null)
                        {
                            if (testClass.Module_id.Equals(testclass.Module_id) ||
                                  testClass.Order != testclass.Order ||
                                    testClass.IsActive != testclass.IsActive)
                            {
                                mongoRepository.GetTestClassRepository.GetId(testClass.TestClassName);
                                UpdateResult result = mongoRepository.GetTestClassRepository.Update(testClass);
                                if (result.IsAcknowledged)
                                {
                                    Logger.log.Debug("TestClass is updated Succesfully:" + testClass.TestClassName);
                                }
                                else
                                {
                                    throw new MongoException("TestClass is not updated:" + testClass.TestClassName);
                                }
                            }
                        }
                        else if (testclass == null)
                        {
                            mongoRepository.GetTestClassRepository.Create(testClass);
                            Logger.log.Debug("TestClass is created successfully:" + testClass.TestClassName);
                        }

                    }
                }
                catch (Exception ex)
                {
                    Logger.log.Error(ex.Message);
                }


            }
        }
        /// <summary>
        /// Get testclasses data
        /// </summary>
        /// <param name="sender"></param>
        /// <returns></returns>
        private IList<Tuple<ObjectId, string, int,bool>> GetTestClassesData(Assembly sender)
        {
            IList<Tuple<ObjectId, string, int,bool>> testClasses = new List<Tuple<ObjectId, string, int,bool>>();

            IDictionary<int, Type> orderedTestClasses = GetTestClasses(sender);
            foreach (KeyValuePair<int, Type> testClass in orderedTestClasses)
            {
                string type = TestModuleService.GetModuleType(testClass.Value.FullName, sender);
                ObjectId moduleId = mongoRepository.GetTestModuleRepository.GetId(type);
                bool isExecute = IsExecutable(testClass.Value);
                testClasses.Add(Tuple.Create(moduleId, testClass.Value.FullName, testClass.Key,isExecute));
            }

            return testClasses;
        }
        /// <summary>
        /// Is testClass executable
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private bool IsExecutable(Type type)
        {
            bool isExecute = false;
            ExecuteAttribute attribute = Attribute.GetCustomAttribute(type, typeof(ExecuteAttribute)) as ExecuteAttribute;
            if (attribute != null)
            {
                if(attribute.ExecuteType == ExecuteType.Execute)
                {
                    isExecute = true;
                }

                return isExecute;

            }
            else
            {
                return isExecute;
            }
        }


    }
}
