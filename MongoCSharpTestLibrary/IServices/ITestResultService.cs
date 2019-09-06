using NextGenTestLibrary.Enums;
using System.Collections.Generic;

namespace NextGenTestLibrary.IServices
{
    public interface ITestResultService
    {
        /// <summary>
        /// Insert Test Results
        /// </summary>
        /// <param name="testCaseName"></param>
        /// <param name="resultTypes"></param>
        /// <param name="results"></param>
        void InsertTestResults(string testCaseName, TestResult resultTypes, IEnumerable<string> results);
    }
}
