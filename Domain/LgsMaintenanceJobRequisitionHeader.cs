using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    /// <summary>
    ///  Developed by asanka
    /// </summary>
    /// 
    public class LgsMaintenanceJobRequisitionHeader : BaseEntity
    {
        public long LgsMaintenanceJobRequisitionHeaderID { get; set; }

        [DefaultValue(0)]
        public long MaintenanceJobRequisitionHeaderID { get; set; } 

        [DefaultValue(0)]
        public int CompanyID { get; set; }

        [DefaultValue(0)]
        public int LocationID { get; set; }

        [DefaultValue(0)]
        public int DocumentID { get; set; }

        [DefaultValue("")]
        [MaxLength(20)]
        public string DocumentNo { get; set; }

        public DateTime DocumentDate { get; set; }

        public DateTime ExpectedDate { get; set; }

        public DateTime ExpiryDate { get; set; } 

        [DefaultValue("")]
        [MaxLength(50)]
        public string RequestedBy { get; set; }

        [DefaultValue("")]
        [MaxLength(20)]
        public string ReferenceDocumentNo { get; set; }

        [DefaultValue("")]
        [MaxLength(500)]
        public string JobDescription { get; set; }

        [DefaultValue(0)]
        public int DocumentStatus { get; set; }

        [DefaultValue(0)]
        public int ReferenceDocumentDocumentID { get; set; }

        [DefaultValue(0)]
        public long ReferenceDocumentID { get; set; }

        [DefaultValue(0)]
        public bool IsAssigned { get; set; }
    }
}
