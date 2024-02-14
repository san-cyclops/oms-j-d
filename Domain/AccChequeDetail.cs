using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class AccChequeDetail : BaseEntity
    {
        public long AccChequeDetailID { get; set; }

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

        public DateTime ChequeDate { get; set; }

        [DefaultValue(0)]
        public long BankID { get; set; }

        [DefaultValue(0)]
        public int BankBranchID { get; set; }

        [DefaultValue("")]
        [MaxLength(15)]
        [Description("Cheque No/ Card No")]
        public string CardNo { get; set; }

        [DefaultValue(0)]
        public long DepositedBankID { get; set; }
       
        [DefaultValue(0)]
        public decimal Amount { get; set; }

        [DefaultValue(0)]
        [Description("0 - Cheque Received, 1 - Cheque Paid")]
        public int ChequeType { get; set; }

        [DefaultValue(0)]
        [Description("0 - Cheque Received/ Cheque Paid, 1 - Cheque Deposited / Cheque Printed, 2 - Cheque Cancel, 3 - Cheque Return, 4 - Third Party Issued")]
        /*
         Cheque Type 0 - Received Cheque
         * Cheque Received
         * Cheque Deposited
         * Cheque Cancel
         * Cheque Return
         * Third Party Issued
         * Realized ==> ?
         * 
         * Cheque Type 1 - Paid Cheque
         * Cheque Paid
         * Cheque Printed
         * Cheque Cancel
         * Cheque Return
         * Realized ==> ?
         */
        public int ChequeStatus { get; set; }

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
        public int DocumentStatus { get; set; } // 0 - paused , 1 - saved , 3 - cancelled

        [DefaultValue(0)]
        public bool IsUpLoad { get; set; }
    }
}
