using EPS.Data.Entities;
using EPS.Service.Dtos.User;
using EPS.Service.Helpers;
using EPS.Utils.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EPS.Service.Dtos.User
{
    public class UserGridPagingDto : PagingParams<UserGridDto>, IUnitTraversal<Data.Entities.User>
    {
        public string Username { get; set; }        
        public string FilterText { get; set; }
        public int GroupId { get; set; }
        public UnitTraversalOption? UnitTraversalOption { get; set; }

        public override List<Expression<Func<UserGridDto, bool>>> GetPredicates()
        {
            var predicates = base.GetPredicates();

            if (!string.IsNullOrEmpty(FilterText))
            {
                predicates.Add(x => x.Username.Contains(FilterText.Trim())|| x.FullName.Contains(FilterText.Trim()));
            }
            if (!string.IsNullOrEmpty(Username))
            {
                predicates.Add(x => x.Username.Contains(Username));
            }           
            predicates.Add(x => x.DeletedUserId == null);
            return predicates;
        }

        public List<Expression<Func<Data.Entities.User, bool>>> GetTraversalPredicates()
        {
            var predicates = new List<Expression<Func<Data.Entities.User, bool>>>();

            //switch (UnitTraversalOption.GetValueOrDefault(Helpers.UnitTraversalOption.IncludeDescendants))
            //{
            //    case Helpers.UnitTraversalOption.IncludeChildren: predicates.Add(x => x.UnitId == UnitId || x.Unit.ParentId == UnitId); break;
            //    //case Helpers.UnitTraversalOption.IncludeDescendants: predicates.Add(x => x.Unit.Ancestors.Any(y => y.UnitAncestorId == UnitId)); break;
            //    default: predicates.Add(x => x.UnitId == UnitId); break;
            //}

            return predicates;
        }

        //public void Traversing(IQueryable<UnitAncestor> unitAncestors, ref IQueryable<Data.Entities.User> query)
        //{
            
        //}
    }
}
