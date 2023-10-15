using System;
using System.Collections.Generic;
using System.Text;

namespace EPS.Utils.Common
{
    public class UploadFilesResult
    {
        public string name { get; set; }
        public int length { set; get; }
        public string type { set; get; }
        public string pathFile { set; get; }
        public bool error { set; get; }
        public string errorMessage { set; get; }
        public string thumbnailUrl { get; set; }
        public string deleteUrl { get; set; }
        public string url { get; set; }
        public long size { get; set; }
        public string deleteType { get; set; }
    }
}
