using MongoDB.Bson;
using MongoDB.Driver;
using MongoTestDatabaseLibrary.IRepository;
using MongoTestDatabaseLibrary.Models;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;

namespace MongoTestDatabaseLibrary.Repository
{
    internal class TestDataRepository : ITestDataRepository<TestDataModel>
    {
        private IMongoCollection<TestDataModel> db;
        private ObjectId ObjectId { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        internal TestDataRepository(IMongoCollection<TestDataModel> context)
        {
            db = context;
        }
        /// <summary>
        /// Not allowed to create TestData
        /// </summary>
        /// <param name="obj"></param>
        public void Create(TestDataModel obj)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Update the of current TestData
        /// </summary>
        /// <param name="obj"></param>
        public UpdateResult Update(TestDataModel obj)
        {
            UpdateResult result = null;
            UpdateOptions updateOptions = new UpdateOptions
            {
                IsUpsert = true
            };

            FilterDefinition<TestDataModel> filter = Builders<TestDataModel>.Filter.Eq("_id", obj._id);
            UpdateDefinition<TestDataModel> update;
            PropertyInfo[] properties = typeof(ExpandoObject).GetProperties();
            foreach (PropertyInfo property in properties)
            {
                if (property.GetValue(obj) != null && property.Name != "_id" && property.Name != "TestCase_Id")
                {
                    update = Builders<TestDataModel>.Update.Set(property.Name, property.GetValue(obj));
                    result = db.UpdateOne(filter, update, updateOptions);
                }
            }

            return result;
        }
        /// <summary>
        /// Deletion
        /// </summary>
        /// <param name="obj"></param>
        public DeleteResult Delete(object obj)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Get Id
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public ObjectId GetId(string name)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Get by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public TestDataModel GetByName(string name)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Get testdata by id
        /// </summary>
        /// <param name="objectId"></param>
        /// <returns></returns>
        public TestDataModel GetById(ObjectId objectId)
        {
            FilterDefinition<TestDataModel> filter = Builders<TestDataModel>
                                                    .Filter.Eq("_id", ObjectId);
            return db.Find(filter).FirstOrDefault();
        }
        /// <summary>
        /// Get testdata by testcase id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IEnumerable<TestDataModel> GetTestDataByTestCaseId(ObjectId id)
        {
            var filter = Builders<TestDataModel>.Filter.Eq("TestCase_Id", id);

            return db.Find(filter).ToList();
        }
        /// <summary>
        /// Get list of testdata id's by testcase id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IEnumerable<ObjectId> GetIdsByTestCaseId(ObjectId id)
        {
            var filter = Builders<TestDataModel>.Filter.Eq("TestCase_Id", id);
            return db.Find(filter).ToList().Select(i => i._id);
        }
        /// <summary>
        /// Get testdata extra field's by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public BsonDocument GetTestDataExtraFieldsById(ObjectId id)
        {
            var filter = Builders<TestDataModel>.Filter.Eq("_id", id);
            return db.Find(filter).FirstOrDefault().ExtraElements;
        }
        /// <summary>
        /// Get test data documents from collection
        /// </summary>
        /// <returns></returns>
        public IQueryable<TestDataModel> GetAll()
        {
            return db.AsQueryable();
        }

        public void UpdateTestDataByFieldName()
        {
            throw new NotImplementedException();
        }
    }
}
