using System;
using System.Collections.Generic;
using System.Linq;
using EF.Implementation;
using EntityManager.Abstract;
using EntityManager.DatabaseContexts;
using EntityManager.Domain.Services;

namespace EntityManager.Services
{
    public class ServiceQueryBase : IServiceQueryBase
    {
        public DbContextScopeFactory DbContextScopeFactory;
        public static readonly AzureWriter AuditLog = new AzureWriter();

        public ServiceQueryBase(DbContextScopeFactory dbContextScopeFactory)
        {
            DbContextScopeFactory = dbContextScopeFactory;
        }

        public T GetEntity<T>(Guid id) where T : class
        {
            using (var dbContextScope = DbContextScopeFactory.CreateReadOnly())
            {
                var dbContext = dbContextScope.DbContexts.Get<EntityManagerDbContext>();

                var result = dbContext.Set<T>().Find(id);

                //AuditLog.Audit(typeof(T).ToString());

                return result;
            }
        }

        public IEnumerable<T> GetAllEntities<T>() where T : class
        {
            using (var dbContextScope = DbContextScopeFactory.CreateReadOnly())
            {
                var dbContext = dbContextScope.DbContexts.Get<EntityManagerDbContext>();
                var results = dbContext.Set<T>().ToList();

                return results;
            }
        }
    }
}