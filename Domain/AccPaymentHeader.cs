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
    public class AccPaymentHeader : BaseEntity
    {
        public long AccPaymentHeaderID { get; set; }

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
        public decimal BalanceAmount { get; set; }

        [DefaultValue("")]
        [MaxLength(50)]
        public string Reference { get; set; }

        [DefaultValue("")]
        [MaxLength(150)]
        public string Remark { get; set; }

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

        //Note :// include field for receipt / payment type // remove not mapped, 
        [Description("1 - Receipt, 2 - Payment")]
        [NotMapped]
        public int DocumentType { get; set; }

        //public virtual ICollection<AccPaymentDetail> AccPaymentDetails { get; set; }
    }
}
