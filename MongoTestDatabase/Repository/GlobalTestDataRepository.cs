using System;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoTestDatabaseLibrary.IRepository;
using MongoTestDatabaseLibrary.Models;

namespace MongoTestDatabaseLibrary.Repository
{
    internal class GlobalTestDataRepository : IGlobalTestDataRepository<GlobalTestDataModel>
    {
        private IMongoCollection<GlobalTestDataModel> db;

        internal GlobalTestDataRepository(IMongoCollection<GlobalTestDataModel> context)
        {
            db = context;
        }

        public void Create(GlobalTestDataModel obj)
        {
            throw new NotImplementedException();
        }

        public DeleteResult Delete(object obj)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Get all documents from global test data collection
        /// </summary>
        /// <returns></returns>
        public IQueryable<GlobalTestDataModel> GetAll()
        {
            return db.AsQueryable();
        }
        /// <summary>
        /// Get global test data by id
        /// </summary>
        /// <param name="objectId"></param>
        /// <returns></returns>
        public GlobalTestDataModel GetById(ObjectId objectId)
        {
            var filterBuilder = new FilterDefinitionBuilder<GlobalTestDataModel>().
                Eq(filter => filter._id, objectId);

            return db.Find(filterBuilder).FirstOrDefault();
        }
        /// <summary>
        /// Get global test data by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public GlobalTestDataModel GetByName(string name)
        {
            throw new NotImplementedException();
        }

        public ObjectId GetId(string name)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Update global test data
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public UpdateResult Update(GlobalTestDataModel obj)
        {
            UpdateResult result = null;

            UpdateOptions updateOptions = new UpdateOptions
            {
                IsUpsert = true
            };

            FilterDefinition<GlobalTestDataModel> filter = Builders<GlobalTestDataModel>.Filter.Eq("_id", obj._id);
            UpdateDefinition<GlobalTestDataModel> update;
            PropertyInfo[] properties = typeof(ExpandoObject).GetProperties();
            foreach (PropertyInfo property in properties)
            {
                if (property.GetValue(obj) != null && property.Name != "_id" && property.Name != "Project_id")
                {
                    update = Builders<GlobalTestDataModel>.Update.Set(property.Name, property.GetValue(obj));
                    result = db.UpdateOne(filter, update, updateOptions);
                }
            }

            return result;
        }
    }
}
