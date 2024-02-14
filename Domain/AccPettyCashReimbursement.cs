using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class AccPettyCashReimbursement:BaseEntity
    {
        public long AccPettyCashReimbursementID { get; set; }

        [DefaultValue(0)]
        public int CompanyID { get; set; }

        [DefaultValue(0)]
        public int LocationID { get; set; }

        public DateTime DocumentDate { get; set; }

        [DefaultValue(0)]
        public int DocumentID { get; set; }

        [DefaultValue("")]
        [MaxLength(25)]
        public string DocumentNo { get; set; }

        [DefaultValue("")]
        [MaxLength(25)]
        public string LedgerSerialNo { get; set; }

        [Required]
        public long EmployeeID { get; set; }

        [Required]
        public string PayeeName { get; set; }

        [Required]
        public long PettyCashLedgerID { get; set; }

        [DefaultValue(0)]
        public int PaymentMethodID { get; set; }

        [Required]
        public long LedgerID { get; set; }

        public DateTime ChequeDate { get; set; }

        public string ChequeNo { get; set; }

        [Required]
        public decimal ReimburseAmount { get; set; }

        [Required]
        public decimal BalanceAmount { get; set; }

        [Required]
        public decimal ImprestAmount { get; set; }

        [Required]
        public decimal IssuedAmount { get; set; }

        [Required]
        public decimal AvailabledAmount { get; set; }

        [Required]
        public decimal UsedAmount { get; set; }

        [Required]
        public decimal BookBalanceAmount { get; set; }

        [DefaultValue("")]
        [MaxLength(50)]
        public string Reference { get; set; }

        [DefaultValue("")]
        [MaxLength(150)]
        public string Remark { get; set; }

        [DefaultValue(0)]
        public int DocumentStatus { get; set; }

        [DefaultValue(0)]
        public bool IsUpLoad { get; set; }
    }
}
