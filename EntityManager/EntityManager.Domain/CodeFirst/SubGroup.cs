using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EntityManager.Domain.CodeFirst
{
    public class SubGroup
    {
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        public virtual Group Group { get; set; }

        public virtual ICollection<Client> Clients { get; set; }

        public bool IsDeleted { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
    }
}
