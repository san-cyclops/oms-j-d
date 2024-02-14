using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    class LgsProductExtendedProperty : BaseEntity
    {
        public long LgsProductExtendedPropertyID { get; set; } 

        [DefaultValue("")]
        [MaxLength(15)]
        public string ExtendedPropertyCode { get; set; }

        [DefaultValue("")]
        [MaxLength(50)]
        public string ExtendedPropertyName { get; set; }

        [DefaultValue("")]
        [MaxLength(15)]
        public string DataType { get; set; }

        [DefaultValue("")]
        [MaxLength(15)]
        public string Parent { get; set; }

        [DefaultValue(0)]
        public bool IsAllLocations { get; set; }

        [DefaultValue(0)]
        public bool IsRequired { get; set; }

        [DefaultValue(0)]
        public bool IsDelete { get; set; }
    }
}
