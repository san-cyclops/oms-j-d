using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class PointsBreakdown : BaseEntity
    {
        public long PointsBreakdownID { get; set; } 

        [DefaultValue("")]
        public decimal RangeFrom { get; set; }

        [DefaultValue("")]
        public decimal RangeTo { get; set; }  
    }
}
