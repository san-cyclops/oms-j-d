using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class InvPromotionDetailsSubTotal:BaseEntity
    {
        public long InvPromotionDetailsSubTotalID { get; set; }

        [DefaultValue(0)]
        public long InvPromotionMasterID { get; set; }

        [DefaultValue(0)]
        public int CompanyID { get; set; }

        [DefaultValue(0)]
        public int LocationID { get; set; }

        [DefaultValue(0)]
        public decimal Points { get; set; }

        [DefaultValue(0)]
        public decimal DiscountPercentage { get; set; }

        [DefaultValue(0)]
        public decimal DiscountAmount { get; set; }

        [DefaultValue(0)]
        public bool IsCombined { get; set; }

        [DefaultValue(0)]
        public bool IsValidForGV { get; set; }

        public virtual InvPromotionMaster InvPromotionMaster { get; set; }
    }
}
