using EPS.Data.Entities;
using EPS.Utils.Common;
using OfficeOpenXml.Style;
using OfficeOpenXml;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using EPS.Utils.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using EPS.Service.Helpers;
using EPS.Service.Dtos.ConfigExcel;

namespace EPS.API.Helpers
{
    public static class CommonAPI
    {

        public static async Task<objExportResult> Export<T>(List<T> lstObject, string type, EPSBaseService BaseService)
       where T : class
        {
            try
            {
                ConfigExcelGridPaging pagingConfig = new ConfigExcelGridPaging { Type = type, loai = 1 };
                var result = await BaseService.FilterPagedAsync<ConfigExcel, ConfigExcelGridDto>(pagingConfig, pagingConfig.GetPredicates().ToArray());
                List<ConfigExcelGridDto> lstConfig = result.Data;
                if (lstConfig.Count > 0)
                {

                    if (!string.IsNullOrEmpty(lstConfig.FirstOrDefault().CauHinh) && !string.IsNullOrEmpty(lstConfig.FirstOrDefault().CauHinhHeader))
                    {

                        List<CauHinhExport> lstMapCell = new List<CauHinhExport>();


                        lstMapCell = JsonConvert.DeserializeObject<List<CauHinhExport>>(lstConfig.FirstOrDefault().CauHinh);


                        //List<CauHinhExport> lstMapCell = JsonConvert.DeserializeObject<List<CauHinhExport>>(lstConfig.FirstOrDefault().CauHinhHeader);
                        Dictionary<int, string> dctMapCellHeader = JsonConvert.DeserializeObject<Dictionary<int, string>>(lstConfig.FirstOrDefault().CauHinhHeader);
                        string fileName = "";
                        return new objExportResult( exportExcelNew<T>(lstObject, dctMapCellHeader, lstMapCell, lstConfig.FirstOrDefault().TieuDe, lstConfig.FirstOrDefault().RowStart, true));

                    }
                    else
                    {
                        return new objExportResult("Cấu hình không đúng");
                    }
                }
                else
                {
                    return new objExportResult( "Cấu hình không đúng");
                }
            }
            catch (FormatException ex)
            {
                return new objExportResult(ex.Message) ;
            }
        }
        public static MemoryStream exportExcelNew<T>(List<T> lstObject, Dictionary<int, string> dctMapCellHeader, List<CauHinhExport> lstMapCell, string title, int rowStart, bool isSTT = false)
where T : class
        {
            List<ClsExcel> lstValue = new List<ClsExcel>();
            //thêm tiêu đề sheet
            lstValue.Add(new ClsExcel(rowStart, 1, title, ExcelHorizontalAlignment.Center, ExcelVerticalAlignment.Center, false, true, false, true, 14, "Times New Roman", true, rowStart, rowStart, 1, isSTT ? lstMapCell.Count + 1 : lstMapCell.Count));

            //thêm header các cột
            rowStart += 2;
            if (isSTT)
                lstValue.Add(new ClsExcel(rowStart, 1, "STT", ExcelHorizontalAlignment.Center, ExcelVerticalAlignment.Center, false, true, true, true, 12));

            foreach (var item in dctMapCellHeader)
            {
                lstValue.Add(new ClsExcel(rowStart, isSTT ? item.Key + 1 : item.Key, item.Value, ExcelHorizontalAlignment.Center, ExcelVerticalAlignment.Center, false, true, true, true, 12));
            }
            //Thêm giá trị vào các dòng
            rowStart += 1;
            int index = 1;
            foreach (var item in lstObject)
            {
                if (isSTT)
                    lstValue.Add(new ClsExcel(rowStart, 1, index, ExcelHorizontalAlignment.Center, ExcelVerticalAlignment.Center, false, false, true, true, 12));
                foreach (var mapCell in lstMapCell)
                {
                    var valuecell = getCellValue<T>(item, mapCell.value);
                    lstValue.Add(new ClsExcel(rowStart, isSTT ? mapCell.index + 1 : mapCell.index, valuecell.value, GetAlignByName(mapCell.align, valuecell.alignH), ExcelVerticalAlignment.Center, false, false, true, true, 12, "Times New Roman", false, 0, 0, 0, 0, mapCell.width));
                }
                rowStart++;
                index += 1;
            }
            //if (isSTT)
            //    lstValue.Add(new ClsExcel(rowStart, 1, "", ExcelHorizontalAlignment.Center, ExcelVerticalAlignment.Center, false, true, true, true, 12));

            foreach (var item in dctMapCellHeader)
            {
                lstValue.Add(new ClsExcel(rowStart, isSTT ? item.Key + 1 : item.Key, "", ExcelHorizontalAlignment.Center, ExcelVerticalAlignment.Center, false, false, false, false, 12));
            }
            //Thêm value các cột
            MemoryStream stream = new MemoryStream();

            using (var package = new ExcelPackage(stream))
            {
                var sheet = package.Workbook.Worksheets.Add("Sheet 1");
                //đổ dữ liệu vào
                TUtils.SetStyleExcel(ref sheet, lstValue);
                package.Save();
            }
            stream.Position = 0;
            return stream;
        }
        public static CellValue getCellValue<T>(T obj, string field)
where T : class
        {

            CellValue valuer = new CellValue();
            var pi = obj.GetType().GetProperty(field);
            if (pi == null)
                return valuer;
            string propertyType = pi.PropertyType.FullName;
            valuer.value = pi.GetValue(obj);


            switch (propertyType)
            {
                case "System.Nullable`1[[System.Int32, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]":
                case "System.Int32?":
                case
                    "System.Nullable`1[[System.Int32, System.Private.CoreLib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e]]":

                case "System.Int32":

                    valuer.alignH = ExcelHorizontalAlignment.Center;
                    break;
                case "System.Decimal":
                    valuer.alignH = ExcelHorizontalAlignment.Center;
                    break;
                case "System.Double":
                    valuer.alignH = ExcelHorizontalAlignment.Center;
                    break;
                case "System.Nullable`1[[System.DateTime, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]":
                case "System.DateTime?":
                case "System.DateTime":
                    if (valuer.value != null)
                        valuer.value = ((DateTime)valuer.value).ToString("dd/MM/yyyy");
                    valuer.alignH = ExcelHorizontalAlignment.Right;
                    break;

                case "System.String":
                case "System.string":
                    valuer.alignH = ExcelHorizontalAlignment.Left;
                    break;
                case "System.Boolean":
                    valuer.alignH = ExcelHorizontalAlignment.Left;
                    break;
                default:
                    valuer.alignH = ExcelHorizontalAlignment.Left;
                    break;

            }


            return valuer;
        }
        private static ExcelHorizontalAlignment GetAlignByName(string align, ExcelHorizontalAlignment alignHdefault)
        {

            if (align == null)
                return alignHdefault;
            if (align.ToLower() == "right")
                return ExcelHorizontalAlignment.Right;
            if (align.ToLower() == "center")
                return ExcelHorizontalAlignment.Center;
            if (align.ToLower() == "left")
                return ExcelHorizontalAlignment.Left;

            return alignHdefault;
        }
    }
}
