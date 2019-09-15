using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextGenTestLibrary.Loggers
{
    public static class ContextLogger
    {
        /// <summary>
        /// Logging the testCase before execution
        /// </summary>
        /// <param name="moduleName"></param>
        /// <param name="testClassName"></param>
        /// <param name="testCaseName"></param>
        /// <param name="testCaseData"></param>
        public static void LogBeforeTestCase
            (string moduleName,string testClassName,string testCaseName,object testCaseData)
        {
            var logObjBefore = new { ModuleName = moduleName,
                                     TestClassName = testClassName,
                                     TestCaseName = testClassName,
                                   };

            Logger.log.Information("TestCase Execution Start:" + testCaseName + ":{@logDetailObj}", logObjBefore);
            Logger.log.Debug("Test Data: {@logDetailObj} ", testCaseData);
        }
        /// <summary>
        /// Logging the testCase after fails
        /// </summary>
        /// <param name="moduleName"></param>
        /// <param name="testClassName"></param>
        /// <param name="testCaseName"></param>
        /// <param name="testCaseData"></param>
        /// <param name="timeElapsed"></param>
        /// <param name="validationResult"></param>
        public static void LogAfterTestCaseFails
            (string moduleName, string testClassName, string testCaseName, object testCaseData,string timeElapsed,
                object validationResult)
        {
            var logObjAfterFails = new
            {
                ModuleName = moduleName,
                TestClassName = testClassName,
                TestCaseName = testClassName,
                ElaspedTime = timeElapsed,
                testCaseData
            };

            Logger.log.Error("TestCase Fails" + testCaseName + ":{@logDetailObj}", logObjAfterFails);
            Logger.log.Debug("{logDetailObj}", validationResult);
        }
        /// <summary>
        /// Logging the testCase afer pass
        /// </summary>
        /// <param name="moduleName"></param>
        /// <param name="testClassName"></param>
        /// <param name="testCaseName"></param>
        /// <param name="timeElapsed"></param>
        /// <param name="validationResult"></param>
        public static void LogAfterTestCasePass
            (string moduleName, string testClassName, string testCaseName, string timeElapsed, object validationResult)
        {
            var logObjAfterPass = new
            {
                ModuleName = moduleName,
                TestClassName = testClassName,
                TestCaseName = testClassName,
                ElaspedTime = timeElapsed
            };

            Logger.log.Information("TestCase Pass" + testCaseName + ":{@logDetailObj}", logObjAfterPass);
            Logger.log.Debug("{logDetailObj}", validationResult);
        }
        /// <summary>
        /// Logging the testCase afer pass
        /// </summary>
        /// <param name="moduleName"></param>
        /// <param name="testClassName"></param>
        /// <param name="testCaseName"></param>
        /// <param name="timeElapsed"></param>
        /// <param name="validationResult"></param>
        public static void LogAfterTestCaseInConclusive
            (string moduleName, string testClassName, string testCaseName, string timeElapsed, object validationResult)
        {
            var logObjAfterInconclusive = new
            {
                ModuleName = moduleName,
                TestClassName = testClassName,
                TestCaseName = testClassName,
                ElaspedTime = timeElapsed
            };

            Logger.log.Information("TestCase Inconclusive" + testCaseName + ":{@logDetailObj}", logObjAfterInconclusive);
            Logger.log.Debug("{logDetailObj}", validationResult);
        }
        /// <summary>
        /// Logging the tetscase after skipped
        /// </summary>
        /// <param name="moduleName"></param>
        /// <param name="testClassName"></param>
        /// <param name="testCaseName"></param>
        /// <param name="timeElapsed"></param>
        /// <param name="validationResult"></param>
        public static void LogAfterTestCaseSkipped
            (string moduleName, string testClassName, string testCaseName, string timeElapsed, object validationResult)
        {
            var logObjAfterSkipped = new
            {
                ModuleName = moduleName,
                TestClassName = testClassName,
                TestCaseName = testClassName,
                ElaspedTime = timeElapsed
            };

            Logger.log.Information("TestCase Skipped" + testCaseName + ":{@logDetailObj}", logObjAfterSkipped);
            Logger.log.Debug("{logDetailObj}", validationResult);
        }
        /// <summary>
        /// Logging testCase if exception
        /// </summary>
        /// <param name="moduleName"></param>
        /// <param name="testClassName"></param>
        /// <param name="testCaseName"></param>
        /// <param name="Exception"></param>
        public static void LogIfException
            (string moduleName,string testClassName,string testCaseName,string Exception)
        {
            var logObjIfException = new
            {
                ModuleName = moduleName,
                TestClassName = testClassName,
                TestCaseName = testClassName,
                ErroMessage = Exception
            };

            Logger.log.Fatal("Error occured while executing TestCase: {@logDetailObj}", logObjIfException);
        }

        /// <summary>
        /// Log before retry start
        /// </summary>
        /// <param name="moduleName"></param>
        /// <param name="testClassName"></param>
        /// <param name="testCaseName"></param>
        /// <param name="testCaseData"></param>
        public static void LogRetryStart
            (string moduleName, string testClassName, string testCaseName, object testCaseData)
        {
            var logObjBefore = new
            {
                ModuleName = moduleName,
                TestClassName = testClassName,
                TestCaseName = testClassName,
            };

            Logger.log.Information("TestCase Re-Execution Start:" + testCaseName + ":{@logDetailObj}", logObjBefore);
            Logger.log.Debug("Test Data: {@logDetailObj} ", testCaseData);
        }

        public static void LogAfterRetryTestCaseFails
            (string moduleName, string testClassName, string testCaseName, object testCaseData, string timeElapsed,
                object validationResult)
        {
            var logObjAfterFails = new
            {
                ModuleName = moduleName,
                TestClassName = testClassName,
                TestCaseName = testClassName,
                ElaspedTime = timeElapsed,
                testCaseData
            };

            Logger.log.Error("TestCase Fails" + testCaseName + ":{@logDetailObj}", logObjAfterFails);
            Logger.log.Debug("{logDetailObj}", validationResult);
        }
    }
}
