using MongoDB.Driver;

namespace MongoCSharpLibrary.DataAccessLayer
{
    public sealed class GetMongoClient
    {
        private GetMongoClient()
        {

        }

        private static readonly object padlock = new object();
        private static IMongoClient instance = null;
        /// <summary>
        /// Get singleton mongo client object
        /// </summary>
        /// <param name="Url"></param>
        /// <returns></returns>
        public static IMongoClient Instance(MongoUrl Url)
        {
            lock (padlock)
            {
               if (instance == null)
               {
                   instance = new MongoClient(Url);
                }
            }

            return instance;
        }

    }
}
