using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NextGenTestLibrary.IServices
{
    interface ITestModuleService
    {
        /// <summary>
        /// Create a new modules
        /// </summary>
        /// <param name="testModules"></param>
        void AddModules(IList<string> testModules);
        /// <summary>
        /// Refresh module collection
        /// </summary>
        /// <param name="sender"></param>
        void RefreshModules(Assembly sender);
        /// <summary>
        /// Get list of modules
        /// </summary>
        /// <param name="sender"></param>
        /// <returns></returns>
        dynamic GetModuleClasses(Assembly sender);
        /// <summary>
        /// Get Executable Module list
        /// </summary>
        /// <returns></returns>
        dynamic GetExecutableModules();
    }
}
