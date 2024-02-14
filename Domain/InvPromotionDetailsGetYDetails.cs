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
    public class InvPromotionDetailsGetYDetails:BaseEntity
    {
        public long InvPromotionDetailsGetYDetailsID { get; set; }

        [DefaultValue(0)]
        public long InvPromotionMasterID { get; set; }

        [DefaultValue(0)]
        public int CompanyID { get; set; }

        [DefaultValue(0)]
        public int LocationID { get; set; }
        
        [DefaultValue(0)]
        public long GetProductID { get; set; }

        [DefaultValue("")]
        [MaxLength(25)]
        [Display(Name = "Product Code")]
        [NotMapped]
        public string ProductCode { get; set; }

        [Required]
        [MaxLength(100)]
        [NotMapped]
        public string ProductName { get; set; }

        [DefaultValue("")]
        [MaxLength(25)]
        [NotMapped]
        public string UnitOfMeasure { get; set; }

        [DefaultValue(0)]
        public long GetUnitOfMeasureID { get; set; }

        [DefaultValue(0)]
        public decimal GetRate { get; set; }

        [DefaultValue(0)]
        public decimal GetQty { get; set; }

        [DefaultValue(0)]
        public long GetPoints { get; set; }

        [DefaultValue(0)]
        public decimal GetDiscountPercentage { get; set; }

        [DefaultValue(0)]
        public decimal GetDiscountAmount { get; set; }

        public virtual InvPromotionMaster InvPromotionMaster { get; set; }
    }
}
