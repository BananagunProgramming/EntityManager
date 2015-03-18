using System;
using System.Collections.Generic;
using System.Linq;
using EF.Implementation;
using EntityManager.Abstract;
using EntityManager.DatabaseContexts;
using log4net;

namespace EntityManager.Services
{
    public class ServiceQueryBase : IServiceQueryBase
    {
        private readonly DbContextScopeFactory _dbContextScopeFactory;
        public static readonly ILog Logger = LogManager.GetLogger(typeof(ServiceQueryBase));

        public ServiceQueryBase(DbContextScopeFactory dbContextScopeFactory)
        {
            _dbContextScopeFactory = dbContextScopeFactory;
        }

        public T GetEntity<T>(Guid id) where T : class
        {
            using (var dbContextScope = _dbContextScopeFactory.CreateReadOnly())
            {
                var dbContext = dbContextScope.DbContexts.Get<EntityManagerDbContext>();

                var result = dbContext.Set<T>().Find(id);

                Logger.Info("Got this entity");

                return result;
            }
        }

        public IEnumerable<T> GetAllEntities<T>() where T : class
        {
            using (var dbContextScope = _dbContextScopeFactory.Create())
            {
                var dbContext = dbContextScope.DbContexts.Get<EntityManagerDbContext>();
                var results = dbContext.Set<T>().ToList();

                return results;
            }
        }
    }
}