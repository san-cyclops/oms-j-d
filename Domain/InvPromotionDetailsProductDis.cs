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
    public class InvPromotionDetailsProductDis:BaseEntity
    {
        public long InvPromotionDetailsProductDisID { get; set; }

        [DefaultValue(0)]
        public long InvPromotionMasterID { get; set; }

        [DefaultValue(0)]
        public int CompanyID { get; set; }

        [DefaultValue(0)]
        public int LocationID { get; set; }

        [DefaultValue(0)]
        public long ProductID { get; set; }

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
        public long UnitOfMeasureID { get; set; }

        [DefaultValue(0)]
        public decimal Rate { get; set; }

        [DefaultValue(0)]
        public decimal Qty { get; set; }

        [DefaultValue(0)]
        public long Points { get; set; }

        [DefaultValue(0)]
        public decimal DiscountPercentage { get; set; }

        [DefaultValue(0)]
        public decimal DiscountAmount { get; set; }

        public virtual InvPromotionMaster InvPromotionMaster { get; set; }
    }
}
