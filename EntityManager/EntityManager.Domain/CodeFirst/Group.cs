using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EntityManager.Domain.CodeFirst
{
    public class Group : DomainBase
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Group name is a required field")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Description is a required field")]
        public string Description { get; set; }

        public virtual ICollection<Subgroup> SubGroups { get; set; }
    }
}
