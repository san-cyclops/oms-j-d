    using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class TempLocationWiseLoyaltyAnalysis:BaseEntity
    {
        public long TempLocationWiseLoyaltyAnalysisID { get; set; }
 
        [DefaultValue(0)]
        public int LocationID { get; set; }

        [DefaultValue("")]
        public string LocationCode { get; set; } 

        [DefaultValue("")]
        public string LocationName { get; set; } 

        [DefaultValue(0)]
        public long UsedCard { get; set; }

        [DefaultValue(0)]
        public long CardIssu { get; set; }

        [DefaultValue(0)]
        public long UsedPoints { get; set; }

        [DefaultValue(0)]
        public long NonUsedCard { get; set; }

        [DefaultValue(0)]
        public long NonUsedPoints { get; set; }
           
        [DefaultValue(0)]
        public long CardInAbeyance { get; set; }

        [DefaultValue(0)]
        public long PointsInAbeyance { get; set; }

        [DefaultValue(0)]
        public long ActiveCard { get; set; }

        [DefaultValue(0)]
        public long ActivePoints { get; set; }

        [DefaultValue(0)]
        public long InactiveCard { get; set; }

        [DefaultValue(0)]
        public long InactivePoints { get; set; }

        [DefaultValue(0)]
        public long UsedCardPer { get; set; }
 
    }
}
