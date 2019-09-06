using MongoTestDatabaseLibrary.IRepository;
using MongoTestDatabaseLibrary.Models;
using MongoTestDatabaseLibrary.Repository;
using System;

namespace MongoTestDatabaseLibrary.DAL
{
    public class MongoRepository : IDisposable
    {
        private MongoDbContext context;
        private ITestProjectRepository<TestProjectModel> testProjectRepository;       
        private ITestCaseRepository<TestCaseModel> testCaseRepository;
        private ITestDataRepository<TestDataModel> testDataRepository;
        private ITestResultRepository<TestResultModel> testResultRepository;
        private ITestModuleRepository<TestModuleModel> testModuleRepository;
        private IGlobalTestDataRepository<GlobalTestDataModel> globalTestDataRepository;
        private ITestClassRepository<TestClassModel> testClassRepository; 

        /// <summary>
        /// On creation of Mongo Repository with DIP
        /// </summary>
        public MongoRepository()
        {
            context = new MongoDbContext();
        }
        /// <summary>
        /// Project Repository
        /// </summary>
        public ITestProjectRepository<TestProjectModel> GetTestProjectRepository
        {
            get
            {
                if (this.testProjectRepository == null)
                {
                    this.testProjectRepository = new TestProjectRespository(context.TestProjects);
                }

                return testProjectRepository;
            }
        }
        /// <summary>
        /// TestCase Repository
        /// </summary>
        public ITestCaseRepository<TestCaseModel> GetTestCaseRepository
        {
            get
            {
                if (testCaseRepository == null)
                {
                    this.testCaseRepository = new TestCaseRepository(context.TestCases);
                }

                return testCaseRepository;
            }
        }
        /// <summary>
        /// TestData Repository
        /// </summary>
        public ITestDataRepository<TestDataModel> GetTestDataRepository
        {
            get
            {
                if(testDataRepository == null)
                {
                    this.testDataRepository = new TestDataRepository(context.TestDatas);
                }

                return testDataRepository;
            }
        }
        /// <summary>
        /// TestResult Repository
        /// </summary>
        public ITestResultRepository<TestResultModel> GetTestResultRepository
        {
            get
            {
                if (testResultRepository == null)
                {
                    this.testResultRepository = new TestResultRepository(context.TestResults);
                }

                return testResultRepository;
            }
        }
        /// <summary>
        /// Property to get Test module
        /// </summary>
        public ITestModuleRepository<TestModuleModel> GetTestModuleRepository
        {
            get
            {
                if (testModuleRepository == null)
                {
                    this.testModuleRepository = new TestModuleRepository(context.TestModules);
                }

                return testModuleRepository;
            }
        }
        /// <summary>
        /// Global testdata repository
        /// </summary>
        public IGlobalTestDataRepository<GlobalTestDataModel> GetGlobalTestDataRepository
        {
            get
            {
                if (globalTestDataRepository == null)
                {
                    this.globalTestDataRepository = new GlobalTestDataRepository(context.GlobalTestDatas);
                }

                return globalTestDataRepository;
            }
        }
        /// <summary>
        /// Get test class repository
        /// </summary>
        public ITestClassRepository<TestClassModel> GetTestClassRepository
        {
            get
            {
                if(testClassRepository == null)
                {
                    this.testClassRepository = new TestClassRepository(context.TestClasses);
                }

                return testClassRepository;
            }
        }
        /// <summary>
        /// Implementation of IDisposable pattern
        /// </summary>
        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }

            }

            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
