using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EF.Implementation;
using EntityManager.Domain.CodeFirst;

namespace EntityManager.Services
{
    public interface ISubgroupCommandService
    {
        Guid Create(Subgroup input);
        void UpdateSubgroup(Subgroup input);
        void DeleteSubgroup(Guid id);
    }

    public class SubgroupCommandService : ServiceCommandBase, ISubgroupCommandService
    {
        private readonly IUserService _userService;

        public SubgroupCommandService(DbContextScopeFactory dbContextScopeFactory, IUserService userService) : base(dbContextScopeFactory)
        {
            _userService = userService;
        }

        public Guid Create(Subgroup input)
        {
            var user = _userService.GetCurrentUser();

            input.Id = Guid.NewGuid();
            input.IsDeleted = false;
            input.LastUpdateDate = DateTime.Now;
            input.LastUpdatedBy = user.Identity.Name;

            CreateEntity(input);

            AuditLog.Audit($"SubgroupCommandService - Subgroup: {input.Name} - User: {user.Identity.Name} - {DateTime.Now}");

            return input.Id;
        }

        public void UpdateSubgroup(Subgroup input)
        {
            var user = _userService.GetCurrentUser();
            var subgroup = GetEntity<Subgroup>(input.Id);

            subgroup.Name = input.Name;
            subgroup.Description = input.Description;
            subgroup.LastUpdateDate = DateTime.Now;
            subgroup.LastUpdatedBy = user.Identity.Name;

            UpdateEntity(subgroup);
        }

        public void DeleteSubgroup(Guid id)
        {
            //authorization
            DeleteEntity<Subgroup>(id);
        }
    }
}
