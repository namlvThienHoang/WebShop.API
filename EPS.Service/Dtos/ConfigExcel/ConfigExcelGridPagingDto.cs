using EPS.Service.Dtos.ConfigExcel;
using EPS.Utils.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
namespace EPS.Service.Dtos.ConfigExcel
{
    public class ConfigExcelGridPaging : PagingParams<ConfigExcelGridDto>
    {
        public string FilterText { get; set; }
        public string Type { get; set; }
        public int? coSoGiaoDucId { get; set; }
        public string startdate { get; set; }
        public string enddate { get; set; }
        public int startyear { get; set; }
        public int endyear { get; set; }
        public int loaiTruong { get; set; }
        public int? CoSoGiaoDuc_Id { get; set; }
        //public int? ToChucKiemDinhId { get; set; }
        
        public string startdateGCNGT { get; set; }
        public string enddateGCNGT { get; set; }
        public string startdateGCNNC { get; set; }
        public string enddateGCNNC { get; set; }
        public string startdateDCN { get; set; }
        public string enddateDCN { get; set; }

        public int loai { get; set; }
        public int? TrinhDo_Id { get; set; }
        public int? ToChucBDKDV_Id { get; set; }
        public string startdateDGN { get; set; }
        public string enddateDGN { get; set; }
        public string startdateTDG { get; set; }
        public string enddateTDG { get; set; }
        public int ToChucKiemDinhID { get; set; }
        public int PhongBanID { get; set; }
        public int IdDVCT { get; set; }
        public int hocViId { get; set; }
        public int hocHamId { get; set; }
        //rendercode{5}
        public override List<Expression<Func<ConfigExcelGridDto, bool>>> GetPredicates()
        {
            var predicates = base.GetPredicates();
            if (!string.IsNullOrEmpty(FilterText))
            {
                
                predicates.Add(x => (x.TieuDe.Contains(FilterText.Trim()) || x.DoiTuong.Contains(FilterText.Trim())));
            }
            if (!string.IsNullOrEmpty(Type))
            {
                predicates.Add(x => (x.DoiTuong.Equals(Type.Trim())));
            }
            if (loai > 0)
            {
                predicates.Add(x => (x.Loai == loai));
            }
            //rendercode{6}
            return predicates;
        }
    }
}
