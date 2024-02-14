using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class InvGiftVoucherBookCode : BaseEntity
    {
        public int InvGiftVoucherBookCodeID { get; set; }

        public int InvGiftVoucherGroupID { get; set; }

        [Required]
        [MaxLength(20)]
        public string BookCode { get; set; }

        [Required]
        [MaxLength(50)]
        public string BookName { get; set; }

        [DefaultValue("")]
        [MaxLength(4)]
        public string BookPrefix { get; set; }

        [Required]
        public decimal GiftVoucherValue { get; set; }

        [Required]
        [DefaultValue(0)]
        public decimal GiftVoucherPercentage { get; set; }

        //[Required]
        [DefaultValue(0)]
        public int ValidityPeriod { get; set; }

        [DefaultValue(0)]
        [Description("0 - Default, 1 - Voucher, 2 - Coupon")]
        public int VoucherType { get; set; }

        [Required]
        public int StartingNo { get; set; }

        public int CurrentSerialNo { get; set; }

        [Required]
        public int SerialLength { get; set; }

        [Required]
        public int PageCount { get; set; }

        [DefaultValue(0)]
        public bool IsDelete { get; set; }

        [DefaultValue(0)]
        public int BasedOn { get; set; }

        public virtual InvGiftVoucherGroup InvGiftVoucherGroup { get; set; }
        public virtual ICollection<InvGiftVoucherMaster> InvGiftVoucherMasters { get; set; }
    }
}
