using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Broker : BaseEntity
    {
        public long  BrokerID { get; set; } 

        [Required]
        [MaxLength(25)] 
        public string BrokerCode { get; set; } 

        [Required]
        [MaxLength(50)]
        public string BrokerName { get; set; }

        [DefaultValue(0)]
        public bool IsDelete { get; set; }

        //public virtual ICollection<Customer> Customers { get; set; }

    }
}
