using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class AccLoanEntryDetail:BaseEntity
    {
        public long AccLoanEntryDetailID { get; set; }

        [DefaultValue(0)]
        public long LoanEntryDetailID { get; set; }

        public long AccLoanEntryHeaderID { get; set; }

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

        [DefaultValue(0)]
        public int RentalNo { get; set; } 

        [Required]
        public decimal RentalAmount { get; set; }

        [Required]
        public decimal CapitalAmount { get; set; }

        [Required]
        public decimal InterestAmount { get; set; }

        [Required]
        public decimal TaxAmount { get; set; }

        [DefaultValue(0)]
        public int DocumentStatus { get; set; }

        [DefaultValue(0)]
        public bool IsUpLoad { get; set; }
    }
}
