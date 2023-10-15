using EPS.Utils.Repository.Audit;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EPS.Data.Entities
{
    public partial class Log
    {
        public Log()
        {
        }
        public int Id { get; set; }
        [Required]
        [StringLength(250)]
        public string Title { get; set; }
        [Required]
        [StringLength(5000)]
        public string Content { get; set; }
        [Required]
        [StringLength(100)]
        public string Object { get; set; }        
        public int Action { get; set; }        
        public DateTime Created { get; set; }
        [Required]
        [StringLength(250)]
        public string CreatedBy { get; set; }
        /// <summary>
        /// 0: Thất bại, 1: Thành công
        /// </summary>
        public int Status { get; set; }
    }
}
