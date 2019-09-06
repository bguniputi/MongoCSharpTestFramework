using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoCSharpLibrary.DataAccessLayer
{
    public class GenericDataAdapter<TClass> where TClass : class
    {
        public GenericDataAdapter()
        {

        }
        /// <summary>
        /// Get collection
        /// </summary>
        /// <param name="database"></param>
        /// <param name="CollectionName"></param>
        /// <returns></returns>
        public static IMongoCollection<TClass> GetCollection
            (IMongoDatabase database, string CollectionName)
        {
            return database.GetCollection<TClass>(CollectionName);
        }
        /// <summary>
        /// Get collection by name
        /// </summary>
        /// <param name="database"></param>
        /// <param name="CollectionName"></param>
        /// <returns></returns>
        public static IMongoCollection<object> GetCollectionName
            (IMongoDatabase database, string CollectionName)
        {
            return database.GetCollection<object>(CollectionName);
        }
        /// <summary>
        /// Insert document
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="document"></param>
        public static void InsertDocument
            (IMongoCollection<TClass> collection, TClass document)
        {
            collection.InsertOne(document);
        }
        /// <summary>
        /// Insert document asynchronous
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="document"></param>
        /// <returns></returns>
        public static async Task InsertDocumentAsync
            (IMongoCollection<TClass> collection, TClass document)
        {
            await collection.InsertOneAsync(document);
        }
        /// <summary>
        /// Update document
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="filter"></param>
        /// <param name="update"></param>
        /// <returns></returns>
        public static UpdateResult UpdateDocument
            (IMongoCollection<TClass> collection, FilterDefinition<TClass> filter,
              UpdateDefinition<TClass> update)
        {
            return collection.UpdateOne(filter, update);
        }
        /// <summary>
        /// Update document asynchronoous
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="filter"></param>
        /// <param name="update"></param>
        /// <returns></returns>
        public static async Task<UpdateResult> UpdateDocumentAsync
            (IMongoCollection<TClass> collection, FilterDefinition<TClass> filter,
               UpdateDefinition<TClass> update)
        {
            return await collection.UpdateOneAsync(filter, update);
        }
        /// <summary>
        /// Find query
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        public static TClass FindQuery
            (IMongoCollection<TClass> collection, FilterDefinition<TClass> filter)
        {
            return collection.Find(filter).FirstOrDefault();
        }
        /// <summary>
        /// Find query asynchronous
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        public static async Task<TClass> FindQueryAsync
            (IMongoCollection<TClass> collection, FilterDefinition<TClass> filter)
        {
            return await collection.Find(filter).FirstOrDefaultAsync();
        }
        /// <summary>
        /// Find query list
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        public static IList<TClass> FindQueryList
            (IMongoCollection<TClass> collection, FilterDefinition<TClass> filter)
        {
            return collection.Find(filter).ToList();
        }
        /// <summary>
        /// Find query list asynchronous
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        public static async Task<IList<TClass>> FindQueryListAsync
            (IMongoCollection<TClass> collection, FilterDefinition<TClass> filter)
        {
            return await collection.Find(filter).ToListAsync();
        }
        /// <summary>
        /// Delete document
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        public static DeleteResult DeleteDocument
            (IMongoCollection<TClass> collection,FilterDefinition<TClass> filter)
        {
            return collection.DeleteOne(filter);
        }
        /// <summary>
        /// Get filter builder
        /// </summary>
        /// <returns></returns>
        public static FilterDefinitionBuilder<TClass> GetBuilder()
        {
            return new FilterDefinitionBuilder<TClass>();
        }
        /// <summary>
        /// Get sort filter
        /// </summary>
        /// <returns></returns>
        public static SortDefinitionBuilder<TClass> GetSort()
        {
            return new SortDefinitionBuilder<TClass>();
        }
    }
}
