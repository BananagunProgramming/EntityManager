using System;
using EF.Implementation;
using EntityManager.Domain.CodeFirst;
using EntityManager.Models.GroupSubgroup;

namespace EntityManager.Services
{
    public class GroupCommandService : ServiceCommandBase, IGroupCommandService
    {
        private readonly IUserService _userService;
        
        public GroupCommandService(DbContextScopeFactory dbContextScopeFactory, 
            IUserService userService) : base(dbContextScopeFactory)
        {
            _userService = userService;
        }

        public Guid Create(Group input)
        {
            var user = _userService.GetCurrentUser();

            input.Id = Guid.NewGuid();
            input.LastUpdateDate = DateTime.Now;
            input.LastUpdatedBy = user.Identity.Name;
            input.IsDeleted = false;

            CreateEntity(input);

            AuditLog.Audit(String.Format("GroupCommandService - Group: {0} - User: {1} - {2}", input.Name, user.Identity.Name, DateTime.Now));

            return input.Id;
        }

        public void UpdateGroup(GroupInputModel input)
        {
            var user = _userService.GetCurrentUser();
            var group = GetEntity<Group>(input.Id);

            group.Name = input.Name;
            group.Description = input.Description;
            group.LastUpdateDate = DateTime.Now;
            group.LastUpdatedBy = user.Identity.Name;

            UpdateEntity(group);
        }
    }

    public interface IGroupCommandService
    {
        Guid Create(Group group);
        void UpdateGroup(GroupInputModel input);
    }
}
