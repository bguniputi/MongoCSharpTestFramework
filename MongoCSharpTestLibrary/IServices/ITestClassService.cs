using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NextGenTestLibrary.IServices
{
    interface ITestClassService
    {
        /// <summary>
        /// Refresh module collection
        /// </summary>
        /// <param name="sender"></param>
        void RefreshTestClasses(Assembly sender);
        /// <summary>
        /// Add test classes
        /// </summary>
        /// <param name="testClasses"></param>
        void AddTestClasses(dynamic testClasses);
        /// <summary>
        /// Get test classes
        /// </summary>
        /// <param name="sender"></param>
        /// <returns></returns>
        dynamic GetTestClasses(Assembly sender);
        /// <summary>
        /// Get Executable test classes
        /// </summary>
        /// <returns></returns>
        dynamic GetExecutableTestClasses(dynamic moduleIds);
    }
}
