using System;
using System.Collections.Generic;
using System.Linq;
using EF.Implementation;
using EntityManager.Domain.CodeFirst;
using EntityManager.Models.GroupSubgroup;

namespace EntityManager.Services
{
    public interface IGroupQueryService
    {
        IEnumerable<Group> GetAllGroups();
        GroupModel GetGroupModelById(Guid id);
        Group GetGroupById(Guid id);
    }

    public class GroupQueryService : ServiceQueryBase, IGroupQueryService
    {
        public GroupQueryService(DbContextScopeFactory dbContextScopeFactory) : base(dbContextScopeFactory){}

        public IEnumerable<Group> GetAllGroups()
        {
            return GetAllEntities<Group>().Where(x => x.IsDeleted == false).OrderBy(x => x.Name);
        }

        public GroupModel GetGroupModelById(Guid id)
        {
            var model = GetEntity<Group>(id);

            if (model.IsDeleted)
            {
                var exception = new NullReferenceException(String.Format("The group tied to Id {0} has been deleted", id));
                //AuditLog.Error(exception);
                throw exception;
            }

            var result = new GroupModel
            {
                Id = model.Id,
                Name = model.Name,
                Description = model.Description
            };

            return result;
        }

        public Group GetGroupById(Guid id)
        {
            //authorizaton
            return GetEntity<Group>(id);
        }
    }
}
