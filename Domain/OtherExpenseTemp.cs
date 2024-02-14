using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace Domain
{
    public class OtherExpenseTemp
    {
        public long OtherExpenseTempID { get; set; }

        [DefaultValue(0)]
        public long AccLedgerAccountID { get; set; }

        [MaxLength(40)]
        public string LedgerCode { get; set; }

        [MaxLength(100)]
        public string LedgerName { get; set; }

        [DefaultValue(0)]
        public decimal ExpenseAmount { get; set; }
    }
}
