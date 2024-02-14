using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class InvGiftVoucherTransferNoteHeader : BaseEntity
    {
        public long InvGiftVoucherTransferNoteHeaderID { get; set; }

        [DefaultValue(0)]
        public long GiftVoucherTransferNoteHeaderID { get; set; } 

        [DefaultValue(0)]
        public int CompanyID { get; set; }

        [DefaultValue(0)]
        public int LocationID { get; set; }

        [DefaultValue(0)]
        public int CostCentreID { get; set; }

        [DefaultValue(0)]
        public int DocumentID { get; set; }

        [DefaultValue("")]
        [MaxLength(20)]
        public string DocumentNo { get; set; }

        public DateTime DocumentDate { get; set; }

        [DefaultValue(0)]
        public int TransferTypeID { get; set; }

        [DefaultValue(0)]
        public decimal GiftVoucherAmount { get; set; }

        [DefaultValue(0)]
        public decimal GiftVoucherPercentage { get; set; }

        [DefaultValue(0)]
        public int GiftVoucherQty { get; set; }

        [DefaultValue("")]
        [MaxLength(150)]
        public string Remark { get; set; }

        [DefaultValue("")]
        [MaxLength(20)]
        public string ReferenceNo { get; set; }

        [DefaultValue(0)]
        public int ReferenceDocumentDocumentID { get; set; }

        [DefaultValue(0)]
        public long ReferenceDocumentID { get; set; }

        [DefaultValue("")]
        [MaxLength(20)]
        public string ReferenceDocumentNo { get; set; }

        [DefaultValue(0)]
        public int ToLocationID { get; set; }

        [DefaultValue(0)]
        [Description("1 - Voucher, 2 - Coupon")]
        public int VoucherType { get; set; }

        [DefaultValue(0)]
        public int DocumentStatus { get; set; }

        public virtual ICollection<InvGiftVoucherTransferNoteDetail> InvGiftVoucherTransferNoteDetails { get; set; }
    }
}
