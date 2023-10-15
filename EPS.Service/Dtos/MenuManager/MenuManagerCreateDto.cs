using System;
using System.Collections.Generic;
using System.Text;

namespace EPS.Service.Dtos.MenuManager
{
    public class MenuManagerCreateDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public int Stt { get; set; }
        public string Icon { get; set; }
        public string Groups { get; set; }//Lưu id group
        public int? ParentId { get; set; }       
        public bool IsBlank { get; set; }       
        public bool IsShow { get; set; }
    }
}
