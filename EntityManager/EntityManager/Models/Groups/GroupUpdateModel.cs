using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityManager.Domain.CodeFirst;

namespace EntityManager.Models.Groups
{
    public class GroupUpdateModel
    {
        public Group Group { get; set; }
        public IEnumerable<Guid> SubgroupId { get; set; }
        public IEnumerable<Subgroup> Subgroups { get; set; }
    }
}
