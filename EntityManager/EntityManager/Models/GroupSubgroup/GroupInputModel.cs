using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EntityManager.Models.GroupSubgroup
{
    public class GroupInputModel
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
    }

    public class GroupManageViewModel
    {
        public Guid Id { get; set; }
        public GroupGeneralViewModel General { get; set; }
        public SubgroupViewModel Subgroups { get; set; }
    }

    public class GroupGeneralViewModel
    {
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
    }

    public class SubgroupViewModel
    {
        public Guid Id { get; set; }
        [Required]
        public IEnumerable<Guid> SubgroupIds { get; set; }
    }
}
