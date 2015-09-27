﻿using System;
using System.Data.Entity.Migrations;
using EF.Implementation;
using EntityManager.DatabaseContexts;

namespace EntityManager.Services
{
    public abstract class ServiceCommandBase : ServiceQueryBase
    {
        protected ServiceCommandBase(DbContextScopeFactory dbContextScopeFactory)
            : base(dbContextScopeFactory)
        {
            DbContextScopeFactory = dbContextScopeFactory;
        }

        public void UpdateEntity<T>(T input) where T : class
        {
            using (var dbContextScope = DbContextScopeFactory.Create())
            {
                var dbContext = dbContextScope.DbContexts.Get<EntityManagerDbContext>();

                dbContext.Set<T>().AddOrUpdate(input);
                dbContext.SaveChanges();
            }
        }

        public void MotherFucker(Guid id)
        {
            throw new NotImplementedException();
        }

        public void DeleteEntity<T>(Guid id) where T : class
        {
            using (var dbContextScope = DbContextScopeFactory.Create())
            {
                var dbContext = dbContextScope.DbContexts.Get<EntityManagerDbContext>();
                var entity = dbContext.Set<T>().Find(id);
                dbContext.Set<T>().Remove(entity);

                dbContext.SaveChanges();
            }
        }

        public void CreateEntity<T>(T input) where T : class
        {
            using (var dbContextScope = DbContextScopeFactory.Create())
            {
                var dbContext = dbContextScope.DbContexts.Get<EntityManagerDbContext>();
                dbContext.Set<T>().Add(input);

                dbContext.SaveChanges();
            }
        }
    }
}