using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace Domain
{
    public class PaymentTemp
    {
        public long PaymentTempID { get; set; }

        
        [DefaultValue(0)]
        public int PaymentMethodID { get; set; }

        [MaxLength(40)]
        public string PaymentMethod { get; set; }

        [MaxLength(40)]
        public string CardCheqNo { get; set; }

        [DefaultValue(0)]
        public long BankID { get; set; }

        [MaxLength(40)]
        public string BankCode { get; set; }

        [MaxLength(100)]
        public string BankName { get; set; }

        public DateTime? ChequeDate { get; set; }

        [DefaultValue(0)]
        public long AccLedgerAccountID { get; set; }

        [DefaultValue(0)]
        public decimal PayAmount { get; set; }
        
    }
}
