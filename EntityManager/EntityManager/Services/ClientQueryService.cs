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
        private readonly IUserService _userService;

        public ClientQueryService(
            DbContextScopeFactory dbContextScopeFactory, 
            IUserService userService)
        {
            _dbContextScopeFactory = dbContextScopeFactory;
            _userService = userService;
        }

        public IEnumerable<Client> GetClientSpecificEntities()
        {
            //do some sort of authorization here
            var entityCode = _userService.GetUserProperty("EntityCode");

            using (var dbContextScope = _dbContextScopeFactory.CreateReadOnly())
            {
                var dbContext = dbContextScope.DbContexts.Get<EntityManagerDbContext>();

                var results = dbContext.Set<Client>().ToList();

                return results;
            }
        }

        public Client GetClientById(Guid id)
        {
            using (var dbContextScope = _dbContextScopeFactory.CreateReadOnly())
            {
                var dbContext = dbContextScope.DbContexts.Get<EntityManagerDbContext>();

                var result = dbContext.Set<Client>().First(x => x.ClientId == id);

                //AuditLog.Audit(typeof(T).ToString());

                return result;
            }
        }
    }
}