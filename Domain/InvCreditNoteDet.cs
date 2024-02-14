using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class InvCreditNoteDet
    {

        public long InvCreditNoteDetID { get; set; }

        [MaxLength(15)]
        public string CrNoteNo { get; set; }

        [MaxLength(15)]
        public string Receipt { get; set; }

        public decimal Amount { get; set; }
        public decimal Balance { get; set; }
        public int LocationID { get; set; }
        public DateTime Date { get; set; }
        public int UnitNo { get; set; }
        public long CashierID { get; set; }
        public DateTime Time { get; set; }
        public long Zno { get; set; }
        public int TransID { get; set; }
    }
}
