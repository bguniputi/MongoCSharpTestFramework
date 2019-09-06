
using MongoDB.Bson;
using MongoDB.Driver;
using MongoTestDatabaseLibrary.IRepository;
using MongoTestDatabaseLibrary.Models;
using System;
using System.Linq;
using System.Reflection;

namespace MongoTestDatabaseLibrary.Repository
{
    internal class TestClassRepository : ITestClassRepository<TestClassModel>
    {
        private IMongoCollection<TestClassModel> db;
        private ObjectId ObjectId { get; set; }

        internal TestClassRepository(IMongoCollection<TestClassModel> context)
        {
            db = context;
        }
        /// <summary>
        /// insert new test class
        /// </summary>
        /// <param name="obj"></param>
        public void Create(TestClassModel obj)
        {
            db.InsertOne(obj);
        }
        /// <summary>
        /// Deletion of existing test class
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public DeleteResult Delete(object obj)
        {
            FilterDefinition<TestClassModel> filterBuilder = new FilterDefinitionBuilder<TestClassModel>().
                                                                    Eq(filter => filter._id, obj);

            return db.DeleteOne(filterBuilder);
        }
        /// <summary>
        /// Get all documents from test class module
        /// </summary>
        /// <returns></returns>
        public IQueryable<TestClassModel> GetAll()
        {
           return db.AsQueryable();
        }
        /// <summary>
        /// Get test class by id
        /// </summary>
        /// <param name="objectId"></param>
        /// <returns></returns>
        public TestClassModel GetById(ObjectId objectId)
        {
            FilterDefinition<TestClassModel> filterBuilder = new FilterDefinitionBuilder<TestClassModel>().
                                                                    Eq(filter => filter._id, objectId);

            return db.Find(filterBuilder).FirstOrDefault();
        }
        /// <summary>
        /// Get test class by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public TestClassModel GetByName(string name)
        {
            FilterDefinition<TestClassModel> filterBuilder = new FilterDefinitionBuilder<TestClassModel>().
                                                                    Eq(filter => filter.TestClassName, name);

            return db.Find(filterBuilder).FirstOrDefault();
        }
        /// <summary>
        /// Get test class id by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public ObjectId GetId(string name)
        {
            FilterDefinition<TestClassModel> filterBuilder = new FilterDefinitionBuilder<TestClassModel>().
                                                                    Eq(filter => filter.TestClassName, name);

            ObjectId = db.Find(filterBuilder).FirstOrDefault()._id;

            return ObjectId;
        }
        /// <summary>
        /// Update test class
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public UpdateResult Update(TestClassModel obj)
        {
            UpdateResult result = null;

            UpdateOptions updateOptions = new UpdateOptions
            {
                IsUpsert = true
            };

            FilterDefinition<TestClassModel> filterBuilder = new FilterDefinitionBuilder<TestClassModel>().
                                    Eq(filter => filter._id, ObjectId);

            UpdateDefinition<TestClassModel> update;
            PropertyInfo[] properties = typeof(TestClassModel).GetProperties();
            foreach (PropertyInfo property in properties)
            {
                if (property.GetValue(obj) != null && property.Name != "_id" && property.Name != "Module_id")
                {
                    update = Builders<TestClassModel>.Update.Set(property.Name, property.GetValue(obj));
                    result = db.UpdateOne(filterBuilder, update, updateOptions);
                }
            }

            return result;
        }
    }
}
