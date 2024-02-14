using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class InvGiftVoucherPurchaseOrderDetailTemp
    {
        public long InvGiftVoucherPurchaseOrderDetailID { get; set; }

        public long InvGiftVoucherPurchaseOrderHeaderID { get; set; }

        public int CompanyID { get; set; }

        public int LocationID { get; set; }

        public int DocumentID { get; set; } //????????????

        public string DocumentNo { get; set; } //????????????

        public DateTime DocumentDate { get; set; }

        public long LineNo { get; set; }

        public long InvGiftVoucherMasterID { get; set; }

        public string VoucherNo { get; set; }

        public string VoucherSerial { get; set; }

        public decimal NumberOfCount { get; set; }

        public decimal VoucherAmount { get; set; }

        public decimal GiftVoucherPercentage { get; set; }

        public decimal VoucherValue { get; set; }

        public decimal GrossAmount { get; set; }

        public decimal DiscountPercentage { get; set; }

        public decimal DiscountAmount { get; set; }

        public decimal SubTotalDiscount { get; set; }

        public decimal NetAmount { get; set; }

        public int VoucherType { get; set; }

        public int DocumentStatus { get; set; }
    }
}
