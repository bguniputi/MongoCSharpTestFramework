using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextGenTestLibrary.Services
{
    public abstract class TestBaseClassService
    {
        /// <summary>
        /// Custom method with testcase Result like Passed(true)/Failed(False) and Can be override in dervied class
        /// for implementing the screenshot capturing for the failed testscase
        /// </summary>
        /// <param name="moduleName"></param>
        /// <param name="className"></param>
        /// <param name="testCaseName"></param>
        /// <param name="isTestCasePassed"></param>
        public virtual void EndOfTestCaseExecution
            (string moduleName, string className, string testCaseName,bool isTestCasePassed)
        {

        }
    }
}
