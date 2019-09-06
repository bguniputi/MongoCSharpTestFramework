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
        private static bool _DisplayExecutionTimeInLogger;
        private static string _projectName;
        private static string _testCycle;
        private static bool _isDependenctEnabled;
        /// <summary>
        /// Default constructor with initialization of settings
        /// </summary>
        static Initialize()
        {
            _projectName = ConfigurationManager.AppSettings["ProjectName"];
            _DisplayExecutionTimeInLogger = Convert.ToBoolean(ConfigurationManager.AppSettings["DisplayExecutionTimeInLog"]);
            _testCycle = ConfigurationManager.AppSettings["TestCycle"];
            _isDependenctEnabled = Convert.ToBoolean(ConfigurationManager.AppSettings["IsDependencyCheckEnabled"]);
        }
        /// <summary>
        /// Get project name
        /// </summary>
        public static string ProjectName
        {
            get
            {
                return _projectName;
            }
        }
        /// <summary>
        /// Get whether execution time in logger is required
        /// </summary>
        public static bool DisplayExecutionTimeInLogger
        {
            get
            {
                return _DisplayExecutionTimeInLogger;
            }
        }
        /// <summary>
        /// Get test cycle
        /// </summary>
        public static string TestCycle
        {
            get
            {
                return _testCycle;
            }
        }
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
        public static bool IsDependencyCheckEnabled
        {
            get
            {
                return _isDependenctEnabled;
            }
        }
        /// <summary>
        /// Load Configuration
        /// </summary>
        private static void LoadConfiguration()
        {
            lock (lockObj)
            {
                _projectId = new TestProjectService().GetProjectId(_projectName);
                if (_projectId == null)
                {
                    new DataFillService().UpdateProjectData();
                    _projectId = new TestProjectService().GetProjectId(_projectName);
                }
            }
        }

    }
}
