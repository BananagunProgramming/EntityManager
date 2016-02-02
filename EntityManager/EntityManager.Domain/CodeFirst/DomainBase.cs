using System;

namespace EntityManager.Domain.CodeFirst
{
    public abstract class DomainBase
    {
        public bool IsDeleted { get; set; }
        public DateTime LastUpdateDate { get; set; }
        public string LastUpdatedBy { get; set; }
}
}
