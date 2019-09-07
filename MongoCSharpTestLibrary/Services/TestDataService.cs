using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson;
using MongoTestDatabaseLibrary.DAL;
using MongoTestDatabaseLibrary.Models;
using NextGenTestLibrary.IServices;
using NextGenTestLibrary.Loggers;

namespace NextGenTestLibrary.Services
{
    public class TestDataService : ITestDataService , IDisposable
    {
        private readonly MongoRepository mongoRepository;

        private static Dictionary<string,object> TestDatas { get; set; }
        private static Dictionary<string,object> GlobalTestDatas { get; set; }

        public Dictionary<string,object> TestData => TestDatas;
        public Dictionary<string, object> GlobalTestData => GlobalTestDatas;

        public TestDataService()
        {
            mongoRepository = new MongoRepository();
        }
        /// <summary>
        /// Fill test data
        /// </summary>
        /// <param name="testCaseID"></param>
        /// <param name="testDataOrder"></param>
        public void FillTestData(ObjectId testCaseID,int testDataOrder)
        {
            if (TestDatas == null)
            {
                TestDatas = GetTestData(testCaseID,testDataOrder);
            }
        }
        /// <summary>
        /// Fill global testdata
        /// </summary>
        /// <param name="projectId"></param>
        public void FillGlobalTestData(ObjectId projectId)
        {
            if(GlobalTestDatas == null)
            {
                GlobalTestDatas = GetGlobalTestData(projectId);
            }
        }
        /// <summary>
        /// Get field value by field name
        /// </summary>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        public object GetFieldValue(string fieldName)
        {
            object value;
            if (TestDatas.ContainsKey(fieldName))
            {
                TestDatas.TryGetValue(fieldName, out value);
                return value;
            }
            else
            {
                throw new KeyNotFoundException("Key Name is not found:"+fieldName);
            }
                
        }
        /// <summary>
        /// Modify existing field value
        /// </summary>
        /// <param name="fieldName"></param>
        /// <param name="fieldValue"></param>
        public void SetFieldValue(string fieldName,object fieldValue)
        {
            if (TestDatas.ContainsKey(fieldName))
            {
                TestDatas[fieldName] = fieldValue;
                
            }
            else
            {
                throw new KeyNotFoundException("Key Name is not found:" + fieldName);
            }

        }
        /// <summary>
        /// Get field name by value
        /// </summary>
        /// <param name="fieldValue"></param>
        /// <returns></returns>
        public string GetFieldName(object fieldValue)
        {
            if (TestDatas.ContainsValue(fieldValue))
            {
                return TestDatas.Where(v => v.Value.Equals(fieldValue)).Select(k => k.Key).ToString();
            }
            else
            {
                throw new Exception("Value is not found:" + fieldValue);
            }
        }
        /// <summary>
        /// Get global field name
        /// </summary>
        /// <param name="globalfieldValue"></param>
        /// <returns></returns>
        public string GetGlobalFieldName(object globalfieldValue)
        {
            if (GlobalTestDatas.ContainsValue(globalfieldValue))
            {
                return GlobalTestDatas.Where(v => v.Value.Equals(globalfieldValue)).Select(a => a.Key).ToString();
            }
            else
            {
                throw new Exception("Value is not found:"+globalfieldValue);
            }
            
        }
        /// <summary>
        /// Get global field value
        /// </summary>
        /// <param name="globalfieldName"></param>
        /// <returns></returns>
        public object GetGlobalFieldValue(string globalfieldName)
        {
            object value;
            if (GlobalTestDatas.ContainsKey(globalfieldName))
            {
                TestDatas.TryGetValue(globalfieldName, out value);
                return value;
            }
            else
            {
                throw new KeyNotFoundException("Key Name is not found:" + globalfieldName);
            }
        }
        /// <summary>
        /// Get test data
        /// </summary>
        /// <param name="testcaseID"></param>
        /// <param name="testDataOrder"></param>
        /// <returns></returns>
        public Dictionary<string, object> GetTestData(ObjectId testcaseID,int testDataOrder)
        {
            TestDataModel testDataFields = (from testData in mongoRepository.GetTestDataRepository.GetAll()
                                      where testData.TestCase_Id == testcaseID && testData.IsActive == true
                                      && testData.Order == testDataOrder
                                      select testData).FirstOrDefault();
           
            return testDataFields.ExtraElements.ToDictionary();
        }
        /// <summary>
        /// Get global test data
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        private Dictionary<string, object> GetGlobalTestData(ObjectId projectId)
        {
            var globalTestDataFields = (from globalTestData in mongoRepository.GetGlobalTestDataRepository.GetAll()
                                        where globalTestData.Project_id == projectId && globalTestData.IsActive == true
                                        select globalTestData).FirstOrDefault();

            return globalTestDataFields.ExtraElements.ToDictionary();
        }

        /// <summary>
        /// Implementation of IDisposable pattern
        /// </summary>
        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    mongoRepository.Dispose();
                }

            }

            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
