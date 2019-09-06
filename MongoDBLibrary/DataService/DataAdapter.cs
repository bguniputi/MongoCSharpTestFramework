using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MongoCSharpLibrary.DataService
{
    public static class DataAdapter
    {
        static DataAdapter()
        {

        }
        /// <summary>
        /// Creation of mongo client
        /// </summary>
        /// <param name="ConnectionString"></param>
        /// <returns></returns>
        public static IMongoClient CreateConnection
            (string ConnectionString)
        {
            return new MongoClient(ConnectionString);

        }
        /// <summary>
        /// Create mongo database
        /// </summary>
        /// <param name="client"></param>
        /// <param name="DatabaseName"></param>
        /// <returns></returns>
        public static IMongoDatabase CreateDatabase
            (this IMongoClient client, string DatabaseName)
        {
            return client.CreateDatabase(DatabaseName);
        }
        /// <summary>
        /// Get mongo database
        /// </summary>
        /// <param name="client"></param>
        /// <param name="DatabaseName"></param>
        /// <returns></returns>
        public static IMongoDatabase GetDatabase
            (this IMongoClient client,string DatabaseName)
        {
            return client.GetDatabase(DatabaseName);
        }
        /// <summary>
        /// Creation new collection
        /// </summary>
        /// <param name="database"></param>
        /// <param name="CollectionName"></param>
        /// <param name="options"></param>
        public static void CreateCollection
            (this IMongoDatabase database,string CollectionName,CreateCollectionOptions options = null)
        {
            database.CreateCollection(CollectionName,options);
        }
        /// <summary>
        /// Get collection
        /// </summary>
        /// <param name="database"></param>
        /// <param name="CollectionName"></param>
        /// <returns></returns>
        public static IMongoCollection<BsonDocument> GetCollection
            (this IMongoDatabase database ,string CollectionName)
        {
            return database.GetCollection<BsonDocument>(CollectionName);
        }
        /// <summary>
        /// Get collection by name to object
        /// </summary>
        /// <param name="database"></param>
        /// <param name="CollectionName"></param>
        /// <returns></returns>
        public static IMongoCollection<object> GetCollectionName
            (this IMongoDatabase database,string CollectionName)
        {
            return database.GetCollection<object>(CollectionName);
        }
        /// <summary>
        /// Insert new BSON document
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="document"></param>
        public static void InsertDocument
            (this IMongoCollection<BsonDocument> collection,BsonDocument document)
        {
            collection.InsertOne(document);
        }
        /// <summary>
        /// Insert of new document
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="document"></param>
        public static void InsertDocument
            (this IMongoCollection<object> collection, object document)
        {
            collection.InsertOne(document);
        }
        /// <summary>
        /// Insert new document asynchronous
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="document"></param>
        /// <returns></returns>
        public static async Task InsertDocumentAsync
            (this IMongoCollection<BsonDocument> collection, BsonDocument document)
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
            (this IMongoCollection<BsonDocument> collection,FilterDefinition<BsonDocument> filter,
              UpdateDefinition<BsonDocument> update)
        {
            return collection.UpdateOne(filter,update);
        }
        /// <summary>
        /// Update document asynchronous
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="filter"></param>
        /// <param name="update"></param>
        /// <returns></returns>
        public static async Task<UpdateResult> UpdateDocumentAsync
            (this IMongoCollection<BsonDocument> collection, FilterDefinition<BsonDocument> filter,
               UpdateDefinition<BsonDocument> update)
        {
            return await collection.UpdateOneAsync(filter, update);
        }
        /// <summary>
        /// Find query
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="document"></param>
        /// <returns></returns>
        public static BsonDocument FindQuery
            (this IMongoCollection<BsonDocument> collection,BsonDocument document)
        {
            return collection.Find(document).FirstOrDefault();
        }
        /// <summary>
        /// Find query asynchronous
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="document"></param>
        /// <returns></returns>
        public static async Task<BsonDocument> FindQueryAsync
            (this IMongoCollection<BsonDocument> collection, BsonDocument document)
        {
            return await collection.Find(document).FirstOrDefaultAsync();
        }
        /// <summary>
        /// Find query list
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="document"></param>
        /// <returns></returns>
        public static IList<BsonDocument> FindQueryList
            (this IMongoCollection<BsonDocument> collection, BsonDocument document)
        {
            return collection.Find(document).ToList();
        }
        /// <summary>
        /// Find query list asynchronous
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="document"></param>
        /// <returns></returns>
        public static async Task<IList<BsonDocument>> FindQueryListAsync
            (this IMongoCollection<BsonDocument> collection, BsonDocument document)
        {
            return await collection.Find(document).ToListAsync();
        }
        /// <summary>
        /// Get object id
        /// </summary>
        /// <returns></returns>
        public static ObjectId GetObjectId()
        {
            return ObjectId.GenerateNewId();
        }

    }
}
