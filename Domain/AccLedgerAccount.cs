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
    public class AccLedgerAccount : BaseEntity
    {
        public long AccLedgerAccountID { get; set; }

        [Required()]
        public string LedgerType { get; set; }

        [Required()]
        public int TypeLevel { get; set; }

        [Required()]
        public string TypeNode { get; set; }

        [Required()]
        [MaxLength(25)]
        public string LedgerCode { get; set; }

        [Required()]
        [MaxLength(100)]
        public string LedgerName { get; set; }

        [MaxLength(150)]
        [DefaultValue("")]
        public string Remark { get; set; }

        [DefaultValue(0)]
        public int AccountStatus { get; set; } //Bank - 1, Cash Book - 2, Petty Cash - 3

        [DefaultValue(0)]
        public int StandardDrCr { get; set; } // Dr - 1, Cr - 2

        [DefaultValue(0)]
        public bool IsDelete { get; set; }

        [DefaultValue(0)]
        public bool IsUpLoad { get; set; }

    }
}
