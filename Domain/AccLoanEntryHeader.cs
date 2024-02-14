using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class AccLoanEntryHeader:BaseEntity
    {
        public long AccLoanEntryHeaderID { get; set; }

        [DefaultValue(0)]
        public long LoanEntryHeaderID { get; set; }

        [DefaultValue(0)]
        public int CompanyID { get; set; }

        [DefaultValue(0)]
        public int LocationID { get; set; }

        [DefaultValue(0)]
        public int CostCentreID { get; set; } // Logged Cost Centre

        public DateTime DocumentDate { get; set; } // Entry Date

        [DefaultValue(0)]
        public int DocumentID { get; set; }

        [DefaultValue("")]
        [MaxLength(25)]
        public string DocumentNo { get; set; }

        [DefaultValue("")]
        [MaxLength(25)]
        public string LedgerSerialNo { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        public decimal AssetAmount { get; set; }

        [Required]
        public decimal DownPayment { get; set; }

        [DefaultValue(0)]
        public int BeginMonthDownPayment { get; set; }

        [DefaultValue(0)]
        public int EndMonthDownPayment { get; set; }

        [DefaultValue(0)]
        public decimal InterestRate { get; set; }

        [Required]
        public decimal ServiceCharge { get; set; }

        [Required]
        public decimal OtherCharge { get; set; }

        [DefaultValue(0)]
        public int PaymentMethodID { get; set; }

        [DefaultValue("")]
        [MaxLength(25)]
        public string ReferenceNo { get; set; }

        [DefaultValue("")]
        [MaxLength(25)]
        public string ManualNo { get; set; }

        [DefaultValue(0)]
        public int FinanceInstituteID { get; set; }

        [DefaultValue(0)]
        public int LoanTypeID { get; set; }

        [DefaultValue(0)]
        public int LoanPurposeID { get; set; }

        [DefaultValue(0)]
        public int LoanTermID { get; set; }

        [DefaultValue(0)]
        public int LoanPeriod { get; set; }

        [DefaultValue(0)]
        public int GracePeriod { get; set; }

        [DefaultValue(0)]
        public int MonthlyGracePeriod { get; set; }

        [DefaultValue("")]
        [MaxLength(150)]
        public string Remark { get; set; }

        [DefaultValue(0)]
        public bool IsSendEmail { get; set; }

        [DefaultValue("")]
        [MaxLength(150)]
        public string EmailAddress { get; set; }

        [DefaultValue(0)]
        public bool IsSendSms { get; set; }

        [DefaultValue("")]
        [MaxLength(50)]
        public string TelephoneNo { get; set; } //Validate valid Telephone No if multiple support allow seperators
        
        [DefaultValue(0)]
        public bool IsUserPopups { get; set; }

        [DefaultValue(0)]
        public int NotifyBeforeNoOfDays { get; set; }

        [DefaultValue(0)]
        public int DocumentStatus { get; set; }

        [DefaultValue(0)]
        public bool IsMortgageLoan { get; set; }

        public DateTime MortgageDate { get; set; }

        [DefaultValue(0)]
        public decimal MortgageInterestRate { get; set; }

        [DefaultValue("")]
        [MaxLength(25)]
        public string MortgageNo { get; set; }

        [DefaultValue(0)]
        public bool IsUpLoad { get; set; }
    }
}
