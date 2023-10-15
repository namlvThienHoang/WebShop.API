using EPS.Data.Entities;
using EPS.Service.Dtos.GroupUser;
using EPS.Service.Helpers;
using EPS.Utils.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EPS.Service.Dtos.GroupUser
{
    public class GroupUserGridPaging : PagingParams<GroupUserGridDto>
    {
        public int GroupId { get; set; }
        public int UserId { get; set; }
        public List<int> LstGroupIds { get; set; }

        public override List<Expression<Func<GroupUserGridDto, bool>>> GetPredicates()
        {
            var predicates = base.GetPredicates();

            if (GroupId > 0)
            {
                predicates.Add(x => x.GroupId.Equals(GroupId));
            }
            if (UserId > 0)
            {
                predicates.Add(x => x.UserId.Equals(UserId));
            }
            if (LstGroupIds.Count > 0)
            {
                predicates.Add(x => LstGroupIds.Contains(x.GroupId));
            }
            return predicates;
        }
    }
}
