using NextGenTestLibrary.IServices;
using MongoDB.Bson;
using MongoTestDatabaseLibrary.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextGenTestLibrary.Services
{
    public class TestProjectService : ITestProjectService
    {
        private readonly MongoRepository mongoRepository;
        internal TestProjectService()
        {
            mongoRepository = new MongoRepository();
        }
        /// <summary>
        /// Get project object id
        /// </summary>
        /// <param name="projectName"></param>
        /// <returns></returns>
        public ObjectId GetProjectId(string projectName)
        {
            return mongoRepository.GetTestProjectRepository.GetId(projectName);
        }

        
    }
}
