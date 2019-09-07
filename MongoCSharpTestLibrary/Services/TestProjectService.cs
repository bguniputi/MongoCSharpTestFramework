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
    public class TestProjectService : ITestProjectService , IDisposable
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

        /// <summary>
        /// Implementation of IDisposable pattern
        /// </summary>
        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    mongoRepository.Dispose();
                }

            }

            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}
