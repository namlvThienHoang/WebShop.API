using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EPS.API.Models
{
    public class FileUploadResult
    {
        public string FilePath { get; set; }
        public long Size { get; set; }
        public string OriginalName { get; set; }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
}
