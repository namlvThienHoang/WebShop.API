
using EPS.Service.Dtos.TTTINTUCHVHANHVI;
using EPS.Utils.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EPS.API.Models
{
    public class TTTINTUCHVHANHVIGridPaging : PagingParams<TTTINTUCHVHANHVIGridDto>
    {
        public string FilterText { get; set; }
        public int idtkTaiKhoan { get; set; }
        public int idBaiViet { get; set; }
        public int HanhDong { get; set; }
        public int LoaiDoiTuong { get; set; }
        
        public override List<Expression<Func<TTTINTUCHVHANHVIGridDto, bool>>> GetPredicates()
        {
            var predicates = base.GetPredicates();
            if (idtkTaiKhoan>0)
            {
                predicates.Add(x => x.ID_TACNHAN == idtkTaiKhoan);
            }
            if(idBaiViet > 0)
            {
                predicates.Add(x => x.ID_DOITUONG == idtkTaiKhoan);
            }
            if (HanhDong > 0)
            {
                predicates.Add(x => x.HANHDONG == HanhDong);
            }         
            if (LoaiDoiTuong > 0)
            {
                predicates.Add(x => x.LOAIDOITUONG == LoaiDoiTuong);
            }
            return predicates;
        }
    }
}
