using EPS.Data.Entities;
using EPS.Service.Dtos.Permission;
using EPS.Service.Helpers;
using EPS.Utils.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EPS.Service.Dtos.Permission
{
    public class PermissionGridPaging : PagingParams<PermissionGridDto>
    {
        public string FilterText { get; set; }
        public string Code { get; set; }
        public override List<Expression<Func<PermissionGridDto, bool>>> GetPredicates()
        {
            var predicates = base.GetPredicates();

            if (!string.IsNullOrEmpty(FilterText))
            {
                predicates.Add(x => x.Title.Contains(FilterText)|| x.Code.Contains(FilterText));
            }
            if (!string.IsNullOrEmpty(Code))
            {
                predicates.Add(x => x.Code.Equals(Code));
            }

            return predicates;
        }
    }
}
