using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class SupplierGroup : BaseEntity
    {
        public int SupplierGroupID { get; set; }

        [Required]
        [MaxLength(20)]
	    public string SupplierGroupCode { get; set; }

        [Required]
        [MaxLength(50)]
	    public string SupplierGroupName { get; set; }

        [DefaultValue("")]
        [MaxLength(150)]
        public string Remark { get; set; }

        [DefaultValue(0)] 
        public bool IsDelete { get; set; }

        public virtual ICollection<Supplier> Suppliers { get; set; }

        public virtual ICollection<LgsSupplier> LgsSuppliers { get; set; }
    }
}
