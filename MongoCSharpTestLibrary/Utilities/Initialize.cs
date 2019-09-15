using NextGenTestLibrary.Services;
using MongoDB.Bson;
using NextGenTestLibrary.Utilities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextGenTestLibrary.Utilities
{
    public static class Initialize
    {
        public static readonly object lockObj = new object();
        private static ObjectId _projectId;
        
        /// <summary>
        /// Default constructor with initialization of settings
        /// </summary>
        static Initialize()
        {
            ProjectName = ConfigurationManager.AppSettings["ProjectName"];
            DisplayExecutionTimeInLogger = Convert.ToBoolean(ConfigurationManager.AppSettings["DisplayExecutionTimeInLog"]);
            TestCycle = ConfigurationManager.AppSettings["TestCycle"];
            IsDependencyCheckEnabled = Convert.ToBoolean(ConfigurationManager.AppSettings["IsDependencyCheckEnabled"]);
            RetryOnException = Convert.ToBoolean(ConfigurationManager.AppSettings["RetryOnException"]);
        }
        /// <summary>
        /// Get project name
        /// </summary>
        public static string ProjectName { get; private set; }
        /// <summary>
        /// Get whether execution time in logger is required
        /// </summary>
        public static bool DisplayExecutionTimeInLogger { get; private set; }
        /// <summary>
        /// Get test cycle
        /// </summary>
        public static string TestCycle { get; private set; }
        /// <summary>
        /// Get project id
        /// </summary>
        public static ObjectId ProjectId
        {
            get
            {
                if(_projectId == null)
                {
                    LoadConfiguration();
                }

                return _projectId;
            }
        }
        /// <summary>
        /// Is dependency check enabled
        /// </summary>
        public static bool IsDependencyCheckEnabled { get; private set; }
        /// <summary>
        /// RetryOnFail
        /// </summary>
        public static bool RetryOnException { get; private set; }
        /// <summary>
        /// Load Configuration
        /// </summary>
        private static void LoadConfiguration()
        {
            lock (lockObj)
            {
                _projectId = new TestProjectService().GetProjectId(ProjectName);
                if (_projectId == null)
                {
                    new DataFillService().UpdateProjectData();
                    _projectId = new TestProjectService().GetProjectId(ProjectName);
                }
            }
        }

    }
}
