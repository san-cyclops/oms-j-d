using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class InvGiftVoucherPurchaseDetail : BaseEntity
    {
        public long InvGiftVoucherPurchaseDetailID { get; set; }

        [DefaultValue(0)]
        public long GiftVoucherPurchaseDetailID { get; set; } 

        [Required]
        public long InvGiftVoucherPurchaseHeaderID { get; set; }

        [DefaultValue(0)]
        public int CompanyID { get; set; }

        [DefaultValue(0)]
        public int LocationID { get; set; }

        [DefaultValue(0)]
        public int DocumentID { get; set; } //????????????

        [DefaultValue("")]
        [MaxLength(20)]
        [NotMapped]
        public string DocumentNo { get; set; } //????????????

        public DateTime DocumentDate { get; set; }

        [DefaultValue(0)]
        public long LineNo { get; set; }

        [DefaultValue(0)]
        public long InvGiftVoucherMasterID { get; set; }

        [DefaultValue(0)]
        public decimal NumberOfCount { get; set; }

        [DefaultValue(0)]
        public decimal VoucherAmount { get; set; }

        [DefaultValue(0)]
        [Description("1 - Voucher, 2 - Coupon")]
        public int VoucherType { get; set; }

        [DefaultValue(0)]
        public bool IsTransfer { get; set; }

        [DefaultValue(0)]
        public int DocumentStatus { get; set; }
    }
}
