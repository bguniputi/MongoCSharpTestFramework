using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoTestDatabaseLibrary.IRepository
{
    public interface ITestRepository<TClass> where TClass: class,new()
    {
        /// <summary>
        /// Create
        /// </summary>
        /// <param name="obj"></param>
        void Create(TClass obj);
        /// <summary>
        /// Update
        /// </summary>
        /// <param name="obj"></param>
        UpdateResult Update(TClass obj);
        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="obj"></param>
        DeleteResult Delete(object obj);
        /// <summary>
        /// Id
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        ObjectId GetId(string name);
        /// <summary>
        /// By Name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        TClass GetByName(string name);
        /// <summary>
        /// By Id
        /// </summary>
        /// <param name="objectId"></param>
        /// <returns></returns>
        TClass GetById(ObjectId objectId);
        /// <summary>
        /// Get all documents
        /// </summary>
        /// <returns></returns>
        IQueryable<TClass> GetAll();

    }
}
