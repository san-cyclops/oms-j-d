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
    public class AccJournalEntryDetail : BaseEntity
    {
        public long AccJournalEntryDetailID { get; set; }

        [DefaultValue(0)]
        public long JournalEntryDetailID { get; set; }

        public long AccJournalEntryHeaderID { get; set; }

        [DefaultValue(0)]
        public int CompanyID { get; set; }

        [DefaultValue(0)]
        public int LocationID { get; set; }

        [DefaultValue(0)]
        public int CostCentreID { get; set; }

        [DefaultValue(0)]
        public int DocumentID { get; set; }

        [DefaultValue(0)]
        public long ReferenceID { get; set; } // Customer/ Vandor ID

        [Required]
        public long LedgerID { get; set; }

        [MaxLength(25)]
        [NotMapped]
        public string LedgerCode { get; set; }

        [MaxLength(100)]
        [NotMapped]
        public string LedgerName { get; set; }

        [DefaultValue(0)]
        [Description("1 - Dr, 2 - Cr")]
        public int DrCr { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [DefaultValue(0)]
        public int DocumentStatus { get; set; }

        [DefaultValue(0)]
        public bool IsUpLoad { get; set; }

        public virtual AccJournalEntryHeader AccJournalEntryHeader { get; set; }
    
    }
}
