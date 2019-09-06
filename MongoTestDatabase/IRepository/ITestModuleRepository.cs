using MongoTestDatabaseLibrary.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoTestDatabaseLibrary.IRepository
{
    public interface ITestModuleRepository<TClass> 
        : ITestRepository<TClass> where TClass : class , new()
    {
        /// <summary>
        /// Is test module exists
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        bool IsExists(string name);
    }
}
