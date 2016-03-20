using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EF.Implementation;
using EntityManager.Domain.CodeFirst;

namespace EntityManager.Services
{
    public interface ISubgroupQueryService
    {
        IEnumerable<Subgroup> GetAllSubgroups();
        Subgroup GetSubgroupById(Guid id);
    }

    public class SubgroupQueryService : ServiceQueryBase, ISubgroupQueryService
    {
        public SubgroupQueryService(DbContextScopeFactory dbContextScopeFactory) : base(dbContextScopeFactory){}

        public IEnumerable<Subgroup> GetAllSubgroups()
        {
            //authorization check
            return GetAllEntities<Subgroup>().Where(x => x.IsDeleted == false).OrderBy(x => x.Name);
        }

        public Subgroup GetSubgroupById(Guid id)
        {
            //authorization check
            return GetEntity<Subgroup>(id);
        }
    }
}
