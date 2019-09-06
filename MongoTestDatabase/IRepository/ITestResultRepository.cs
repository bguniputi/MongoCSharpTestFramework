using MongoDB.Bson;
using MongoTestDatabaseLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoTestDatabaseLibrary.IRepository
{
    public interface ITestResultRepository<TClass> :
        ITestRepository<TClass> where TClass : class, new()
    {
        /// <summary>
        /// Test Results
        /// </summary>
        /// <returns></returns>
        IEnumerable<object> GetTestResults();
        /// <summary>
        /// Get test results by test cases id
        /// </summary>
        /// <param name="testCaseId"></param>
        /// <returns></returns>
        IEnumerable<TestResultModel> GetTestResultsByTestCaseId(ObjectId testCaseId);
    }
}
