using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EntityManager.Domain.CodeFirst
{
    public class Group : DomainBase
    {
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual ICollection<SubGroup> SubGroups { get; set; }
    }
}
