using System;
using System.Collections.Generic;
using System.Linq;
using EF.Implementation;
using EntityManager.DatabaseContexts;
using EntityManager.Domain.CodeFirst;
using EntityManager.Domain.Services;

namespace EntityManager.Services
{
    public interface IClientQueryService
    {
        IEnumerable<Client> GetClientSpecificEntities();
        Client GetClientById(Guid id);
    }

    public class ClientQueryService : IClientQueryService
    {
        private readonly DbContextScopeFactory _dbContextScopeFactory;
        public static readonly AzureWriter AuditLog = new AzureWriter();

        public ClientQueryService(
            DbContextScopeFactory dbContextScopeFactory)
        {
            _dbContextScopeFactory = dbContextScopeFactory;
        }

        public IEnumerable<Client> GetClientSpecificEntities()
        {
            //do some sort of authorization here

            using (var dbContextScope = _dbContextScopeFactory.CreateReadOnly())
            {
                var dbContext = dbContextScope.DbContexts.Get<EntityManagerDbContext>();
                var results = dbContext.Set<Client>().Include("Subgroup").OrderBy(x=>x.Name).ToList();
                return results;
            }
        }

        public Client GetClientById(Guid id)
        {
            using (var dbContextScope = _dbContextScopeFactory.CreateReadOnly())
            {
                var dbContext = dbContextScope.DbContexts.Get<EntityManagerDbContext>();

                var result = dbContext.Set<Client>().Include("Subgroup").First(x => x.Id == id);

                //AuditLog.Audit(typeof(T).ToString());

                return result;
            }
        }
    }
}