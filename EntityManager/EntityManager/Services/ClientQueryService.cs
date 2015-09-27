using System;
using System.Collections.Generic;
using System.Linq;
using EF.Implementation;
using EntityManager.Domain.CodeFirst;

namespace EntityManager.Services
{
    public interface IClientQueryService
    {
        IEnumerable<Client> GetClientSpecificEntities();
        Client GetClientById(Guid id);
    }

    public class ClientQueryService : ServiceQueryBase, IClientQueryService
    {
        private readonly IUserService _userService;

        public ClientQueryService(DbContextScopeFactory dbContextScopeFactory, 
            IUserService userService)
            : base(dbContextScopeFactory)
        {
            _userService = userService;
        }

        public IEnumerable<Client> GetClientSpecificEntities()
        {
            //do some sort of authorization here
            var entityCode = _userService.GetUserProperty("EntityCode");
            
            return GetAllEntities<Client>().Where(x => x.EntityCode == entityCode && x.IsDeleted == false);
        }

        public Client GetClientById(Guid id)
        {
            return GetEntity<Client>(id);
        }
    }
}