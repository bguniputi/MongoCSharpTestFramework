using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoTestDatabaseLibrary.IRepository
{
    public interface ITestCaseRepository<TClass> : 
        ITestRepository<TClass> where TClass : class, new()
    {
        /// <summary>
        /// Is testcase exists
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        bool IsExists(string name);
    }
    
}
