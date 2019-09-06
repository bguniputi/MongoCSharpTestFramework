using MongoDB.Bson;
using MongoDB.Driver;
using MongoTestDatabaseLibrary.IRepository;
using MongoTestDatabaseLibrary.Models;
using System;
using System.Linq;
using System.Reflection;

namespace MongoTestDatabaseLibrary.Repository
{
    internal class TestProjectRespository : ITestProjectRepository<TestProjectModel>
    {
        //private MongoDbContext db;
        private IMongoCollection<TestProjectModel> db;
        private ObjectId ObjectId { get; set; }
       
        internal TestProjectRespository(IMongoCollection<TestProjectModel> context)
        {
            db = context;
        }
        /// <summary>
        /// Creation of new project
        /// </summary>
        /// <param name="obj"></param>
        public void Create(TestProjectModel obj)
        {
            if (obj.CreatedDTM == null)
                obj.CreatedDTM = DateTime.UtcNow;
            
            db.InsertOne(obj);
        }
        /// <summary>
        /// Deletion of testproject
        /// </summary>
        /// <param name="obj"></param>
        public DeleteResult Delete(object obj)
        {
            var filterBuilder = new FilterDefinitionBuilder<TestProjectModel>().
                Eq(filter => filter._id, obj);

            return db.DeleteOne(filterBuilder);
        }
        /// <summary>
        /// Get test project by id
        /// </summary>
        /// <param name="objectId"></param>
        /// <returns></returns>
        public TestProjectModel GetById(ObjectId objectId)
        {
            var filterBuilder = new FilterDefinitionBuilder<TestProjectModel>().
                Eq(filter => filter._id, objectId);

            return db.Find(filterBuilder).FirstOrDefault();
        }
        /// <summary>
        /// Get test prject by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public TestProjectModel GetByName(string name)
        {
            var filterBuilder = new FilterDefinitionBuilder<TestProjectModel>().
                Eq(filter => filter.ProjectName, name);

            return db.Find(filterBuilder).FirstOrDefault();
        }
        /// <summary>
        /// Get project id by project name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public ObjectId GetId(string name)
        {
            var filterBuilder = new FilterDefinitionBuilder<TestProjectModel>().
                Eq(filter => filter.ProjectName, name);

            ObjectId = db.Find(filterBuilder).FirstOrDefault()._id;

            return ObjectId;
        }
        /// <summary>
        /// Update the project
        /// </summary>
        /// <param name="obj"></param>
        public UpdateResult Update(TestProjectModel obj)
        {
            UpdateResult result = null;

            UpdateOptions updateOptions = new UpdateOptions
            {
                IsUpsert = true
            };

            FilterDefinition<TestProjectModel> filterBuilder = new FilterDefinitionBuilder<TestProjectModel>().
                                    Eq(filter => filter._id, ObjectId);

            UpdateDefinition<TestProjectModel> update;
            PropertyInfo[] properties = typeof(TestProjectModel).GetProperties();
            foreach (PropertyInfo property in properties)
            {
                if (property.GetValue(obj) != null && property.Name != "_id")
                {
                    update = Builders<TestProjectModel>.Update.Set(property.Name, property.GetValue(obj));
                    result = db.UpdateOne(filterBuilder, update, updateOptions);
                }
            }

            return result;
        }
        /// <summary>
        /// Get all project documents from collection
        /// </summary>
        /// <returns></returns>
        public IQueryable<TestProjectModel> GetAll()
        {
            return db.AsQueryable();
        }
        /// <summary>
        /// Is project exists
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool IsExists(string name)
        {
            var filterBuilder = new FilterDefinitionBuilder<TestProjectModel>().
                                    Eq(filter => filter.ProjectName, name);

            long count = db.Find(filterBuilder).CountDocuments();
            if (count > 0)
                return true;
            else
                return false;
        }
        /// <summary>
        /// Is project in active status
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool IsActive(string name)
        {
            var filterBuilder = new FilterDefinitionBuilder<TestProjectModel>().
                                   Eq(filter => filter.ProjectName, name);

            return db.Find(filterBuilder).FirstOrDefault().IsActive;
        }

    }
}
