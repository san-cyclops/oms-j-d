using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class AccBillEntryHeader : BaseEntity
    {
        public long AccBillEntryHeaderID { get; set; }

        [DefaultValue(0)]
        public long BillEntryHeaderID { get; set; }

        [DefaultValue(0)]
        public int CompanyID { get; set; }

        [DefaultValue(0)]
        public int LocationID { get; set; }

        [DefaultValue(0)]
        public int CostCentreID { get; set; } // Logged Cost Centre

        public DateTime DocumentDate { get; set; } // Entry Date

        public DateTime ReceivedDate { get; set; }

        public DateTime BillDate { get; set; } 

        public DateTime DueDate { get; set; }

        [DefaultValue(0)]
        public int DocumentID { get; set; }

        [DefaultValue("")]
        [MaxLength(25)]
        public string DocumentNo { get; set; }

        [DefaultValue("")]
        [MaxLength(25)]
        public string LedgerSerialNo { get; set; }

        [Required]
        public long LedgerID { get; set; } // Bill Vendor Control Ledger ID

        [DefaultValue(0)]
        public long ReferenceID { get; set; } // Supplier ID

        [Required]
        public decimal Amount { get; set; }

        [DefaultValue("")]
        [MaxLength(25)]
        public string ReferenceNo { get; set; }

        [DefaultValue("")]
        [MaxLength(25)]
        public string ManualNo { get; set; }

        [DefaultValue("")]
        [MaxLength(150)]
        public string Remark { get; set; }

        [DefaultValue(0)]
        public int DocumentStatus { get; set; }

        [DefaultValue(0)]
        public bool IsUpLoad { get; set; }

        public virtual ICollection<AccBillEntryDetail> AccBillEntryDetails { get; set; }
    }
}
