using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.WindowsAzure.Storage.Table;

namespace EntityManager.Domain.CodeFirst
{
    public class Subgroup : DomainBase
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Group name is a required field")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Description is a required field")]
        public string Description { get; set; }
        [NotMapped]
        public bool IsChecked { get; set; }
        public virtual Group Group { get; set; }
        public virtual ICollection<Client> Clients { get; set; }
    }
}
