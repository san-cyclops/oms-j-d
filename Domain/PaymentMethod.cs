using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class PaymentMethod : BaseEntity
    {
        public int PaymentMethodID { get; set; }

        [DefaultValue("")]
        [MaxLength(15)]
	    public string PaymentMethodCode { get; set; }

        [DefaultValue("")]
        [MaxLength(50)]
	    public string PaymentMethodName { get; set; }

        [DefaultValue(0)]
        public decimal CommissionRate { get; set; }

        [DefaultValue(0)]
        public int PaymentType { get; set; }

        [DefaultValue(0)]
	    public bool IsDelete { get; set; }

        public virtual ICollection<Customer> Customers { get; set; }

        ////Cash	0
        ////Credit	1
        ////Cheque	2
        ////Credit Card	3
    }
}
