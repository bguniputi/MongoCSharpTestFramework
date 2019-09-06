using MongoDB.Bson;
using MongoDB.Driver;
using MongoTestDatabaseLibrary.IRepository;
using MongoTestDatabaseLibrary.Models;
using System;
using System.Linq;
using System.Reflection;

namespace MongoTestDatabaseLibrary.Repository
{
    internal class TestModuleRepository : ITestModuleRepository<TestModuleModel>
    {
        private IMongoCollection<TestModuleModel> db;
        private ObjectId ObjectId { get; set; }
        
        internal TestModuleRepository(IMongoCollection<TestModuleModel> context)
        {
            db = context;
        }
        /// <summary>
        /// Insert of new modules
        /// </summary>
        /// <param name="obj"></param>
        public void Create(TestModuleModel obj)
        {
            if (obj.CreatedDTM == null)
                obj.CreatedDTM = DateTime.UtcNow;

            db.InsertOne(obj);
        }
        /// <summary>
        /// Deletion of module
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public DeleteResult Delete(object obj)
        {
            var filterBuilder = new FilterDefinitionBuilder<TestModuleModel>().
                Eq(filter => filter._id, obj);

            return db.DeleteOne(filterBuilder);
        }
        /// <summary>
        /// Get test module collection
        /// </summary>
        /// <returns></returns>
        public IQueryable<TestModuleModel> GetAll()
        {
            return db.AsQueryable();
        }
        /// <summary>
        /// Get test module by id
        /// </summary>
        /// <param name="objectId"></param>
        /// <returns></returns>
        public TestModuleModel GetById(ObjectId objectId)
        {
            var filterBuilder = new FilterDefinitionBuilder<TestModuleModel>().
                Eq(filter => filter._id, objectId);

            return db.Find(filterBuilder).FirstOrDefault();
        }
        /// <summary>
        /// Get test module by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public TestModuleModel GetByName(string name)
        {
            var filterBuilder = new FilterDefinitionBuilder<TestModuleModel>().
                Eq(filter => filter.ModuleType, name);

            return db.Find(filterBuilder).FirstOrDefault();
        }
        /// <summary>
        /// Get module id by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public ObjectId GetId(string name)
        {
            var filterBuilder = new FilterDefinitionBuilder<TestModuleModel>().
                Eq(filter => filter.ModuleType, name);

            ObjectId = db.Find(filterBuilder).FirstOrDefault()._id;

            return ObjectId;
        }
        /// <summary>
        /// Update module
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public UpdateResult Update(TestModuleModel obj)
        {
            UpdateResult result = null;
            UpdateOptions updateOptions = new UpdateOptions
            {
                IsUpsert = true
            };

            FilterDefinition<TestModuleModel> filterBuilder = new FilterDefinitionBuilder<TestModuleModel>().
                                    Eq(filter => filter._id, obj._id);

            UpdateDefinition<TestModuleModel> update;
            PropertyInfo[] properties = typeof(TestCaseModel).GetProperties();
            foreach (PropertyInfo property in properties)
            {
                if (property.GetValue(obj) != null && property.Name != "_id" && property.Name != "Project_id")
                {
                    update = Builders<TestModuleModel>.Update.Set(property.Name, property.GetValue(obj));
                    result = db.UpdateOne(filterBuilder, update, updateOptions);
                }
            }

            return result;
        }
        /// <summary>
        /// Is module exits
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool IsExists(string name)
        {
            var filterBuilder = new FilterDefinitionBuilder<TestModuleModel>().
                                    Eq(filter => filter.ModuleType, name);

            return db.Find(filterBuilder).FirstOrDefault()._id != null ? true : false;

        }

    }
}
