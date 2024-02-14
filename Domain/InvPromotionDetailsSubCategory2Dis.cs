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
    public class InvPromotionDetailsSubCategory2Dis:BaseEntity
    {
        public long InvPromotionDetailsSubCategory2DisID { get; set; }

        [DefaultValue(0)]
        public long InvPromotionMasterID { get; set; }

        [DefaultValue(0)]
        public int CompanyID { get; set; }

        [DefaultValue(0)]
        public int LocationID { get; set; }

        [DefaultValue(0)]
        public long SubCategory2ID { get; set; }

        [DefaultValue("")]
        [MaxLength(25)]
        [Display(Name = "Sub Category2 Code")]
        [NotMapped]
        public string SubCategory2Code { get; set; }

        [Required]
        [MaxLength(100)]
        [NotMapped]
        public string SubCategory2Name { get; set; }

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
