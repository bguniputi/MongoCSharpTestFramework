using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextGenTestLibrary.IServices
{
    internal interface ITestProjectService
    {
        /// <summary>
        /// Get project id
        /// </summary>
        /// <param name="projectName"></param>
        /// <returns></returns>
        ObjectId GetProjectId(string projectName);
    }
}
