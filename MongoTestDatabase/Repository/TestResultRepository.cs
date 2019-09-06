using MongoDB.Bson;
using MongoDB.Driver;
using MongoTestDatabaseLibrary.IRepository;
using MongoTestDatabaseLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MongoTestDatabaseLibrary.Repository
{
    internal class TestResultRepository : ITestResultRepository<TestResultModel>
    {
        private IMongoCollection<TestResultModel> db;

        internal TestResultRepository(IMongoCollection<TestResultModel> context)
        {
            db = context;
        }
        /// <summary>
        /// Insertion test result 
        /// </summary>
        /// <param name="obj"></param>
        public void Create(TestResultModel obj)
        {
            if (obj.ExecutedDTM == null)
                obj.ExecutedDTM = DateTime.UtcNow;

            db.InsertOne(obj);
        }

        public DeleteResult Delete(object obj)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Get all record from test result model collection
        /// </summary>
        /// <returns></returns>
        public IQueryable<TestResultModel> GetAll()
        {
            return db.AsQueryable();
        }

        public TestResultModel GetById(ObjectId objectId)
        {
            throw new NotImplementedException();
        }

        public TestResultModel GetByName(string name)
        {
            throw new NotImplementedException();
        }
        
        public ObjectId GetId(string name)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Get test results by descending of execution datetime
        /// </summary>
        /// <returns></returns>
        public IEnumerable<object> GetTestResults()
        {
            FilterDefinition<TestResultModel> filterBuilder = new FilterDefinitionBuilder<TestResultModel>().Empty;
            FieldDefinition<TestResultModel> fieldDefinition = "ExecutedDTM";
            var sortBuilder = new SortDefinitionBuilder<TestResultModel>().Descending(fieldDefinition);

            return db.Find(filterBuilder).Sort(sortBuilder).ToList();
        }
        /// <summary>
        /// Get test results by testcase id
        /// </summary>
        /// <param name="testCaseId"></param>
        /// <returns></returns>
        public IEnumerable<TestResultModel> GetTestResultsByTestCaseId(ObjectId testCaseId)
        {
            FilterDefinition<TestResultModel> filterBuilder = new FilterDefinitionBuilder<TestResultModel>()
                                                                    .Eq(filter => filter.TestCase_Id,testCaseId);
            FieldDefinition<TestResultModel> fieldDefinition = "ExecutedDTM";
            var sortBuilder = new SortDefinitionBuilder<TestResultModel>().Descending(fieldDefinition);

           return db.Find(filterBuilder).Sort(sortBuilder).ToList();
        }
        /// <summary>
        /// Update test results
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public UpdateResult Update(TestResultModel obj)
        {
            throw new NotImplementedException();
        }
    }
}
