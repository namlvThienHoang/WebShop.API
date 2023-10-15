using EPS.Utils.Repository.Audit;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EPS.Data.Entities
{
    //public partial class User : IdentityUser<int>, IDeleteInfo<User, int>, ICascadeDelete
    public partial class User : IdentityUser<int>, IDeleteInfo<User, int>, ICascadeDelete
    {
        public User()
        {

        }

        [StringLength(250)]
        public string FullName { get; set; }
        public bool IsAdministrator { get; set; }
        public int Status { get; set; }        
        public DateTime? DeletedDate { get; set; }
        public int? DeletedUserId { get; set; }
        public virtual User DeletedUser { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        [InverseProperty("User")]
        public virtual ICollection<GroupUser> GroupUsers { get; set; }
        [InverseProperty("User")]
        public virtual ICollection<UserDetail> UserDetails { get; set; }
        //[InverseProperty("User")]
        //public virtual ICollection<FileUpload> FileUploads { get; set; }
        //[InverseProperty("User")]
        //public virtual ICollection<Image> Images { get; set; }

        public void OnDelete()
        {

        }

        public enum StatusUser
        {
            [Description("Deactive")]
            Deactive = 1,
            [Description("Active")]
            Active = 2,
        }
    }
}
