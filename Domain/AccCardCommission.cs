using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class AccCardCommission : BaseEntity
    {
        public int AccCardCommissionID { get; set; }

        [Required]
        public long BankLedgerID { get; set; }

        [Required]
        public long LedgerID { get; set; }

        [DefaultValue(0)]
        public decimal CommissionRate { get; set; }

        [DefaultValue("")]
        [MaxLength(150)]
        public string Remark { get; set; }

        [DefaultValue(0)]
        public bool IsDelete { get; set; }

    }
}
