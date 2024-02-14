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
    public class LgsProductExtendedPropertyValue : BaseEntity
    {
        public long LgsProductExtendedPropertyValueID { get; set; } 

        [Required]
        public long ProductID { get; set; }

        [Required]
        public long LgsProductExtendedPropertyID { get; set; } 

        [NotMapped]
        [MaxLength(50)]
        public string ExtendedPropertyName { get; set; }

        [Required]
        public long LgsProductExtendedValueID { get; set; }
         
        [NotMapped]
        [MaxLength(50)]
        public string ValueData { get; set; }

        [DefaultValue(0)]
        public bool IsDelete { get; set; }

        public virtual LgsProductMaster LgsProductMaster { get; set; } 
    }
}
