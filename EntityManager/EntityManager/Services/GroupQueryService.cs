using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using EF.Implementation;
using EntityManager.DatabaseContexts;
using EntityManager.Domain.CodeFirst;
using EntityManager.Domain.Services;
using Microsoft.Ajax.Utilities;

namespace EntityManager.Services
{
    public interface IGroupQueryService
    {
        IEnumerable<Group> GetAllGroups();
        Group GetGroupById(Guid id);
    }

    public class GroupQueryService : IGroupQueryService
    {
        private readonly DbContextScopeFactory _dbContextScopeFactory;
        public static readonly AzureWriter AuditLog = new AzureWriter();

        public GroupQueryService(DbContextScopeFactory dbContextScopeFactory)
        {
            _dbContextScopeFactory = dbContextScopeFactory;
        }

        public IEnumerable<Group> GetAllGroups()
        {
            using (var dbContextScope = _dbContextScopeFactory.CreateReadOnly())
            {
                var dbContext = dbContextScope.DbContexts.Get<EntityManagerDbContext>();

                var results = dbContext.Set<Group>().Include("Subgroups").ToList();

                return results;
            }
        }

        public Group GetGroupById(Guid id)
        {
            //authorizaton
            using (var dbContextScope = _dbContextScopeFactory.CreateReadOnly())
            {
                var dbContext = dbContextScope.DbContexts.Get<EntityManagerDbContext>();

                var result = dbContext.Set<Group>().Where(x => x.Id == id).Include("Subgroups").First();

                //AuditLog.Audit(typeof(T).ToString());

                return result;
            }
        }
    }
}
