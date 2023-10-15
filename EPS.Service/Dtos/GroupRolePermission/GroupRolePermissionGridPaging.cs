using EPS.Data.Entities;
using EPS.Service.Dtos.GroupRolePermission;
using EPS.Service.Helpers;
using EPS.Utils.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EPS.Service.Dtos.GroupRolePermission
{
    public class GroupRolePermissionGridPaging : PagingParams<GroupRolePermissionGridDto>
    {
        public int GroupId { get; set; }
        public int RoleId { get; set; }
        public int PermissionId { get; set; }
        public string PermissionCode { get; set; }

        public override List<Expression<Func<GroupRolePermissionGridDto, bool>>> GetPredicates()
        {
            var predicates = base.GetPredicates();
            if (GroupId > 0)
            {
                predicates.Add(x => x.GroupId.Equals(GroupId));
            }
            if (RoleId > 0)
            {
                predicates.Add(x => x.RoleId.Equals(RoleId));
            }
            if (PermissionId > 0)
            {
                predicates.Add(x => x.PermissionId.Equals(PermissionId));
            }
            if(!string.IsNullOrEmpty(PermissionCode))
            {
                predicates.Add(x => x.PermissionCode.Equals(PermissionCode));
            }    
            return predicates;
        }
    }
}
