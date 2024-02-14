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
    public class AccPettyCashImprestDetail : BaseEntity
    {
        public long AccPettyCashImprestDetailID { get; set; }

        [DefaultValue(0)]
        public int CompanyID { get; set; }

        [DefaultValue(0)]
        public int LocationID { get; set; }

        [Required]
        public long PettyCashLedgerID { get; set; }

        [Required]
        public decimal ImprestAmount { get; set; }

        [Required]
        public decimal IssuedAmount { get; set; }

        [Required]
        public decimal AvailabledAmount { get; set; }

        [Required]
        public decimal UsedAmount { get; set; }

        [Required]
        public decimal BalanceAmount { get; set; }

        [DefaultValue(0)]
        public long ReferenceDocumentID { get; set; }

        [DefaultValue(0)]
        public int ReferenceDocumentDocumentID { get; set; }

        [DefaultValue(0)]
        public int ReferenceLocationID { get; set; }

        [DefaultValue(0)]
        public int DocumentStatus { get; set; }

        [DefaultValue(0)]
        public bool IsUpLoad { get; set; }
    }
}
