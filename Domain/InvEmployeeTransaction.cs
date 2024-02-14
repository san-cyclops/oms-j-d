using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class InvEmployeeTransaction
    {

        public  long InvEmployeeTransactionID { get; set; }
        public long EmployeeID { get; set; }

        [DefaultValue("")]
        [MaxLength(15)]
        public string Receipt { get; set; }

        public decimal Amount { get; set; }
        public decimal Balance { get; set; }
        public int TransID { get; set; }
        public int LocationID { get; set; }
        public DateTime Date { get; set; }
        public int UnitNo { get; set; }
        public DateTime Time { get; set; }
        public decimal DiscPer { get; set; }
        public decimal DiscAmt { get; set; }
        public long Zno { get; set; }
        public decimal CrLimit { get; set; }
        public decimal PchLimit { get; set; }
        public decimal AccCredit { get; set; }
        public decimal AccPch { get; set; }
        public long CashierID { get; set; }

        [DefaultValue(0)]
        public int IsSync { get; set; } 


    }
}
