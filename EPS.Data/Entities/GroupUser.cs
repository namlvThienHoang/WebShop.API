using EPS.Utils.Repository.Audit;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EPS.Data.Entities
{
    public partial class GroupUser
    {
        public GroupUser()
        {
        }
        public int Id { get; set; }
        public int GroupId { get; set; }
        [ForeignKey("GroupId")]
        [InverseProperty("GroupUsers")]
        public virtual Group Group { get; set; }        
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        [InverseProperty("GroupUsers")]
        public virtual User User { get; set; }
    }
}
