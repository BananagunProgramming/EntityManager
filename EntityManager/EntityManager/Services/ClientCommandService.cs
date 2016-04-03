
using System;
using System.Data.Entity.Migrations;
using EF.Implementation;
using EntityManager.DatabaseContexts;
using EntityManager.Domain.CodeFirst;
using EntityManager.Domain.Services;

namespace EntityManager.Services
{
    public class ClientCommandService : IClientCommandService
    {
        private readonly DbContextScopeFactory _dbContextScopeFactory;
        public static readonly AzureWriter AuditLog = new AzureWriter();
        private readonly IUserService _userService;
        private readonly IClientQueryService _clientQueryService;

        public ClientCommandService(
            DbContextScopeFactory dbContextScopeFactory, 
            IUserService userService, 
            IClientQueryService clientQueryService)
        {
            _dbContextScopeFactory = dbContextScopeFactory;
            _userService = userService;
            _clientQueryService = clientQueryService;
        }

        //put non generic methods here
        public void CreateClient(Client input)
        {
            var user = _userService.GetCurrentUser();

            input.ClientId = Guid.NewGuid();
            input.IsDeleted = false;
            input.LastUpdateDate = DateTime.Now;
            input.LastUpdatedBy = user.Identity.Name;

            using (var dbContextScope = _dbContextScopeFactory.Create())
            {
                var dbContext = dbContextScope.DbContexts.Get<EntityManagerDbContext>();
                dbContext.Set<Client>().Add(input);

                dbContext.SaveChanges();
            }
        }

        public void UpdateClient(Client input)
        {
            var user = _userService.GetCurrentUser();
            var client = _clientQueryService.GetClientById(input.ClientId);

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

            using (var dbContextScope = _dbContextScopeFactory.Create())
            {
                var dbContext = dbContextScope.DbContexts.Get<EntityManagerDbContext>();

                dbContext.Set<Client>().AddOrUpdate(client);
                dbContext.SaveChanges();
            }
        }

        public void DeleteClient(Guid id)
        {
            using (var dbContextScope = _dbContextScopeFactory.Create())
            {
                var dbContext = dbContextScope.DbContexts.Get<EntityManagerDbContext>();
                var entity = dbContext.Set<Client>().Find(id);
                dbContext.Set<Client>().Remove(entity);

                dbContext.SaveChanges();
            }
        }
    }

    public interface IClientCommandService
    {
        void CreateClient(Client input);
        void UpdateClient(Client input);
        void DeleteClient(Guid id);
    }
}