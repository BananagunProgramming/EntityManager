using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EF.Implementation;
using EntityManager.DatabaseContexts;
using EntityManager.Domain.CodeFirst;
using EntityManager.Domain.Services;

namespace EntityManager.Services
{
    public interface ISubgroupCommandService
    {
        Guid Create(Subgroup input);
        void UpdateSubgroup(Subgroup input);
        void DeleteSubgroup(Guid id);
    }

    public class SubgroupCommandService : ISubgroupCommandService
    {
        private readonly DbContextScopeFactory _dbContextScopeFactory;
        public static readonly AzureWriter AuditLog = new AzureWriter();
        private readonly IUserService _userService;
        private readonly ISubgroupQueryService _subgroupQueryService;

        public SubgroupCommandService(
            IUserService userService, 
            DbContextScopeFactory dbContextScopeFactory, 
            ISubgroupQueryService subgroupQueryService)
        {
            _userService = userService;
            _dbContextScopeFactory = dbContextScopeFactory;
            _subgroupQueryService = subgroupQueryService;
        }

        public Guid Create(Subgroup input)
        {
            var user = _userService.GetCurrentUser();

            input.Id = Guid.NewGuid();
            input.IsDeleted = false;
            input.LastUpdateDate = DateTime.Now;
            input.LastUpdatedBy = user.Identity.Name;

            using (var dbContextScope = _dbContextScopeFactory.Create())
            {
                var dbContext = dbContextScope.DbContexts.Get<EntityManagerDbContext>();
                dbContext.Set<Subgroup>().Add(input);

                dbContext.SaveChanges();
            }

            AuditLog.Audit($"SubgroupCommandService - Subgroup: {input.Name} - User: {user.Identity.Name} - {DateTime.Now}");

            return input.Id;
        }

        public void UpdateSubgroup(Subgroup input)
        {
            var user = _userService.GetCurrentUser();
            var subgroup = _subgroupQueryService.GetSubgroupById(input.Id);

            subgroup.Name = input.Name;
            subgroup.Description = input.Description;
            subgroup.LastUpdateDate = DateTime.Now;
            subgroup.LastUpdatedBy = user.Identity.Name;

            using (var dbContextScope = _dbContextScopeFactory.Create())
            {
                var dbContext = dbContextScope.DbContexts.Get<EntityManagerDbContext>();

                dbContext.Set<Subgroup>().AddOrUpdate(subgroup);
                dbContext.SaveChanges();
            }
        }

        public void DeleteSubgroup(Guid id)
        {
            //authorization
            using (var dbContextScope = _dbContextScopeFactory.Create())
            {
                var dbContext = dbContextScope.DbContexts.Get<EntityManagerDbContext>();
                var entity = dbContext.Set<Subgroup>().Find(id);
                dbContext.Set<Subgroup>().Remove(entity);

                dbContext.SaveChanges();
            }
        }
    }
}
