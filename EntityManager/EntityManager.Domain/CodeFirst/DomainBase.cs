using System;

namespace EntityManager.Domain.CodeFirst
{
    public abstract class DomainBase
    {
        public bool IsDeleted { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
    }
}
