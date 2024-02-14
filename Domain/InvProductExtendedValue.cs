using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class InvProductExtendedValue : BaseEntity
    {        
        public long InvProductExtendedValueID { get; set; }

        ///// <summary>
        ///// Forign key
        ///// </summary>
        [DefaultValue(0)]
        public long InvProductExtendedPropertyID { get; set; }

        //public InvProductExtendedProperty InvProductExtendedProperty { get; set; }

        //[DefaultValue("")]
        //[MaxLength(15)]
        //public string ProductExtendedValueCode { get; set; }
        
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
