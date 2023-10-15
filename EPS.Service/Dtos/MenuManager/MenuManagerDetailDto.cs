using EPS.Service.Dtos.Group;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EPS.Service.Dtos.MenuManager
{
    public class MenuManagerDetailDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public int Stt { get; set; }
        public string Icon { get; set; }
        public string Groups { get; set; }//Lưu id group
        public List<GroupDetailDto> LstGroups { get; set; }//Lưu id group
        public int? ParentId { get; set; }
        public string Parent { get; set; }
        public bool IsBlank { get; set; }       
        public bool IsShow { get; set; }
        public MenuManagerDetailDto()
        {
            LstGroups = new List<GroupDetailDto>();            
        }

    }
}
