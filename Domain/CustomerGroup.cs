using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class CustomerGroup : BaseEntity
    {
        public int CustomerGroupID { get; set; }

        [Required]
        [MaxLength(20)]
	    public string CustomerGroupCode { get; set; }

        [Required]
        [MaxLength(100)]
	    public string CustomerGroupName { get; set; }

        [DefaultValue("")]
        [MaxLength(150)]
        public string Remark { get; set; }

        [DefaultValue(0)] 
        public bool IsDelete { get; set; }

        public virtual ICollection<Customer> Customers { get; set; }
    }
}
