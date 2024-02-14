using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class InvReportBinCard : BaseEntity
    {
        public long InvReportBinCardID { get; set; }

        [DefaultValue(0)]
        public int CompanyID { get; set; }

        [DefaultValue(0)]
        public int LocationID { get; set; }

        [DefaultValue(0)]
        public int CostCentreID { get; set; }

        [DefaultValue(0)]
        public long DocumentID { get; set; }

        [DefaultValue(0)]
        public int DocumentDocumentID { get; set; }

        public DateTime DocumentDate { get; set; }

        [Required]
        [DefaultValue("")]
        [MaxLength(50)]
        public string DocumentDescription { get; set; }

        [DefaultValue(0)]
        public long ReferenceID { get; set; } // Customer/ Vendor ID

        [Required]
        [MaxLength(15)]
        public string ReferenceIDCode { get; set; }

        [Required]
        [MaxLength(100)]
        public string ReferenceIDName { get; set; }

        [DefaultValue("")]
        [MaxLength(20)]
        public string ReferenceNo { get; set; }

        [DefaultValue("")]
        [MaxLength(20)]
        public string PartyReferenceNo { get; set; }

        [DefaultValue(0)]
        public long ProductID { get; set; }

        [Required]
        [MaxLength(25)]
        public string ProductCode { get; set; }

        [Required]
        [MaxLength(100)]
        public string ProductName { get; set; }

        [MaxLength(50)]
        public string BatchNo { get; set; }

        public DateTime ExpiryDate { get; set; }

        [DefaultValue(0)]
        [Required]
        public decimal CostPrice { get; set; }

        [DefaultValue(0)]
        [Required]
        public decimal SellingPrice { get; set; }

        [DefaultValue(0)]
        public decimal FreeQty { get; set; }

        [DefaultValue(0)]
        public decimal InQty { get; set; }

        [DefaultValue(0)]
        public decimal OutQty { get; set; }

        [DefaultValue(0)]
        public decimal BalanceQty { get; set; }

        [DefaultValue("")]
        [MaxLength(150)]
        public string Remark { get; set; }

        [DefaultValue(0)]
        public bool IsUpLoad { get; set; }
    }
}
