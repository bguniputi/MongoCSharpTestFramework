using NextGenTestLibrary.Enums;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Reflection;
using NUnit.Framework.Interfaces;

namespace NextGenTestLibrary.IServices
{
    public interface ITestCaseService
    {
        /// <summary>
        /// Method to get testcases
        /// </summary>
        /// <param name="sender"></param>
        /// <returns></returns>
        IList<Tuple<string, string,int>> GetTestCases(Assembly sender);
        /// <summary>
        /// Method to execute testcases
        /// </summary>
        /// <param name="testCaseFullName"></param>
        /// <param name="testModuleId"></param>
        /// <param name="testCaseId"></param>
        /// <returns></returns>
        void ExecuteTestCase(string className,string testCaseName,int testDataOrder);
        /// <summary>
        /// Get list of testcases
        /// </summary>
        /// <param name="testClasses"></param>
        /// <param name="sender"></param>
        /// <returns></returns>
        dynamic GetTestCaseList(List<string> testClasses, Assembly sender);
        /// <summary>
        /// Refresh testcase collection
        /// </summary>
        /// <param name="testCases"></param>
        /// <param name="sender"></param>
        void RefreshTestCases(object testCases,Assembly sender);
        /// <summary>
        /// Capturing of TestStatus and insertion
        /// </summary>
        /// <param name="testStatus"></param>
        void TestCaseResults(TestStatus testStatus);
    }
}
