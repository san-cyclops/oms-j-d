using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class TempPointsBreakdown : BaseEntity
    {
        public long TempPointsBreakdownID { get; set; }

        public long PointsBreakdownID { get; set; } 

        [DefaultValue("")]
        public decimal RangeFrom { get; set; }

        [DefaultValue("")]
        public decimal RangeTo { get; set; }

        [DefaultValue("")]
        public string Range { get; set; } 

        [DefaultValue(0)]
        public int CustomerCount { get; set; }

        [DefaultValue(0)]
        public decimal PointsTotal { get; set; }     
    }
}
