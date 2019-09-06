using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoTestDatabaseLibrary.IRepository
{
    public interface IGlobalTestDataRepository<TClass> :
        ITestRepository<TClass> where TClass : class,new()
    {
    }
}
