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
    public class LgsPurchaseOrderHeader : BaseEntity
    {
        public long LgsPurchaseOrderHeaderID { get; set; }

        [DefaultValue(0)]
        public long PurchaseOrderHeaderID { get; set; }  

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
        public long LgsSupplierID { get; set; } 

        public DateTime ExpectedDate { get; set; }

        public DateTime ExpiryDate { get; set; }

        public DateTime PaymentExpectedDate { get; set; }

        [DefaultValue(0)]
        public int ValidityPeriod { get; set; }

        [DefaultValue(0)]
        public bool IsConsignmentBasis { get; set; } 

        [DefaultValue(0)]
        public decimal GrossAmount { get; set; }

        [DefaultValue(0)]
        public decimal OtherCharges { get; set; }

        [DefaultValue(0)]
        public decimal DiscountAmount { get; set; }

        [DefaultValue(0)]
        public decimal DiscountPercentage { get; set; }

        [DefaultValue(0)]
        public decimal TaxAmount1 { get; set; }

        [DefaultValue(0)]
        public decimal TaxAmount2 { get; set; }

        [DefaultValue(0)]
        public decimal TaxAmount3 { get; set; }

        [DefaultValue(0)]
        public decimal TaxAmount4 { get; set; }

        [DefaultValue(0)]
        public decimal TaxAmount5 { get; set; }

        [DefaultValue(0)]
        public decimal TaxAmount { get; set; }

        [DefaultValue(0)]
        public decimal Addition { get; set; }

        [DefaultValue(0)]
        public decimal Deduction { get; set; }

        [DefaultValue(0)]
        public decimal NetAmount { get; set; }

        [DefaultValue("")]
        [MaxLength(50)]
        public string RequestedBy { get; set; }

        [DefaultValue(0)]
        public int DeliveryLocationID { get; set; }

        [DefaultValue(0)]
        public decimal LineDiscountTotal { get; set; }

        [DefaultValue(0)]
        public int PaymentMethodID { get; set; }

        [DefaultValue(0)]
        public int CurrencyID { get; set; }

        [DefaultValue(0)]
        public decimal CurrencyRate { get; set; }

        [DefaultValue("")]
        [MaxLength(500)]
        public string DeliveryDetail { get; set; }

        [DefaultValue(0)]
        public int ReferenceDocumentID { get; set; }

        [DefaultValue("")]
        [MaxLength(20)]
        public string ReferenceNo { get; set; } 

        [DefaultValue("")]
        [MaxLength(150)]
        public string Remark { get; set; }

        [DefaultValue(0)]
        public int DocumentStatus { get; set; }

        [DefaultValue(0)]
        public bool IsUpLoad { get; set; }

        [NotMapped]
        public string DocumentDateShort { get; set; }

        [NotMapped]
        public string ExpiryDateShort { get; set; }  

        public virtual ICollection<LgsPurchaseOrderDetail> LgsPurchaseOrderDetails { get; set; } 
    }
}
