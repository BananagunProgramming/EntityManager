using System;
using System.Data.Entity.Migrations;
using EF.Implementation;
using EntityManager.DatabaseContexts;
using EntityManager.Domain.CodeFirst;
using EntityManager.Domain.Services;

namespace EntityManager.Services
{
    public interface IGroupCommandService
    {
        Guid Create(Group group);
        void UpdateGroup(Group input);
        void DeleteGroup(Guid id);
    }

    public class GroupCommandService : IGroupCommandService
    {
        private readonly DbContextScopeFactory _dbContextScopeFactory;
        public static readonly AzureWriter AuditLog = new AzureWriter();

        private readonly IUserService _userService;
        private readonly IGroupQueryService _groupQueryService;
        
        public GroupCommandService(
            DbContextScopeFactory dbContextScopeFactory, 
            IUserService userService, 
            IGroupQueryService groupQueryService)
        {
            _userService = userService;
            _groupQueryService = groupQueryService;
            _dbContextScopeFactory = dbContextScopeFactory;
        }

        public Guid Create(Group input)
        {
            var user = _userService.GetCurrentUser();

            input.Id = Guid.NewGuid();
            input.LastUpdateDate = DateTime.Now;
            input.LastUpdatedBy = user.Identity.Name;
            input.IsDeleted = false;

            using (var dbContextScope = _dbContextScopeFactory.Create())
            {
                var dbContext = dbContextScope.DbContexts.Get<EntityManagerDbContext>();
                dbContext.Set<Group>().Add(input);

                dbContext.SaveChanges();
            }

            AuditLog.Audit($"GroupCommandService - Group: {input.Name} - User: {user.Identity.Name} - {DateTime.Now}");

            return input.Id;
        }

        public void UpdateGroup(Group input)
        {
            var user = _userService.GetCurrentUser();
            var group = _groupQueryService.GetGroupById(input.Id);

            group.Name = input.Name;
            group.Description = input.Description;
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
    }
}
