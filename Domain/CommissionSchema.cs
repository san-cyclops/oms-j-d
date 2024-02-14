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
    public class CommissionSchema : BaseEntity 
    {
        public int CommissionSchemaID { get; set; } 

        [Required]
        [MaxLength(15)]
        public string CommissionSchemaCode { get; set; } 

        [Required]
        [MaxLength(50)]
        public string CommissionSchemaName { get; set; }

        [DefaultValue(0)]
        public bool IsDelete { get; set; }

        //public virtual ICollection<InvSalesPerson> InvSalesmans { get; set; }
    }
}
