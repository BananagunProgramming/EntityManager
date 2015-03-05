using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using EF.Implementation;
using EntityManager.Abstract;
using EntityManager.DatabaseContexts;

namespace EntityManager.Services
{
    public class ServiceCommandBase : IServiceCommandBase
    {
       private readonly DbContextScopeFactory _dbContextScopeFactory;

        public ServiceCommandBase(DbContextScopeFactory dbContextScopeFactory)
        {
            _dbContextScopeFactory = dbContextScopeFactory;
        }

        public void UpdateEntity<T>(T input) where T : class
        {
            using (var dbContextScope = _dbContextScopeFactory.Create())
            {
                var dbContext = dbContextScope.DbContexts.Get<EntityManagerDbContext>();
                dbContext.Set<T>().AddOrUpdate(input);

                dbContext.SaveChanges();
            }
        }

        public T GetEntity<T>(Guid id) where T : class
        {
            using (var dbContextScope = _dbContextScopeFactory.CreateReadOnly())
            {
                var dbContext = dbContextScope.DbContexts.Get<EntityManagerDbContext>();

                var result = dbContext.Set<T>().Find(id);

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

        public void DeleteEntity<T>(Guid id) where T : class
        {
            using (var dbContextScope = _dbContextScopeFactory.Create())
            {
                var dbContext = dbContextScope.DbContexts.Get<EntityManagerDbContext>();
                var entity = dbContext.Set<T>().Find(id);
                dbContext.Set<T>().Remove(entity);

                dbContext.SaveChanges();
            }
        }

        public void CreateEntity<T>(T input) where T : class
        {
            using (var dbContextScope = _dbContextScopeFactory.Create())
            {
                var dbContext = dbContextScope.DbContexts.Get<EntityManagerDbContext>();
                dbContext.Set<T>().Add(input);

                dbContext.SaveChanges();
            }
        }
    }
}