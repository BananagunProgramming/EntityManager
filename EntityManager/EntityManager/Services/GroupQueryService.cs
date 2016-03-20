using System;
using System.Collections.Generic;
using System.Linq;
using EF.Implementation;
using EntityManager.Domain.CodeFirst;

namespace EntityManager.Services
{
    public interface IGroupQueryService
    {
        IEnumerable<Group> GetAllGroups();
        Group GetGroupById(Guid id);
    }

    public class GroupQueryService : ServiceQueryBase, IGroupQueryService
    {
        public GroupQueryService(DbContextScopeFactory dbContextScopeFactory) : base(dbContextScopeFactory){}

        public IEnumerable<Group> GetAllGroups()
        {
            return GetAllEntities<Group>().Where(x => x.IsDeleted == false).OrderBy(x => x.Name);
        }

        public Group GetGroupById(Guid id)
        {
            //authorizaton
            return GetEntity<Group>(id);
        }
    }
}
