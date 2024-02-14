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
    public class InvPromotion : BaseEntity
    {
        public long PromotionID { get; set; }

        [Required]
        [MaxLength(15)]
        public string PromotionCode { get; set; }

        [Required]
        [MaxLength(100)]
        public string PromotionName { get; set; }

        [MaxLength(150)]
        [DefaultValue("")]
        public string Remark { get; set; }

        [DefaultValue(0)]
        public bool IsDelete { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        [DefaultValue(0)]
        public int PaymentMethodID { get; set; }

        [DefaultValue(0)]
        public int ProviderID { get; set; }

        [DefaultValue(0)]
        public decimal MinAmount { get; set; }

        [DefaultValue(0)]
        public decimal MaxAmount { get; set; }

        [DefaultValue(0)]
        public bool IsMonday { get; set; }

        [DefaultValue(0)]
        public bool IsTuesday { get; set; }

        [DefaultValue(0)]
        public bool IsWednesday { get; set; }

        [DefaultValue(0)]
        public bool IsThuresday { get; set; }

        [DefaultValue(0)]
        public bool IsFriday { get; set; }

        [DefaultValue(0)]
        public bool IsSaturday { get; set; }

        [DefaultValue(0)]
        public bool IsSunday { get; set; }

        [DefaultValue(0)]
        public bool IsValueRange { get; set; }

        



    }
}
