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
    public class AccPettyCashPaymentProcessDetail:BaseEntity
    {
        public long AccPettyCashPaymentProcessDetailID { get; set; }

        [DefaultValue(0)]
        public long PettyCashPaymentProcessDetailID { get; set; } 

        public long AccPettyCashPaymentProcessHeaderID { get; set; }

        public long PettyCashPaymentProcessHeaderID { get; set; }

        [DefaultValue(0)]
        public int CompanyID { get; set; }

        [DefaultValue(0)]
        public int LocationID { get; set; }

        [DefaultValue(0)]
        public int CostCentreID { get; set; }

        [DefaultValue("")]
        [MaxLength(25)]
        public string DocumentNo { get; set; }

        [DefaultValue(0)]
        public int DocumentID { get; set; }

        public DateTime DocumentDate { get; set; }

        public DateTime PaymentDate { get; set; }

        [DefaultValue(0)]
        public long ReferenceDocumentID { get; set; }

        [DefaultValue(0)]
        public int ReferenceDocumentDocumentID { get; set; }

        [DefaultValue(0)]
        public int ReferenceLocationID { get; set; }

        [DefaultValue(0)]
        public long SetoffDocumentID { get; set; }

        [DefaultValue(0)]
        public int SetoffDocumentDocumentID { get; set; }

        [DefaultValue(0)]
        public int SetoffLocationID { get; set; }

        [Required]
        public long LedgerID { get; set; }

        [MaxLength(25)]
        [NotMapped]
        public string LedgerCode { get; set; }

        [MaxLength(100)]
        [NotMapped]
        public string LedgerName { get; set; }

        [Required]
        public decimal Amount { get; set; }

        //[DefaultValue(0)]
        //public int paymentModeID { get; set; }

        //[DefaultValue(0)]
        //public int BankID { get; set; }

        //[DefaultValue(0)]
        //public int BankBranchID { get; set; }

        //[DefaultValue("")]
        //[MaxLength(15)]
        //[Description("Cheque No/ Card No")]
        //public string CardNo { get; set; }

        //public DateTime? ChequeDate { get; set; }

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
    }
}
