using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class InvPaymentCardType
    {
        public long InvPaymentCardTypeID { get; set; }

        [Required]
        public long BankID { get; set; }

        [Required]
        [MaxLength(15)]
        public string PaymentCardCode { get; set; }

        [Required]
        [MaxLength(50)]
        public string PaymentCardName { get; set; }


        [DefaultValue(0)]
        public bool IsDelete { get; set; }
       
    }
}
