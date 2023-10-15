using EPS.Utils.Repository.Audit;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EPS.Data.Entities
{
    public partial class Role : ICascadeDelete
    {
        public Role()
        {
        }
        public int Id { get; set; }
        [Required]
        [StringLength(250)]
        public string Name { get; set; }
        [StringLength(2000)]
        public string Description { get; set; }
        //public int? UnitId { get; set; }

        //[ForeignKey("UnitId")]
        //[InverseProperty("Roles")]
        //public virtual Unit Unit { get; set; }
        [InverseProperty("Role")]
        public virtual ICollection<GroupRolePermission> GroupRolePermissions { get; set; }
        public void OnDelete()
        {
        }
    }
}
