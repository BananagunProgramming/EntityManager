
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
        public void CreateClient(Client input)
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

        public void UpdateClient(Client input)
        {
            var user = _userService.GetCurrentUser();
            var client = GetEntity<Client>(input.ClientId);

            client.Name = input.Name;
            client.EntityCode = input.EntityCode;
            client.YearIncorporated = input.YearIncorporated;
            client.TaxId = input.TaxId;
            client.Phone = input.Phone;
            client.Fax = input.Fax;
            client.Email = input.Email;
            client.Website = input.Website;
            client.Schedule = input.Schedule;
            client.YearEndDate = input.YearEndDate;
            client.FiscalYearEndDate = input.FiscalYearEndDate;
            client.Managed = input.Managed;

            client.LastUpdateDate = DateTime.Now;
            client.LastUpdatedBy = user.Identity.Name;

            UpdateEntity(client);
        }

        public void DeleteClient(Guid id)
        {
            DeleteEntity<Client>(id);
        }
    }

    public interface IClientCommandService : IServiceCommandBase
    {
        void CreateClient(Client input);
        void UpdateClient(Client input);
        void DeleteClient(Guid id);
    }
}