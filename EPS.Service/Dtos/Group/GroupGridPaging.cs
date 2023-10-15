using EPS.Data.Entities;
using EPS.Service.Dtos.Group;
using EPS.Service.Helpers;
using EPS.Utils.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EPS.Service.Dtos.Group
{
    public class GroupGridPaging : PagingParams<GroupGridDto>
    {
        public string FilterText { get; set; }

        public override List<Expression<Func<GroupGridDto, bool>>> GetPredicates()
        {
            var predicates = base.GetPredicates();

            if (!string.IsNullOrEmpty(FilterText))
            {
                predicates.Add(x => x.Title.Contains(FilterText)|| x.Code.Contains(FilterText));
            }            
            return predicates;
        }
    }
}
