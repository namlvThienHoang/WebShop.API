using EPS.Data.Entities;
using EPS.Utils.Common;

using SolrNet.Attributes;

using System;
using System.Collections.Generic;
using System.Text;

namespace EPS.Data.SolrEntities
{
    public class SolrLogs
    {
        [SolrField]
        public string id { get; set; }
        [SolrField]
        public string Title { get; set; }
        [SolrField]
        public string Content { get; set; }
        [SolrField]
        public int Object { get; set; }
        [SolrField]
        public string RemoteIpAddress { get; set; }
        [SolrField]
        public int Action { get; set; }
        [SolrField]
        public int Status { get; set; }
        [SolrField]
        public DateTime Created { get; set; }
        [SolrField]
        public string CreatedBy { get; set; }
        [SolrField]
        public int CreatedByID { get; set; }
        [SolrField]
        public DateTime Finished { get; set; }
        [SolrField]

        public int ObjectID { get; set; }
        /// <summary>
        /// Khởi tạo đối tượng log
        /// </summary>
        /// <param name="Content">Nội dung</param>
        /// <param name="_object">Đối tượng thao tác</param>
        /// <param name="RemoteIpAddress">IP thao tác</param>
        /// <param name="_action">Thao tác</param>
        /// <param name="_status">trạng thái log</param>
        /// <param name="CreatedBy">Người thao tác</param>
        public string ActionName
        {
            get
            {
                return EnumHelper.GetEnumDescription((ActionLogs)(Action));
            }
        }
        public SolrLogs(string Content, DOITUONG _object, string RemoteIpAddress, int _action, int _status, string CreatedBy, int CreatedByID, int ObjectID)
        {
            this.id = Guid.NewGuid().ToString();
            this.Created = DateTime.Now;
            this.Title = EnumHelper.GetEnumDescription(_object);
            this.Content = Content;
            this.Object = (int)_object;
            this.RemoteIpAddress = RemoteIpAddress;
            this.Action =_action;
            this.Status = _status;
            this.CreatedBy = CreatedBy;
            this.CreatedByID = CreatedByID;
            this.ObjectID = ObjectID;
        }
        public SolrLogs(string Content, DOITUONG _object, string RemoteIpAddress, ActionLogs _action, StatusLogs _status, string CreatedBy, int CreatedByID, int ObjectID, DateTime Finished)
        {
            this.id = Guid.NewGuid().ToString();
            this.Created = DateTime.Now;
            this.Title = EnumHelper.GetEnumDescription(_object);
            this.Content = Content;
            this.Object = (int)_object;
            this.RemoteIpAddress = RemoteIpAddress;
            this.Action = (int)_action;
            this.Status = (int)_status;
            this.CreatedBy = CreatedBy;
            this.CreatedByID = CreatedByID;
            this.ObjectID = ObjectID;
            this.Finished = Finished;
        }
        public SolrLogs()
        {

        }
    }
}
