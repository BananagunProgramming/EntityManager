using System;
using System.Collections.Generic;
using System.Linq;
using EF.Implementation;
using EntityManager.Abstract;
using EntityManager.Domain.CodeFirst;

namespace EntityManager.Services
{
    public interface IGroupQueryService : IServiceQueryBase
    {
        IEnumerable<Group> GetAllGroups();
    }

    public class GroupQueryService : ServiceQueryBase, IGroupQueryService
    {
        public GroupQueryService(DbContextScopeFactory dbContextScopeFactory) : base(dbContextScopeFactory)
        {
        }

        public IEnumerable<Group> GetAllGroups()
        {
            return GetAllEntities<Group>().Where(x => x.IsDeleted == false);
        }
    }
}
