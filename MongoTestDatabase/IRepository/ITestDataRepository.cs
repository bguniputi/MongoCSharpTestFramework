using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoTestDatabaseLibrary.IRepository
{
    public interface ITestDataRepository<TClass>: 
        ITestRepository<TClass> where TClass : class, new()
    {
        /// <summary>
        /// Get Testdata by Testcase Id
        /// </summary>
        /// <param name="testcaseId"></param>
        /// <returns></returns>
        IEnumerable<TClass> GetTestDataByTestCaseId(ObjectId testcaseId);
        /// <summary>
        /// Get Testdata Id's by TestCase Id
        /// </summary>
        /// <param name="testcaseId"></param>
        /// <returns></returns>
        IEnumerable<ObjectId> GetIdsByTestCaseId(ObjectId testcaseId);
        /// <summary>
        /// Get testdata key and value pairs by Testdata Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        BsonDocument GetTestDataExtraFieldsById(ObjectId id);
        /// <summary>
        /// Update testdata by field name
        /// </summary>
        void UpdateTestDataByFieldName();

    }
}
