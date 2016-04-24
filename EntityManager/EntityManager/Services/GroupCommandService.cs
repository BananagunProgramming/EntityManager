using System;
using System.Data.Entity.Migrations;
using System.Linq;
using EF.Implementation;
using EntityManager.DatabaseContexts;
using EntityManager.Domain.CodeFirst;
using EntityManager.Domain.Services;
using EntityManager.Models.Groups;

namespace EntityManager.Services
{
    public interface IGroupCommandService
    {
        Guid Create(Group model);
        void UpdateGroup(GroupUpdateModel model);
        void DeleteGroup(Guid id);
        void SetUserGroups(GroupUpdateModel model);
    }

    public class GroupCommandService : IGroupCommandService
    {
        private readonly DbContextScopeFactory _dbContextScopeFactory;
        public static readonly AzureWriter AuditLog = new AzureWriter();

        private readonly IUserService _userService;
        private readonly IGroupQueryService _groupQueryService;
        private readonly ISubgroupQueryService _subgroupQueryService;
        
        public GroupCommandService(
            DbContextScopeFactory dbContextScopeFactory, 
            IUserService userService, 
            IGroupQueryService groupQueryService, ISubgroupQueryService subgroupQueryService)
        {
            _userService = userService;
            _groupQueryService = groupQueryService;
            _subgroupQueryService = subgroupQueryService;
            _dbContextScopeFactory = dbContextScopeFactory;
        }

        public Guid Create(Group model)
        {
            var user = _userService.GetCurrentUser();

            model.Id = Guid.NewGuid();
            model.LastUpdateDate = DateTime.Now;
            model.LastUpdatedBy = user.Identity.Name;
            model.IsDeleted = false;

            using (var dbContextScope = _dbContextScopeFactory.Create())
            {
                var dbContext = dbContextScope.DbContexts.Get<EntityManagerDbContext>();
                dbContext.Set<Group>().Add(model);

                dbContext.SaveChanges();
            }

            AuditLog.Audit($"GroupCommandService - Group: {model.Name} - User: {user.Identity.Name} - {DateTime.Now}");

            return model.Id;
        }

        public void UpdateGroup(GroupUpdateModel input)
        {
            var user = _userService.GetCurrentUser();
            var group = _groupQueryService.GetGroup(input.Group.Id);

            group.Name = input.Group.Name;
            group.Description = input.Group.Description;
            group.LastUpdateDate = DateTime.Now;
            group.LastUpdatedBy = user.Identity.Name;

            using (var dbContextScope = _dbContextScopeFactory.Create())
            {
                var dbContext = dbContextScope.DbContexts.Get<EntityManagerDbContext>();

                dbContext.Set<Group>().AddOrUpdate(group);
                dbContext.SaveChanges();
            }
        }

        public void DeleteGroup(Guid id)
        {
            using (var dbContextScope = _dbContextScopeFactory.Create())
            {
                var dbContext = dbContextScope.DbContexts.Get<EntityManagerDbContext>();
                var entity = dbContext.Set<Group>().Find(id);
                dbContext.Set<Group>().Remove(entity);

                dbContext.SaveChanges();
            }
        }

        public void SetUserGroups(GroupUpdateModel model)
        {
            using (var dbContextScope = _dbContextScopeFactory.Create())
            {
                var dbContext = dbContextScope.DbContexts.Get<EntityManagerDbContext>();

                var group = _groupQueryService.GetGroup(model.Group.Id);
                var subgroups = _subgroupQueryService.GetAllSubgroups().Where(x => model.SubgroupId.Contains(x.Id));

                group.SubGroups.Clear();

                if (model.SubgroupId != null)
                {
                    foreach (var subgroup in subgroups)
                    {
                        group.SubGroups.Add(subgroup);
                    }
                }
                dbContext.SaveChanges();
            }
        }
    }
}
