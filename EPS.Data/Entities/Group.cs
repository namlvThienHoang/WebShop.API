using EPS.Utils.Repository.Audit;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EPS.Data.Entities
{
    public partial class Group
    {
        public Group()
        {
        }
        public int Id { get; set; }
        [Required]
        [StringLength(250)]
        public string Title { get; set; }
        [Required]
        [StringLength(10)]
        public string Code { get; set; }
        [InverseProperty("Group")]
        public virtual ICollection<GroupRolePermission> GroupRolePermissions { get; set; }
        [InverseProperty("Group")]
        public virtual ICollection<GroupUser> GroupUsers { get; set; }
    }
}
