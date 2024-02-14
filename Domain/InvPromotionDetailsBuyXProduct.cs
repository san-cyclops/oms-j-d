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
    public class InvPromotionDetailsBuyXProduct:BaseEntity
    {
        public long InvPromotionDetailsBuyXProductID { get; set; }

        [DefaultValue(0)]
        public long InvPromotionMasterID { get; set; }

        [DefaultValue(0)]
        public int CompanyID { get; set; }

        [DefaultValue(0)]
        public int LocationID { get; set; }

        [DefaultValue(0)]
        public long BuyProductID { get; set; }

        [DefaultValue("")]
        [MaxLength(25)]
        [Display(Name = "Product Code")]
        [NotMapped]
        public string BuyProductCode { get; set; }

        [Required]
        [MaxLength(100)]
        [NotMapped]
        public string BuyProductName { get; set; }

        [DefaultValue("")]
        [MaxLength(25)]
        [NotMapped]
        public string UnitOfMeasure { get; set; }

        [DefaultValue(0)]
        public long BuyUnitOfMeasureID { get; set; }

        [DefaultValue(0)]
        public decimal BuyRate { get; set; }

        [DefaultValue(0)]
        public decimal BuyQty { get; set; }

        public virtual InvPromotionMaster InvPromotionMaster { get; set; }

    }
}
