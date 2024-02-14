using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class AccPettyCashVoucherHeader:BaseEntity
    {
        public long AccPettyCashVoucherHeaderID { get; set; }

        [DefaultValue(0)]
        public long PettyCashVoucherHeaderID { get; set; } 

        [DefaultValue(0)]
        public int CompanyID { get; set; }

        [DefaultValue(0)]
        public int LocationID { get; set; }

        [DefaultValue(0)]
        public int CostCentreID { get; set; }

        [DefaultValue(0)]
        public int DocumentID { get; set; }

        [DefaultValue("")]
        [MaxLength(25)]
        public string DocumentNo { get; set; }

        [DefaultValue("")]
        [MaxLength(25)]
        public string LedgerSerialNo { get; set; }

        [Required]
        public long PettyCashLedgerID { get; set; }

        [Required]
        public long EmployeeID { get; set; }

        [Required]
        public string PayeeName { get; set; }

        public DateTime DocumentDate { get; set; }

        public DateTime PaymentDate { get; set; }

        [DefaultValue(0)]
        public long ReferenceDocumentID { get; set; }

        [DefaultValue(0)]
        public int ReferenceDocumentDocumentID { get; set; }

        //public DateTime ReferenceDocumentDate { get; set; }

        [DefaultValue(0)]
        public int ReferenceLocationID { get; set; }

        [DefaultValue(0)]
        public long ReferenceID { get; set; } // Customer/ Vandor ID

        [DefaultValue(0)]
        public decimal Amount { get; set; }

        [DefaultValue(0)]
        public decimal IOUAmount { get; set; }

        [DefaultValue(0)]
        public decimal BalanceAmount { get; set; }

        [DefaultValue("")]
        [MaxLength(50)]
        public string Reference { get; set; }

        [DefaultValue("")]
        [MaxLength(150)]
        public string Remark { get; set; }

        [DefaultValue(0)]
        [Description("0 - Default, 1 - Approved, 2 - Issued, 3 - Cancel")]
        /*
         * 0 - Default
         * 1 - Approved
         * 2 - Issued
         * 3 - Cancel
         * 4 - 
         * 5 - 
         * */
        public int VoucherStatus { get; set; }

        [DefaultValue(0)]
        [Description("0 - Select, 1 - Deselect")]
        [NotMapped]
        public bool Status { get; set; }

        [DefaultValue(0)]
        [Description("0 - Default, 1 - Approved, 2 - Post, 3 - Cancel")]
        /*
         * 0 - Default
         * 1 - Approved
         * 2 - Post
         * 3 - Cancel
         * 4 - 
         * 5 - 
         * */
        public int DocumentStatus { get; set; }

        [DefaultValue(0)]
        public bool IsUpLoad { get; set; }

        public virtual ICollection<AccPettyCashVoucherDetail> AccPettyCashVoucherDetails { get; set; }
    }
}
