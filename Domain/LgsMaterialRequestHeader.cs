using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    /// <summary>
    ///  Developed by asanka
    /// </summary>
    public class LgsMaterialRequestHeader : BaseEntity
    {
        public long LgsMaterialRequestHeaderID { get; set; }

        [DefaultValue(0)]
        public long MaterialRequestHeaderID { get; set; }

        [DefaultValue(0)]
        public int CompanyID { get; set; }

        [DefaultValue(0)]
        public int LocationID { get; set; }

        [DefaultValue(0)]
        public int CostCentreID { get; set; }

        [DefaultValue(0)]
        public int DocumentID { get; set; }

        [DefaultValue("")]
        [MaxLength(20)]
        public string DocumentNo { get; set; }

        public DateTime DocumentDate { get; set; }

        public DateTime ExpiryDate { get; set; }

        [DefaultValue(0)]
        public int ValidityPeriod { get; set; }
        
        public DateTime DiliveryDate { get; set; }
        
        [DefaultValue("")]
        [MaxLength(50)]
        public string RequestedBy { get; set; }

        [DefaultValue("")]
        [MaxLength(20)]
        public string ReferenceDocumentNo { get; set; }

        [DefaultValue(0)]
        public int ReferenceDocumentDocumentID { get; set; }

        [DefaultValue(0)]
        public long ReferenceDocumentID { get; set; }

        [DefaultValue("")]
        [MaxLength(150)]
        public string Remark { get; set; }

        [DefaultValue(0)]
        public int DocumentStatus { get; set; }

        public virtual ICollection<LgsMaterialRequestDetail> LgsMaterialRequestDetails { get; set; }

        [DefaultValue(0)]
        public bool Viewed { get; set; }

        [DefaultValue(0)]
        public bool IsExpired { get; set; }  

        [NotMapped]
        public string LocationName { get; set; }
        
        [NotMapped]
        public string DocumentDateShort { get; set; } 
    }
}
