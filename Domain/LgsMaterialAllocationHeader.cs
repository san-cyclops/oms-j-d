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
    public class LgsMaterialAllocationHeader : BaseEntity
    {
        public long LgsMaterialAllocationHeaderID { get; set; }

        [DefaultValue(0)]
        public long MaterialAllocationHeaderID { get; set; } 

        //[NotMapped]
        //public long LgsMaintenanceJobAssignHeaderID { get; set; }

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
       
        [DefaultValue("")]
        [MaxLength(20)]
        public string ReferenceDocumentNo { get; set; }

        [DefaultValue("")]
        [MaxLength(150)]
        public string Remark { get; set; }

        [DefaultValue(0)]
        public int DocumentStatus { get; set; }

        [DefaultValue(0)]
        public int ReferenceDocumentDocumentID { get; set; }

        [DefaultValue(0)]
        public long ReferenceDocumentID { get; set; }

        public virtual ICollection<LgsMaterialAllocationDetail> LgsMaterialAllocationDetails { get; set; }

    }
}
