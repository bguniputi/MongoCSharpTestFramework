using MongoTestDatabaseLibrary.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoTestDatabaseLibrary.IRepository
{
    public interface ITestProjectRepository<TClass>
        : ITestRepository<TClass> where TClass : class, new()
    {
        /// <summary>
        /// Is Project exists
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        bool IsExists(string name);
        /// <summary>
        /// Is Project is active
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        bool IsActive(string name);
    }
}
