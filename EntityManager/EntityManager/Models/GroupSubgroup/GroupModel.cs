using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EntityManager.Models.GroupSubgroup
{
    public class GroupModel
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }

        public SubgroupModel Subgroups { get; set; }
    }

    public class SubgroupModel
    {
        public Guid Id { get; set; }
        [Required]
        public IEnumerable<Guid> SubgroupIds { get; set; }
    }
}
