using MongoDB.Bson;
using System.Collections.Generic;

namespace NextGenTestLibrary.IServices
{
    public interface ITestDataService
    {
        /// <summary>
        /// Method to get testdata by id
        /// </summary>
        /// <param name="testDataID"></param>
        /// <returns></returns>
        Dictionary<string, object> GetTestData(ObjectId testcaseID,int testDataOrder);
        /// <summary>
        /// Method to get test data field value
        /// </summary>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        object GetFieldValue(string fieldName);
        /// <summary>
        /// Method to get test data field name
        /// </summary>
        /// <param name="fieldValue"></param>
        /// <returns></returns>
        string GetFieldName(object fieldValue);
        /// <summary>
        /// Method to get global test data field value
        /// </summary>
        /// <param name="globalfieldName"></param>
        /// <returns></returns>
        object GetGlobalFieldValue(string globalfieldName);
        /// <summary>
        /// Method to get global test data field name
        /// </summary>
        /// <param name="globalfieldValue"></param>
        /// <returns></returns>
        string GetGlobalFieldName(object globalfieldValue);
    }
}
