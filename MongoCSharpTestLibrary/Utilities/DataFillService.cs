using MongoTestDatabaseLibrary.DAL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using NextGenTestLibrary.IServices;
using NextGenTestLibrary.Services;
using MongoTestDatabaseLibrary.Models;

namespace NextGenTestLibrary.Utilities
{
    internal class DataFillService : IDisposable
    {
        private readonly MongoRepository mongoRepository;
        private ITestModuleService testModuleService;
        private ITestCaseService testCaseService;
        private ITestClassService testClassService;

        internal DataFillService()
        {
            mongoRepository = new MongoRepository();
        }
        /// <summary>
        /// Refresh master database
        /// </summary>
        /// <param name="sender"></param>
        internal void RefreshMasterDatabase(Assembly sender)
        {
            testModuleService = new TestModuleService();
            testCaseService = new Services.TestCaseService();
            testClassService = new TestClassService();

            //Update Project data
            UpdateProjectData();

            //Refresh Modules
            testModuleService.RefreshModules(sender);

            //Refres Test Classes
            testClassService.RefreshTestClasses(sender);

            //Refresh TestCases
            IDictionary<int, Type> testClasses = testClassService.GetTestClasses(sender);
            List<string> testModuleClasses = testClasses.Select(tc => tc.Value.FullName).ToList();
            IList<TestCaseModel> testCases = testCaseService.GetTestCaseList(testModuleClasses, sender);
            testCaseService.RefreshTestCases(testCases,sender);

        }
        /// <summary>
        /// Update project detail
        /// </summary>
        internal void UpdateProjectData()
        {
            var testRepository = mongoRepository.GetTestProjectRepository;
            string projectName = ConfigurationManager.AppSettings["ProjectName"];
            string regulatory = ConfigurationManager.AppSettings["Regulator"];
            bool isActive = bool.Parse(ConfigurationManager.AppSettings["IsActive"]);

            if ((projectName != null || projectName.Trim() != string.Empty) &&
                (regulatory != null || regulatory.Trim() != string.Empty))

                if(!testRepository.GetAll().Any(a => a.ProjectName == projectName 
                        && a.Regulator == regulatory))
                {
                    TestProjectModel testProjectModel = new TestProjectModel()
                    {
                        ProjectName = projectName,
                        Regulator = regulatory,
                        CreatedDTM = DateTime.UtcNow
                    };

                    testRepository.Create(testProjectModel);

                }
            else if (testRepository.GetAll().Any(a => a.ProjectName == projectName 
                        && a.Regulator != regulatory))
            {
                TestProjectModel testProjectModel = new TestProjectModel
                {
                    ProjectName = projectName,
                    Regulator = regulatory
                };
                testRepository.Create(testProjectModel);

            }
            else if (testRepository.IsActive(projectName) != isActive)
            {
                  testRepository.GetId(projectName);
                  testRepository.Update(new TestProjectModel() { IsActive = isActive });
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
                    mongoRepository.Dispose();
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
