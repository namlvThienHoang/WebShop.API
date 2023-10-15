using AspNetCore.Reporting;
using EPS.ReportDatasuorces.Model;
using System.Reflection;
using System.Text;

namespace EPS.ReportWebApi.Services
{
    public partial class ReportService : IReportService
    {
        public byte[] GenerateReportAsync(string reportType)
        {
            string fileDirPath = Assembly.GetExecutingAssembly().Location.Replace("EPS.ReportWebApi.dll", string.Empty);
            string rdlcFilePath = string.Format("{0}ReportFiles\\BaoCaoTuyBien.rdlc", fileDirPath);

            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            Encoding.GetEncoding("utf-8");

            var report = new LocalReport(rdlcFilePath);

            // prepare data for report
            var dataList = new List<BaoCaoTuyBienModel>();

            var data1 = new BaoCaoTuyBienModel()
            {
                MA_TAI_SAN = "Mã tài sản 1",
                TEN_TAI_SAN = "Tên tài sản 1",
                NHOM_TAI_SAN = "Nhóm tài sản 1",
                NGAY_SU_DUNG = DateTime.Now,
                TONG_NGUYEN_GIA = 1000000000,
                NGUON_NS = 600000000,
                NGUON_ODA = 100000000,
                NGUON_SU_NGHIEP = 100000000,
                NGUON_KHAC = 100000000,
                GTCL = 900000000,
                LUY_KE_HAO_MON = 100000000,
                LUY_KE_KHAU_HAO = 100000000,
                HAO_MON_NAM = 100000000,
                KHAU_HAO_NAM = 100000000,
                NGAY_NHAP = DateTime.Now,
                NGAY_TANG = DateTime.Now,
                BO_PHAN_SU_DUNG = "Bộ phận sử dụng tài sản 1",
                NGUOI_SU_DUNG = "Người sử dụng tài sản 1",
                DIEN_TICH = 1000,
                DIEN_TICH_XD = 2000,
                TONG_DIEN_TICH_SAN_XD = 2000,
                SO_TANG = 10,
                TONG_DIEN_TICH_SD = 2000,
                TIEN_TICH_THE_TICH_CHIEU_DAI = 500,
                DT_TSLV = 100,
                DT_HDSN = 100,
                DT_NHA_O = 100,
                DT_CHO_THUE = 2000,
                DT_BO_TRONG = 100,
                DT_BI_LAN_CHIEM = 2000,
                DT_KHAC = 2000,
                DT_SD_CTCM = 100,
                KHO_LUONG_THUC = 100,
                KHO_VAT_TU = 2000,
                KHO_KHAC = 2000,
                SU_DUNG_KHAC = 100,
                BIEN_KIEM_SOAT = "30A11505",
                NHAN_HIEU = "Nhãn hiệu tài sản 1",
                LOAI_VO_TAU = "Loại vỏ tài sản 1",
                NGUON_GOC_TANG = "Nguồn gốc tài sản 1",
                LY_DO = "Lý do tài sản 1",
                HINH_THUC_GIAM = "Hình thức giảm tài sản 1",
                SO_TIEN_THU_DUOC = 200000000,
                CHI_PHI_THANH_LY = 50000000,
                GIA_TRI_THU_HOI = 150000000,
                NOP_NGAN_SACH_NHA_NUOC = 150000000,
                DE_LAI_DON_VI = 50000000,
                DIA_CHI = "Địa chỉ tài sản 1",
                MA_LO = "Mã lô tài sản 1",
                KY_HIEU = "Ký hiệu tài sản 1",
                SO_SERRIAL = "Số seri tài sản 1",
                MA_TAI_SAN_CU = "Mã tài sản cũ 1"
            };
            var data2 = new BaoCaoTuyBienModel()
            {
                MA_TAI_SAN = "Mã tài sản 1",
                TEN_TAI_SAN = "Tên tài sản 1",
                NHOM_TAI_SAN = "Nhóm tài sản 1",
                NGAY_SU_DUNG = DateTime.Now,
                TONG_NGUYEN_GIA = 1000000000,
                NGUON_NS = 600000000,
                NGUON_ODA = 100000000,
                NGUON_SU_NGHIEP = 100000000,
                NGUON_KHAC = 100000000,
                GTCL = 900000000,
                LUY_KE_HAO_MON = 100000000,
                LUY_KE_KHAU_HAO = 100000000,
                HAO_MON_NAM = 100000000,
                KHAU_HAO_NAM = 100000000,
                NGAY_NHAP = DateTime.Now,
                NGAY_TANG = DateTime.Now,
                BO_PHAN_SU_DUNG = "Bộ phận sử dụng tài sản 1",
                NGUOI_SU_DUNG = "Người sử dụng tài sản 1",
                DIEN_TICH = 1000,
                DIEN_TICH_XD = 2000,
                TONG_DIEN_TICH_SAN_XD = 2000,
                SO_TANG = 10,
                TONG_DIEN_TICH_SD = 2000,
                TIEN_TICH_THE_TICH_CHIEU_DAI = 500,
                DT_TSLV = 100,
                DT_HDSN = 100,
                DT_NHA_O = 100,
                DT_CHO_THUE = 2000,
                DT_BO_TRONG = 100,
                DT_BI_LAN_CHIEM = 2000,
                DT_KHAC = 2000,
                DT_SD_CTCM = 100,
                KHO_LUONG_THUC = 100,
                KHO_VAT_TU = 2000,
                KHO_KHAC = 2000,
                SU_DUNG_KHAC = 100,
                BIEN_KIEM_SOAT = "30A11505",
                NHAN_HIEU = "Nhãn hiệu tài sản 1",
                LOAI_VO_TAU = "Loại vỏ tài sản 1",
                NGUON_GOC_TANG = "Nguồn gốc tài sản 1",
                LY_DO = "Lý do tài sản 1",
                HINH_THUC_GIAM = "Hình thức giảm tài sản 1",
                SO_TIEN_THU_DUOC = 200000000,
                CHI_PHI_THANH_LY = 50000000,
                GIA_TRI_THU_HOI = 150000000,
                NOP_NGAN_SACH_NHA_NUOC = 150000000,
                DE_LAI_DON_VI = 50000000,
                DIA_CHI = "Địa chỉ tài sản 1",
                MA_LO = "Mã lô tài sản 1",
                KY_HIEU = "Ký hiệu tài sản 1",
                SO_SERRIAL = "Số seri tài sản 1",
                MA_TAI_SAN_CU = "Mã tài sản cũ 1"
            };

            dataList.Add(data1);
            dataList.Add(data2);

            report.AddDataSource("BaoCaoTuyBienData", dataList);

            var parameters = new Dictionary<string, string>()
            {
                { "TEN_DON_VI", "Tên đơn vị" },
                { "TEN_BAO_CAO", "Báo cáo tùy biến" },
                { "TEN_DONVI_BICAO", "Tên đơn vị báo cáo" },
                { "THOI_GIAN_BAO_CAO", "2023" },
                { "DVT_DT", "m2" },
                { "DVT_TIEN", "đồng" },
                { "DVT_SL", "Cái" }
            };
            var result = report.Execute(GetRenderType(reportType), 1, parameters);

            return result.MainStream;
        }

        private RenderType GetRenderType(string reportType)
        {
            var renderType = reportType.ToUpper() switch
            {
                "XLS" => RenderType.Excel,
                "XLSX" => RenderType.ExcelOpenXml,
                "WORD" => RenderType.Word,
                "WORDX" => RenderType.WordOpenXml,
                _ => RenderType.Pdf,
            };
            return renderType;
        }
    }
}
