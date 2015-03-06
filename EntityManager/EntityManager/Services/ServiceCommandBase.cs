using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using EF.Implementation;
using EntityManager.Abstract;
using EntityManager.DatabaseContexts;

namespace EntityManager.Services
{
    public class ServiceCommandBase : ServiceQueryBase, IServiceCommandBase
    {
       private readonly DbContextScopeFactory _dbContextScopeFactory;

       public ServiceCommandBase(DbContextScopeFactory dbContextScopeFactory)
           : base(dbContextScopeFactory)
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