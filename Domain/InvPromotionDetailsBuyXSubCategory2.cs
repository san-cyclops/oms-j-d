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
    public class InvPromotionDetailsBuyXSubCategory2 : BaseEntity
    {
        public long InvPromotionDetailsBuyXSubCategory2ID { get; set; }

        [DefaultValue(0)]
        public long InvPromotionMasterID { get; set; }

        [DefaultValue(0)]
        public int CompanyID { get; set; }

        [DefaultValue(0)]
        public int LocationID { get; set; }

        [DefaultValue(0)]
        public long BuySubCategory2ID { get; set; }

        [DefaultValue("")]
        [MaxLength(25)]
        [Display(Name = "Sub Category 2 Code")]
        [NotMapped]
        public string SubCategory2Code { get; set; }

        [Required]
        [MaxLength(100)]
        [NotMapped]
        public string SubCategory2Name { get; set; }

        [DefaultValue(0)]
        public decimal BuyQty { get; set; }

        public virtual InvPromotionMaster InvPromotionMaster { get; set; }
    }
}
