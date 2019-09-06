using MongoCSharpLibrary.DataAccessLayer;
using MongoDB.Driver;
using System;

namespace MongoTestDatabaseLibrary.DAL
{
    internal abstract class DbContext : IDisposable
    {
        private IMongoClient client;
        
        /// <summary>
        /// Provide the Mongodb Connection string 
        /// </summary>
        /// <param name="connectionString"></param>
        protected DbContext(string connectionString)
        {
            MongoUrl url = new MongoUrl(connectionString);
            client = GetMongoClient.Instance(url);
            GetDatabase = GetMongoDatabase.Database(client,url.DatabaseName);
        }
        /// <summary>
        /// Get Database
        /// </summary>
        protected IMongoDatabase GetDatabase { get; private set; }
        /// <summary>
        /// Drop the existing database
        /// </summary>
        /// <param name="name"></param>
        protected void DropDatabase(string name)
        {
            client.DropDatabase(name);
        }
        /// <summary>
        /// Dispose method
        /// </summary>
        public virtual void Dispose()
        {
            GetDatabase = null;
            client = null;                      
        }
    }
}
