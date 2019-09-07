using NextGenTestLibrary.Attributes;
using NextGenTestLibrary.Utilities;
using MongoDB.Bson;
using MongoTestDatabaseLibrary.DAL;
using MongoTestDatabaseLibrary.Models;
using NextGenTestLibrary.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;


namespace NextGenTestLibrary.Services
{
    public class TestModuleService : ITestModuleService, IDisposable
    {
        private readonly MongoRepository mongoRepository;

        internal TestModuleService()
        {
            mongoRepository = new MongoRepository();
        }

        /// <summary>
        /// Get modules from assembly
        /// </summary>
        /// <param name="sender"></param>
        /// <returns></returns>
        public dynamic GetModuleClasses(Assembly sender)
        {
            return sender.GetTypes()
                            .Where(t => t.IsPublic && !t.IsAbstract && !t.IsSealed && t.IsClass)
                            .Where(t => t.IsDefined(typeof(ModuleAttribute), false))
                            .Select(t => t.FullName)
                            .ToList();
        }
        /// <summary>
        /// Add new modules
        /// </summary>
        /// <param name="testModules"></param>
        /// <param name="sender"></param>
        public void AddModules(IList<string> testModule)
        {
            List<TestModuleModel> modules = testModule.Select(a => new TestModuleModel
            {
                 ModuleType = a,
                 CreatedDTM = DateTime.UtcNow,
                 IsActive = true
            }).ToList();

            foreach (var module in modules)
            {
                mongoRepository.GetTestModuleRepository.Create(module);
            }
        }
        /// <summary>
        /// Add individual Module
        /// </summary>
        /// <param name="testModule"></param>
        private void AddModule(string testModule)
        {
            TestModuleModel module = new TestModuleModel()
            {
                ModuleType = testModule,
                CreatedDTM = DateTime.UtcNow               
            };

            mongoRepository.GetTestModuleRepository.Create(module);
        }
        /// <summary>
        /// Refresh modules
        /// </summary>
        /// <param name="sender"></param>
        public void RefreshModules(Assembly sender)
        {
           IList<string> moduleClassList = GetModuleClasses(sender);
            
            if (mongoRepository.GetTestModuleRepository.GetAll().Count().Equals(0))
            {
                //Add below Modules
                IList<string> moduleList = GetUniqueModuleList(moduleClassList,sender);
                AddModules(moduleList);
            }
            else
            {
                foreach (string module in moduleClassList)
                {
                    string type = GetModuleType(module, sender);
                    if (mongoRepository.GetTestModuleRepository.IsExists(type) == false)
                    {
                        //Add Module
                        AddModule(module);
                    }

                }

            }
           
        }

        /// <summary>
        /// Get Module Type
        /// </summary>
        /// <param name="moduleName"></param>
        /// <param name="sender"></param>
        /// <returns></returns>
        public static dynamic GetModuleType(string moduleName,Assembly sender)
        {
            Type type = sender.GetType(moduleName);
            return Attribute.GetCustomAttribute(type, typeof(ModuleAttribute)) is ModuleAttribute attribute
                ? (dynamic)attribute.ModuleType.ToString()
                : (dynamic)null;

        }
        /// <summary>
        /// Get Unique Modules List
        /// </summary>
        /// <param name="classNames"></param>
        /// <param name="sender"></param>
        /// <returns></returns>
        private IList<string> GetUniqueModuleList(IList<string> classNames,Assembly sender)
        {
            HashSet<string> moduleList = new HashSet<string>();
            foreach (var className in classNames)
            {
                string type = GetModuleType(className,sender);
                moduleList.Add(type);

            }

            return moduleList.ToList();
        }
        /// <summary>
        /// Get executable modules
        /// </summary>
        /// <returns></returns>
        public dynamic GetExecutableModules()
        {
            return mongoRepository.GetTestModuleRepository.GetAll()
                    .Where(m => m.IsActive == true)
                    .Select(m => m._id)
                    .ToList();
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
