using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class AccGlTransactionHeader : BaseEntity
    {
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
        
        [DefaultValue(0)]
        public int DocumentID { get; set; }

        public DateTime DocumentDate { get; set; }

        [DefaultValue(0)]
        public long ReferenceID { get; set; } // Customer/ Vandor ID

        [DefaultValue(0)]
        public decimal Amount { get; set; }

        [DefaultValue(0)]
        public int ReferenceDocumentDocumentID { get; set; } //ex: GRN/WIN/

        [DefaultValue(0)]
        public long ReferenceDocumentID { get; set; } //ex: GRN/WIN/ ID

        [DefaultValue("")]
        [MaxLength(25)]
        public string ReferenceDocumentNo { get; set; }

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

        public virtual ICollection<AccGlTransactionDetail> AccGlTransactionDetails { get; set; }
    }
}
