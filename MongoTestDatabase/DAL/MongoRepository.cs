using MongoTestDatabaseLibrary.IRepository;
using MongoTestDatabaseLibrary.Models;
using MongoTestDatabaseLibrary.Repository;
using System;

namespace MongoTestDatabaseLibrary.DAL
{
    public class MongoRepository : IDisposable
    {
        private bool isDisposed = false;
        private MongoDbContext _context;
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
            _context = new MongoDbContext();
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
                    this.testProjectRepository = new TestProjectRespository(_context.TestProjects);
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
                    this.testCaseRepository = new TestCaseRepository(_context.TestCases);
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
                    this.testDataRepository = new TestDataRepository(_context.TestDatas);
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
                    this.testResultRepository = new TestResultRepository(_context.TestResults);
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
                    this.testModuleRepository = new TestModuleRepository(_context.TestModules);
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
                    this.globalTestDataRepository = new GlobalTestDataRepository(_context.GlobalTestDatas);
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
                    this.testClassRepository = new TestClassRepository(_context.TestClasses);
                }

                return testClassRepository;
            }
        }
        /// <summary>
        /// Disposible pattern implementation
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (isDisposed)
                return;
            if (disposing)
            {
                _context.Dispose();
            }

            isDisposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
