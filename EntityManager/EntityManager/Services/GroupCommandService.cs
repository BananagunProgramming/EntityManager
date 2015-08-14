using System;
using System.Web.Mvc;
using EF.Implementation;
using EntityManager.Abstract;
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

        public void Create(Group input)
        {
            var user = _userService.GetCurrentUser();

            input.Id = Guid.NewGuid();
            input.CreatedOn = DateTime.Now;
            input.CreatedBy = user.Identity.Name;
            input.IsDeleted = false;

            CreateEntity(input);

            AuditLog.Audit(String.Format("GroupCommandService - Group: {0} - User: {1} - {2}", input.Name, user.Identity.Name, DateTime.Now));
        }

        public void UpdateGroup(GroupInputModel input)
        {
            var user = _userService.GetCurrentUser();
            var group = GetEntity<Group>(input.Id);

            group.Name = input.Name;
            group.Description = input.Description.ToString();
            group.LastUpdateDate = DateTime.Now;
            group.LastUpdatedBy = user.Identity.Name;

            UpdateEntity(group);
        }
    }

    public interface IGroupCommandService : IServiceCommandBase
    {
        void Create(Group group);
        void UpdateGroup(GroupInputModel input);
    }
}
