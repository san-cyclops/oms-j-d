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
    public class LgsProductSupplierLink : BaseEntity
    {
        public long LgsProductSupplierLinkID { get; set; } 

        [DefaultValue(0)]
        public int CompanyID { get; set; }

        [DefaultValue(0)]
        public int LocationID { get; set; }

        [DefaultValue(0)]
        public int CostCentreID { get; set; }

        [DefaultValue(0)]
        public long ProductID { get; set; }

        [DefaultValue(0)]
        public long SupplierID { get; set; }

        [NotMapped]
        public string SupplierCode { get; set; }

        [NotMapped]
        public string SupplierName { get; set; }

        [DefaultValue(0)]
        public int DocumentID { get; set; }

        [DefaultValue(0)]
        public int ReferenceDocumentID { get; set; }

        [MaxLength(50)]
        public string ReferenceDocumentNo { get; set; }

        public DateTime DocumentDate { get; set; }

        [DefaultValue(0)]
        public decimal CostPrice { get; set; }

        [DefaultValue(0)]
        public decimal FixedGP { get; set; }

        [DefaultValue(0)]
        public int DocumentStatus { get; set; }

        [DefaultValue(0)]
        public bool IsDelete { get; set; } 
    }
}
