using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EntityManager.Domain.CodeFirst
{
    public class Subgroup : DomainBase
    {
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        public virtual Group Group { get; set; }

        public virtual ICollection<Client> Clients { get; set; }
    }
}
