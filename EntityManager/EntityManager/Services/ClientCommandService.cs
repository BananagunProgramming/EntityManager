
using System;
using EF.Implementation;
using EntityManager.Abstract;
using EntityManager.Domain.CodeFirst;

namespace EntityManager.Services
{
    public class ClientCommandService : ServiceCommandBase, IClientCommandService
    {
        private readonly IUserService _userService;

        public ClientCommandService(DbContextScopeFactory dbContextScopeFactory, 
            IUserService userService) : base(dbContextScopeFactory)
        {
            _userService = userService;
        }

        //put non generic methods here
        public void Create(Client input)
        {
            var user = _userService.GetCurrentUser();

            input.ClientId = Guid.NewGuid();
            input.CreatedOn = DateTime.Now;
            input.CreatedBy = user.Identity.Name;
            input.IsDeleted = false;
            input.LastUpdateDate = DateTime.Now;
            input.LastUpdatedBy = user.Identity.Name;

            CreateEntity(input);
        }
    }

    public interface IClientCommandService : IServiceCommandBase
    {
        void Create(Client client);
    }
}