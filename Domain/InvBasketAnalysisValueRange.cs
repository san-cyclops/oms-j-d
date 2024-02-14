using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class InvBasketAnalysisValueRange
    {
        public long InvBasketAnalysisValueRangeID { get; set; }
        public decimal RangeFrom { get; set; }
        public decimal RangeTo { get; set; }
        [DefaultValue(0)]
        public int RangeType { get; set; }

    }
}
