using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EntityManager.Domain.CodeFirst
{
    public class Group
    {
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        public virtual ICollection<SubGroup> SubGroups { get; set; } 

        public DateTime CreatedOn { get; set; }
        public Guid CreatedBy { get; set; }
    }
}
