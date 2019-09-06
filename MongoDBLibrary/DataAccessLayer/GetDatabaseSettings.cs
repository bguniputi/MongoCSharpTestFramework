using MongoDB.Driver;

namespace MongoCSharpLibrary.DataAccessLayer
{
    public static class GetDatabaseSettings
    {
        /// <summary>
        /// Get databasename
        /// </summary>
        public static string DatabaseName { get; private set; }
        /// <summary>
        /// Set database settings
        /// </summary>
        public static MongoDatabaseSettings DatabaseSettings { get; private set; } = null;

    }
}
