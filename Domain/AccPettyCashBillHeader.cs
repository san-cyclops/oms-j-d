using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class AccPettyCashBillHeader : BaseEntity
    {
        public long AccPettyCashBillHeaderID { get; set; }

        [DefaultValue(0)]
        public long PettyCashBillHeaderID { get; set; }

        [DefaultValue(0)]
        public int CompanyID { get; set; }

        [DefaultValue(0)]
        public int LocationID { get; set; }

        [DefaultValue(0)]
        public int CostCentreID { get; set; }

        [Required]
        public long PettyCashLedgerID { get; set; }

        [Required]
        public long EmployeeID { get; set; }

        [Required]
        public string PayeeName { get; set; }

        public DateTime DocumentDate { get; set; }

        [DefaultValue(0)]
        public int DocumentID { get; set; }

        [DefaultValue("")]
        [MaxLength(25)]
        public string DocumentNo { get; set; }

        [DefaultValue("")]
        [MaxLength(25)]
        public string LedgerSerialNo { get; set; }

        [DefaultValue(0)]
        public long ReferenceDocumentID { get; set; }

        [DefaultValue(0)]
        public int ReferenceDocumentDocumentID { get; set; }

        //public DateTime ReferenceDocumentDate { get; set; }

        [DefaultValue(0)]
        public int ReferenceLocationID { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [DefaultValue(0)]
        public decimal IOUAmount { get; set; }

        [Required]
        public decimal ReturnedAmount { get; set; }

        [Required]
        public decimal ToSettleAmount { get; set; }

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

        public virtual ICollection<AccPettyCashBillDetail> AccPettyCashBillDetails { get; set; }
    }
}
