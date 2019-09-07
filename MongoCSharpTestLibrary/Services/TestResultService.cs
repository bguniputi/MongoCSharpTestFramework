using NextGenTestLibrary.IServices;
using MongoDB.Bson;
using MongoTestDatabaseLibrary.DAL;
using MongoTestDatabaseLibrary.Models;
using NextGenTestLibrary.Enums;
using NextGenTestLibrary.Utilities;
using System.Collections.Generic;
using System;

namespace NextGenTestLibrary.Services
{
    public class TestResultService : ITestResultService, IDisposable
    {
        private readonly MongoRepository mongoRepository;
        public TestResultService()
        {
            mongoRepository = new MongoRepository();
        }
        /// <summary>
        /// Insert test results
        /// </summary>
        /// <param name="testCaseName"></param>
        /// <param name="resultTypes"></param>
        /// <param name="results"></param>
        public void InsertTestResults
            (string testCaseName,TestResult resultTypes,IEnumerable<string> results)
        {
            ObjectId testCaseId = mongoRepository.GetTestCaseRepository.GetId(testCaseName);
            TestResultModel testResultModel = new TestResultModel
            {
                TestCase_Id = testCaseId,
                TestCycle = Initialize.TestCycle,
                Result = resultTypes.ToString(),
                Resultsets = results

            };

            mongoRepository.GetTestResultRepository.Create(testResultModel);
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
