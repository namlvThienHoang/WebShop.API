using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
namespace EPS.Data.Entities
{
    public class ConfigExcel
    {
        public ConfigExcel()
        {
        }
        [Required]
        public int Id { get; set; }
        [StringLength(250)]
        [Required]
        public string TieuDe { get; set; }
        [StringLength(250)]
        [Required]
        public string DoiTuong { get; set; }
        [Required]
        public int Loai { get; set; }
        [Required]
        public string CauHinh { get; set; }
        [Required]
        public int RowStart { get; set; }
        public string FileDinhKem { get; set; }
        public string CauHinhHeader { get; set; }
        //add foreign key {4}
    }
    public class CauHinhImport
    {
        public int index { get; set; }
        public string value { get; set; }
        public string category { get; set; }
    }
    public class CauHinhHeaderExport
    {
        public int index { get; set; } 
        public int row { get; set; }
        public int rowspan { get; set; }
        public int colspan { get; set; }
        public string value { get; set; }
     
    }

}
