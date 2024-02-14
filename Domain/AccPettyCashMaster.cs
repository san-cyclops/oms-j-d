using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class AccPettyCashMaster:BaseEntity
    {
        public int AccPettyCashMasterID { get; set; }

        [DefaultValue(0)]
        public int CompanyID { get; set; }

        [DefaultValue(0)]
        public int LocationID { get; set; }

        [Required]
        public long LedgerID { get; set; }
         
        [Required]
        public decimal Amount { get; set; }

        [DefaultValue(0)]
        public bool IsDelete { get; set; }

        //public virtual AccLedgerAccount AccLedgerAccount { get; set; }
    }
}
