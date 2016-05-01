
using System;
using System.Collections.Generic;
using EntityManager.Domain.CodeFirst;

namespace EntityManager.Models.Client
{
    public class ClientManageModel: Domain.CodeFirst.Client
    {
        public IEnumerable<Subgroup> Subgroups { get; set; }
    }
}
