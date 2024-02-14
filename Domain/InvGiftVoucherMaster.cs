using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class InvGiftVoucherMaster : BaseEntity
    {
        public long InvGiftVoucherMasterID { get; set; }

        public int InvGiftVoucherBookCodeID { get; set; }

        public int InvGiftVoucherGroupID { get; set; }

        [DefaultValue(0)]
        public int CompanyID { get; set; }

        [DefaultValue(0)]
        public int LocationID { get; set; }

        [Required]
        [MaxLength(15)]
        public string VoucherNo { get; set; }

        [Required]
        public int VoucherNoSerial { get; set; }

        [DefaultValue("")]
        [MaxLength(4)]
        public string VoucherPrefix { get; set; }

        [Required]
        //[MaxLength(4)]
        public int SerialLength { get; set; }

        [Required]
        [DefaultValue(0)]
        public decimal GiftVoucherValue { get; set; }

        [Required]
        [DefaultValue(0)]
        public decimal GiftVoucherPercentage { get; set; }

        [Required]
        public int StartingNo { get; set; }

        [Required]
        public int VoucherCount { get; set; }

        //[Required]
        [DefaultValue(0)]
        public int PageCount { get; set; }

        [DefaultValue(0)]
        public string VoucherSerial { get; set; }

        [DefaultValue(0)]
        public int VoucherSerialNo { get; set; }
        
        [DefaultValue(0)]
        [Description("1 - Voucher, 2 - Coupon")]
        public int VoucherType { get; set; }

        [DefaultValue(0)]
        [Description("0 - Default/Create, 1 - Request, 2 - Received, 3 - Transfered, 4 - Issued, 5 - Lost / Damage, 6 - Redeemed ")]
        /*
         * 0 - Default
         * 1 - Request (PO Generated) 
         * 2 - Reeceived (GRN)
         * 3 - Transfered
         * 4 - Issued
         * 5 - Lost / Damage
         * */
        public int VoucherStatus { get; set; }

        [DefaultValue(0)]
        public int ToLocationID { get; set; }

        [DefaultValue(0)]
        public int SoldLocationID { get; set; }

        [DefaultValue(0)]
        public long SoldCashierID { get; set; }

        [DefaultValue(0)]
        public string SoldReceiptNo { get; set; }

        [DefaultValue(0)]
        public int SoldUnitID { get; set; }

        [DefaultValue(0)]
        public long SoldZNo { get; set; }

        
        public DateTime SoldDate { get; set; }


        [DefaultValue(0)]
        public int RedeemedLocationID { get; set; }

        [DefaultValue(0)]
        public long RedeemedCashierID { get; set; }

        [DefaultValue(0)]
        public string RedeemedReceiptNo { get; set; }

        [DefaultValue(0)]
        public int RedeemedUnitID { get; set; }

        [DefaultValue(0)]
        public long RedeemedZNo { get; set; }


        public DateTime RedeemedDate { get; set; }

        public bool IsDelete { get; set; }

        public virtual InvGiftVoucherGroup InvGiftVoucherGroup { get; set; }
        public virtual InvGiftVoucherBookCode InvGiftVoucherBookCode { get; set; }
    }
}
