using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EF.Implementation;
using EntityManager.DatabaseContexts;
using EntityManager.Domain.CodeFirst;
using EntityManager.Domain.Services;

namespace EntityManager.Services
{
    public interface ISubgroupQueryService
    {
        IEnumerable<Subgroup> GetAllSubgroups();
        Subgroup GetSubgroupById(Guid id);
        List<Subgroup> MarkSelectedSubgroups(Group group);
    }

    public class SubgroupQueryService : ISubgroupQueryService
    {
        private readonly DbContextScopeFactory _dbContextScopeFactory;
        public static readonly AzureWriter AuditLog = new AzureWriter();

        public SubgroupQueryService(
            DbContextScopeFactory dbContextScopeFactory)
        {
            _dbContextScopeFactory = dbContextScopeFactory;
        }

        public IEnumerable<Subgroup> GetAllSubgroups()
        {
            //authorization check
            using (var dbContextScope = _dbContextScopeFactory.CreateReadOnly())
            {
                var dbContext = dbContextScope.DbContexts.Get<EntityManagerDbContext>();

                var results = dbContext.Set<Subgroup>().ToList();

                return results;
            }
        }

        public Subgroup GetSubgroupById(Guid id)
        {
            //authorization check
            using (var dbContextScope = _dbContextScopeFactory.CreateReadOnly())
            {
                var dbContext = dbContextScope.DbContexts.Get<EntityManagerDbContext>();

                var result = dbContext.Set<Subgroup>().First(x => x.Id == id);

                //AuditLog.Audit(typeof(T).ToString());

                return result;
            }
        }

        public List<Subgroup> MarkSelectedSubgroups(Group group)
        {
            var subgroups = GetAllSubgroups().ToList();
            foreach (var sg in subgroups)
            {
                foreach (var gsg in group.SubGroups)
                {
                    if (sg.Id == gsg.Id)
                    {
                        sg.IsChecked = true;
                    }
                }
            }

            return subgroups;
        }
    }
}
