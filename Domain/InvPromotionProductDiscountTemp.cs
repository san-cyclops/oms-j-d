using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class InvPromotionProductDiscountTemp
    {
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
        public string ProductCode { get; set; }

        [Required]
        [MaxLength(100)]
        public string ProductName { get; set; }

        [Required]
        public long UnitOfMeasureID { get; set; }

        [DefaultValue("")]
        [MaxLength(25)]
        public string UnitOfMeasure { get; set; }

        [DefaultValue(0)]
        public decimal Rate { get; set; }

        [DefaultValue(0)]
        [Required]
        public decimal Qty { get; set; }

        [DefaultValue(0)]
        public decimal DiscPer { get; set; }

        [DefaultValue(0)]
        public decimal DiscAmount { get; set; }

        [DefaultValue(0)]
        public int Points { get; set; }
    }
}
