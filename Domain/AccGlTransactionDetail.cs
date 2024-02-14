using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class AccGlTransactionDetail : BaseEntity
    {
        public long AccGlTransactionDetailID { get; set; } 

        public long AccGlTransactionHeaderID { get; set; }

        [DefaultValue(0)]
        public int CompanyID { get; set; }

        [DefaultValue(0)]
        public int LocationID { get; set; }

        [DefaultValue(0)]
        public int CostCentreID { get; set; }

        [DefaultValue("")]
        [MaxLength(25)]
        public string DocumentNo { get; set; }


        [Required]
        public long LedgerID { get; set; }

        [DefaultValue("")]
        [MaxLength(25)]
        public string LedgerSerial { get; set; } // have to maintain & read from serial table ledger wise [Ex: cash book/ bank book serial]

        [DefaultValue(0)]
        public int DocumentID { get; set; }

        public DateTime DocumentDate { get; set; }

        public DateTime PaymentDate { get; set; } //Cheque Date/Payment Date --> ?

        [DefaultValue(0)]
        public long ReferenceID { get; set; } // Customer/ Vandor ID

        [DefaultValue(0)]
        [Description("1 - Dr, 2 - Cr")]
        public int DrCr { get; set; }

        [DefaultValue(0)]
        public decimal Amount { get; set; }

        [DefaultValue(0)]
        public int TransactionTypeId { get; set; } //ex: GRN/WIN/

        [DefaultValue(0)]
        public long ReferenceDocumentID { get; set; } //ex: GRN/WIN/ ID

        [DefaultValue(0)]
        public int ReferenceDocumentDocumentID { get; set; } //ex: GRN/WIN/ ID

        [DefaultValue(0)]
        public int ReferenceLocationID { get; set; }

        [DefaultValue("")]
        [MaxLength(25)]
        public string ReferenceDocumentNo { get; set; }

        [DefaultValue(0)]
        public int TransactionDefinitionId { get; set; } //Ex: Transaction,Discount,Tax

        //other expence 1

        [DefaultValue("")]
        [MaxLength(15)]
        [Description("Cheque No")]
        public string ChequeNo { get; set; } // FROM -> Cheque Payment/ Bank Deposit - [Bank + BankBranch + ChequeNo]

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

        public virtual AccGlTransactionHeader AccGlTransactionHeader { get; set; }
    }
}
