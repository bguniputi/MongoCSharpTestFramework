using MongoDB.Bson;
using MongoDB.Driver;
using MongoTestDatabaseLibrary.IRepository;
using MongoTestDatabaseLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace MongoTestDatabaseLibrary.Repository
{
    internal class TestCaseRepository : ITestCaseRepository<TestCaseModel>
    {
        private IMongoCollection<TestCaseModel> db;
        private ObjectId ObjectId { get; set; }
        public DeleteResult DeleteResult { get; private set; }

        internal TestCaseRepository(IMongoCollection<TestCaseModel> context)
        {
            db = context;
        }
        /// <summary>
        /// Creation of testcase
        /// </summary>
        /// <param name="obj"></param>
        public void Create(TestCaseModel obj)
        {
            if (obj.CreatedDTM == null)
                obj.CreatedDTM = DateTime.UtcNow;

            db.InsertOne(obj);
        }
        /// <summary>
        /// Deletion of testcase
        /// </summary>
        /// <param name="obj"></param>
        public DeleteResult Delete(object obj)
        {
            var filterBuilder = new FilterDefinitionBuilder<TestCaseModel>().
                            Eq(filter => filter._id, obj);

            return db.DeleteOne(filterBuilder);
        }
        /// <summary>
        /// Get TestCase By Id
        /// </summary>
        /// <param name="objectId"></param>
        /// <returns></returns>
        public TestCaseModel GetById(ObjectId objectId)
        {
            var filterBuilder = new FilterDefinitionBuilder<TestCaseModel>().
                            Eq(filter => filter._id, objectId);

            return db.Find(filterBuilder).FirstOrDefault();
        }
        /// <summary>
        /// Get TestCase by Name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public TestCaseModel GetByName(string name)
        {
            var filterBuilder = new FilterDefinitionBuilder<TestCaseModel>().
                            Eq(filter => filter.TestCaseName, name);

            return db.Find(filterBuilder).FirstOrDefault();
        }
        /// <summary>
        /// Get Id by TestCase name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public ObjectId GetId(string name)
        {
            var filterBuilder = new FilterDefinitionBuilder<TestCaseModel>().
                            Eq(filter => filter.TestCaseName, name);

            ObjectId = db.Find(filterBuilder).FirstOrDefault()._id;

            return ObjectId;
        }
        /// <summary>
        /// Update the testcase
        /// </summary>
        /// <param name="obj"></param>
        public UpdateResult Update(TestCaseModel obj)
        {
            UpdateResult result = null;
            UpdateOptions updateOptions = new UpdateOptions
            {
                IsUpsert = true
            };

            FilterDefinition<TestCaseModel> filterBuilder = new FilterDefinitionBuilder<TestCaseModel>().
                                    Eq(filter => filter._id, ObjectId);

            UpdateDefinition<TestCaseModel> update;
            PropertyInfo[] properties = typeof(TestCaseModel).GetProperties();
            foreach (PropertyInfo property in properties)
            {
                if (property.GetValue(obj) != null && property.Name != "_id" && property.Name != "TestClass_id")
                {
                    update = Builders<TestCaseModel>.Update.Set(property.Name, property.GetValue(obj));
                    result = db.UpdateOne(filterBuilder, update, updateOptions);
                }
            }

            return result;
        }
        /// <summary>
        /// Get testcase id's by module id
        /// </summary>
        /// <param name="classId"></param>
        /// <returns></returns>
        public IEnumerable<object> GetIdsByClassId(object classId)
        {
            var filter = Builders<TestCaseModel>.Filter.Eq("TestClass_id", classId);
            yield return db.Find(filter).ToList().Select(obj => obj._id);
        }
        /// <summary>
        /// Get testcase id by module id and name
        /// </summary>
        /// <param name="module"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public object GetIdByClassIdByName(object moduleId, string name)
        {
            var Builder = new FilterDefinitionBuilder<TestCaseModel>();

            var filter = Builder.Eq(filterBuilder => filterBuilder.TestClass_id, moduleId) &
                Builder.Eq(filterBuilder => filterBuilder.TestCaseName, name);

            return db.Find(filter).FirstOrDefault()._id;
        }
        /// <summary>
        /// Get all documents from testcase collection
        /// </summary>
        /// <returns></returns>
        public IQueryable<TestCaseModel> GetAll()
        {
            return db.AsQueryable();
        }
        /// <summary>
        /// Is testcase is exits
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool IsExists(string name)
        {
            var filterBuilder = new FilterDefinitionBuilder<TestCaseModel>().
                                        Eq(filter => filter.TestCaseName, name);

            return db.Find(filterBuilder).FirstOrDefault()._id != null ? true : false;
        }
        
    }
}
