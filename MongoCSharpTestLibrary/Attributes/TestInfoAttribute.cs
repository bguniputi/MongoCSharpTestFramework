using NextGenTestLibrary.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextGenTestLibrary.Attributes
{
    /// <summary>
    /// Test info attribute
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true,Inherited = false)]
    public class TestInfoAttribute : Attribute
    {
        public TestCaseType TestCaseType { get; private set; }
        public TestPriority Priority { get; private set; }
        public string Version { get; private set; }
        public string TestCategory { get; private set; }

        /// <param name="testCaseType"></param>
        /// <param name="priority"></param>
        /// <param name="version"></param>
        /// <param name="testCategory"></param>
        public TestInfoAttribute
            (TestCaseType testCaseType, TestPriority priority,string version,string testCategory)
        {
            TestCaseType = testCaseType;
            Priority = priority;
            Version = version;
            TestCategory = testCategory;
        }
    }
}
