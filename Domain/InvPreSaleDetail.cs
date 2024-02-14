using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class InvPreSaleDetail : BaseEntity
    {
        public long InvPreSaleDetailID { get; set; }

        [DefaultValue(0)]
	    public long PreSaleHeaderID { get; set; }

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

	    public int DocumentID { get; set; }

        [DefaultValue(0)]
        [MaxLength(20)]
	    public string DocumentNo { get; set; }
 
	    public DateTime DocumentDate { get; set; }

        [DefaultValue(0)]
	    public decimal OrderQty { get; set; }

        [DefaultValue(0)]
	    public decimal CurrentQty { get; set; }

        [DefaultValue(0)]
	    public decimal BalanceQty { get; set; }

        [DefaultValue(0)]
	    public decimal CostPrice { get; set; }

        [DefaultValue(0)]
	    public decimal SellingPrice { get; set; }

        [DefaultValue(0)]
	    public decimal PackSize { get; set; }

        [DefaultValue(0)]
	    public int PackID { get; set; }

        [DefaultValue("")]
        [MaxLength(5)]
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
	    public int DocumentStatus { get; set; }  
    }
}
