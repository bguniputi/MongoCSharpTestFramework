using MongoDB.Driver;
using MongoTestDatabaseLibrary.Models;
using System.Configuration;


namespace MongoTestDatabaseLibrary.DAL
{
    internal class MongoDbContext : DbContext
    {
        /// <summary>
        /// Read the connection string from Configuration manager
        /// </summary>
        private static readonly string connectionString =
            ConfigurationManager.ConnectionStrings["MongoDbContext"].ConnectionString ?? "mongodb://localhost";
        public MongoDbContext() 
            : base(connectionString)
        {
            
        }
        /// <summary>
        /// Get the Testproject collection
        /// </summary>
        internal IMongoCollection<TestProjectModel> TestProjects =>
            Database.GetCollection<TestProjectModel>("TestProjects");
        /// <summary>
        /// Get the Testcase database table
        /// </summary>
        internal IMongoCollection<TestCaseModel> TestCases =>
            Database.GetCollection<TestCaseModel>("TestCases");
        /// <summary>
        /// Get the Testdata collection
        /// </summary>
        internal IMongoCollection<TestDataModel> TestDatas =>
           Database.GetCollection<TestDataModel>("TestData");
        /// <summary>
        /// Get the Testresuls collection
        /// </summary>
        internal IMongoCollection<TestResultModel> TestResults =>
            Database.GetCollection<TestResultModel>("TestResults");
        /// <summary>
        /// Get the Test Module collection
        /// </summary>
        internal IMongoCollection<TestModuleModel> TestModules =>
            Database.GetCollection<TestModuleModel>("TestModules");
        /// <summary>
        /// Get the Global Test Data Collection
        /// </summary>
        internal IMongoCollection<GlobalTestDataModel> GlobalTestDatas =>
            Database.GetCollection<GlobalTestDataModel>("GlobalTestData");
        /// <summary>
        /// Get the test class collection
        /// </summary>
        internal IMongoCollection<TestClassModel> TestClasses =>
            Database.GetCollection<TestClassModel>("TestClasses");
    }
}
