using System;
using System.Collections.Generic;
using System.Text;
namespace EPS.Service.Dtos.ConfigExcel
{
    public class ConfigExcelDetailDto
    {
        	public  int Id { get; set; }
	public  string TieuDe { get; set; }
	public  string DoiTuong { get; set; }
	public  int Loai { get; set; }
	public  string CauHinh { get; set; }
	public  int RowStart { get; set; }
		public string FileDinhKem { get; set; }
		public string CauHinhHeader { get; set; }
	}
}
