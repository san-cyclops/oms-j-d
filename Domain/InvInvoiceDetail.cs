using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class InvInvoiceDetail : BaseEntity
    {
        [DefaultValue(0)]
        public long InvoiceDetailID { get; set; }

        [DefaultValue(0)]
	    public long InvoiceHeaderID { get; set; }

        [DefaultValue(0)]
	    public int CompanyID { get; set; }

        [DefaultValue(0)]
	    public int LocationID { get; set; }

        [DefaultValue(0)]
	    public int CostCentreID { get; set; }

        [DefaultValue(0)]
	    public long ProductID { get; set; } 

        [DefaultValue("")]
        [MaxLength(25)]
	    public string ProductCode { get; set; }

        [DefaultValue(0)]
	    public int DocumentID { get; set; }

        [DefaultValue("")]
        [MaxLength(20)]
	    public string DocumentNo { get; set; } 

	    public DateTime DocumentDate { get; set; }
 
	    public decimal Qty { get; set; }

        [DefaultValue(0)]
	    public decimal OrderQty { get; set; }

        [DefaultValue(0)]
	    public decimal CurrentQty { get; set; }

        [DefaultValue(0)]
	    public decimal ReturnQty { get; set; }

        [DefaultValue(0)]
	    public decimal DispatchQty { get; set; }

        [DefaultValue("")]
        [MaxLength(50)]
	    public string BatchNo { get; set; } 

	    public DateTime BatchExpiryDate { get; set; }

        [DefaultValue(0)]
	    public decimal AvgCost { get; set; }

        [DefaultValue(0)]
	    public decimal CostPrice { get; set; }

        [DefaultValue(0)]
	    public decimal SellingPrice { get; set; }

        [DefaultValue(0)]
	    public int PackID { get; set; }

        [DefaultValue(0)]
	    public decimal PackSize { get; set; } 

        [MaxLength(5)]
        [DefaultValue("")]
	    public string UnitOfMeasure { get; set; }

        [DefaultValue(0)]
	    public decimal GrossAmount { get; set; }

        [DefaultValue("")]
        [MaxLength(10)]
	    public string DiscountPercentage { get; set; }

        [DefaultValue(0)]
	    public decimal DiscountAmount { get; set; }

        [DefaultValue(0)]
	    public decimal SubTotalDiscount { get; set; }

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
	    public decimal NetAmount { get; set; }

        [DefaultValue(0)]
	    public int CurrencyID { get; set; }

        [DefaultValue(0)]
	    public decimal CurrencyRate { get; set; }

        [DefaultValue(0)]
	    public long LineNo { get; set; }

        [DefaultValue(0)]
	    public long ProductPriceLinkID { get; set; }

        [DefaultValue(0)]
	    public long DispatchLocationID { get; set; }
 
	    public DateTime DeliveryDate { get; set; }

        [DefaultValue(0)]
	    public long PromotionID { get; set; }

        [DefaultValue(0)]
	    public int DocumentStatus { get; set; }  
    }
}
