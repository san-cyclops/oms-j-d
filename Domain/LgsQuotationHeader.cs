using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class LgsQuotationHeader : BaseEntity
    {
        public long LgsQuotationHeaderID { get; set; }

        [DefaultValue(0)]
        public long QuotationHeaderID { get; set; } 

        [DefaultValue(0)]
        public int CompanyID { get; set; }

        [DefaultValue(0)]
        public int LocationID { get; set; }

        [DefaultValue(0)]
        public int CostCentreID { get; set; }

        [DefaultValue(0)]
        public int DocumentID { get; set; }

        [MaxLength(20)]
        public string DocumentNo { get; set; }

        [MaxLength(20)]
        public string ReferenceDocumentNo { get; set; }

        [DefaultValue(0)]
        public long SupplierID { get; set; }

        public long InvSalesPersonID { get; set; }

        public DateTime DocumentDate { get; set; }

        public DateTime DeliveryDate { get; set; }

        [DefaultValue(0)]
        public int PaymentMethodID { get; set; }

        [DefaultValue(0)]
        public decimal GrossAmount { get; set; }

        [DefaultValue(0)]
        public decimal DiscountAmount { get; set; }

        [DefaultValue(0)]
        public decimal OtherCharges { get; set; } 

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

        [DefaultValue(0)]
        public int DeliveryLocationID { get; set; }

        [DefaultValue(0)]
        public decimal LineDiscountTotal { get; set; }

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
        [MaxLength(150)]
        public string Remark { get; set; }

        [DefaultValue(0)]
        public int DocumentStatus { get; set; }

        [DefaultValue(0)]
        public bool IsDelete { get; set; }

        [DefaultValue("")]
        [MaxLength(20)]
        public string ReferenceNo { get; set; }
    }
}
