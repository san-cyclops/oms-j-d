using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class SystemFeature
    {
        public long SystemFeatureID { get; set; }  

        public long SystemConfigID { get; set; }

        [DefaultValue(0)]
        public int ProductID { get; set; }

        [DefaultValue("")]
        [Description("Entry Update Types 1=>CustomerLevel/SupplierLevel, 2=>ItemLevel, 3=>TransactionLevel")]
        public int EntryLevel { get; set; }

        [DefaultValue(0)]
        public bool IsMinusStock { get; set; }

        [DefaultValue(0)]
        public bool IsBatch { get; set; } 
    }
}
