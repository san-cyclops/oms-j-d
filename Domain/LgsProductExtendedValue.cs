using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class LgsProductExtendedValue : BaseEntity
    {
        [Key]
        public long LgsProductExtendedValueID { get; set; } 

        [DefaultValue("")]
        [MaxLength(15)]
        public string ProductExtendedValueCode { get; set; }

        public long LgsExtendedPropertyID { get; set; } 

        [DefaultValue("")]
        [MaxLength(50)]
        [Display(Name = "Value Data")]
        public string ValueData { get; set; }

        [DefaultValue("")]
        [MaxLength(50)]
        public string ParentValueData { get; set; }

        [DefaultValue(0)]
        public bool IsDelete { get; set; }
    }
}
