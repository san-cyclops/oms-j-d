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
    public class AccPettyCashBillDetail : BaseEntity
    {
        public long AccPettyCashBillDetailID { get; set; }

        [DefaultValue(0)]
        public long PettyCashBillDetailID { get; set; }

        public long AccPettyCashBillHeaderID { get; set; }

        [DefaultValue(0)]
        public int CompanyID { get; set; }

        [DefaultValue(0)]
        public int LocationID { get; set; }

        [DefaultValue(0)]
        public int CostCentreID { get; set; }

        [DefaultValue(0)]
        public int DocumentID { get; set; }

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

        [DefaultValue(0)]
        public int DocumentStatus { get; set; }

        [DefaultValue(0)]
        public bool IsUpLoad { get; set; }

        public virtual AccPettyCashBillHeader AccPettyCashBillHeader { get; set; }
    }
}
