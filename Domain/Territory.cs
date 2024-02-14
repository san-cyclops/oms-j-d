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
    public class Territory : BaseEntity
    {
        //[Key]
        public long TerritoryID { get; set; }

        [Required]
	    public long AreaID { get; set; }

        [DefaultValue("")]
        [MaxLength(15)]
        [Required]
	    public string TerritoryCode { get; set; }

        [DefaultValue("")]
        [MaxLength(100)]
        [Required]
	    public string TerritoryName { get; set; }

        [DefaultValue(0)]
	    public bool IsDelete { get; set; }

        public Area Area { get; set; }

        public virtual ICollection<Customer> Customers { get; set; }
    }
}
