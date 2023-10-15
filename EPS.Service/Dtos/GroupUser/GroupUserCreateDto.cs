using EPS.Utils.Repository.Audit;
using System;
using System.Collections.Generic;
using System.Text;

namespace EPS.Service.Dtos.GroupUser
{
    public class GroupUserCreateDto
    {
        public int GroupId { get; set; }
        public int UserId { get; set; }
    }
}
