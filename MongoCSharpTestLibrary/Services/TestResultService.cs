using NextGenTestLibrary.IServices;
using MongoDB.Bson;
using MongoTestDatabaseLibrary.DAL;
using MongoTestDatabaseLibrary.Models;
using NextGenTestLibrary.Enums;
using NextGenTestLibrary.Utilities;
using System.Collections.Generic;

namespace NextGenTestLibrary.Services
{
    public class TestResultService : ITestResultService
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
    }
}
