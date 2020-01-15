using MongoCSharpLibrary.DataAccessLayer;
using MongoDB.Driver;
using System;

namespace MongoTestDatabaseLibrary.DAL
{
    internal abstract class DbContext : IDisposable
    {
        private IMongoClient _client;
        private IMongoDatabase _Database;
        private bool isDisposed = false;

        /// <summary>
        /// Provide the Mongodb Connection string 
        /// </summary>
        /// <param name="connectionString"></param>
        protected DbContext(string connectionString)
        {
            MongoUrl url = new MongoUrl(connectionString);
            _client = GetMongoClient.Instance(url);
            _Database = GetMongoDatabase.Database(_client,url.DatabaseName);
        }

        public IMongoDatabase Database => _Database;
        /// <summary>
        /// Drop the existing database
        /// </summary>
        /// <param name="name"></param>

        protected void DropDatabase(string name)
        {
            _client.DropDatabase(name);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (isDisposed)
                return;

            if (disposing)
            {
                _client = null;
                _Database = null;
            }

            isDisposed = true;
        }
        /// <summary>
        /// Dispose method
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);                     
        }
    }
}
