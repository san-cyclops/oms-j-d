using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class InvGiftVoucherPurchaseDetailTemp
    {
        public long InvGiftVoucherPurchaseDetailID { get; set; }

        public long InvGiftVoucherPurchaseHeaderID { get; set; }

        public int CompanyID { get; set; }

        public int LocationID { get; set; }

        public int DocumentID { get; set; } //????????????

        public string DocumentNo { get; set; } //????????????

        public DateTime DocumentDate { get; set; }

        public string PartyInvoiceNo { get; set; }

        public string DispatchNo { get; set; }

        public DateTime PartyInvoiceDate { get; set; }

        public DateTime DispatchDate { get; set; }

        public long LineNo { get; set; }

        public long InvGiftVoucherMasterID { get; set; }

        public int InvGiftVoucherBookCodeID { get; set; }

        public int InvGiftVoucherGroupID { get; set; }

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

        public string DeliveryPerson { get; set; }

        public string DeliveryPersonNICNo { get; set; }

        public string VehicleNo { get; set; }

        public int VoucherType { get; set; }

        public int DocumentStatus { get; set; }
    }
}
