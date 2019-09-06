using MongoDB.Driver;

namespace MongoCSharpLibrary.DataAccessLayer
{
    public sealed class GetMongoDatabase
    {
        private static readonly object _lock = new object();
        private static IMongoDatabase database = null;

        private GetMongoDatabase()
        {

        }
        /// <summary>
        /// Get mongo database singletn object
        /// </summary>
        /// <param name="client"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static IMongoDatabase Database
            (IMongoClient client,string name)
        {
            if(database == null)
            {
                lock (_lock)
                {
                    if (database == null)
                    {
                        database = client.GetDatabase(name, GetDatabaseSettings.DatabaseSettings);

                    }
                }

            }

            return database;
            
        }
    }
}
