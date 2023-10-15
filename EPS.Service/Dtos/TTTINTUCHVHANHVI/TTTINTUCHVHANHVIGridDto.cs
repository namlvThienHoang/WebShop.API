using System;
using System.Collections.Generic;
using System.Text;

namespace EPS.Service.Dtos.TTTINTUCHVHANHVI
{
    public class TTTINTUCHVHANHVIGridDto
    {
        public int ID { get; set; }
        public int ID_DOITUONG { get; set; }    
        public int LOAIDOITUONG { get; set; }
        public int HANHDONG { get; set; }
        public int? ID_TACNHAN { get; set; }
        public DateTime? NGAYTHUCHIEN { get; set; }
    }
}
