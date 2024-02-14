using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class InvDispatchDetail : BaseEntity
    {
        public long InvDispatchDetailID { get; set; }
        
        [DefaultValue(0)]
	    public long DispatchHeaderID { get; set; } 

        [DefaultValue(0)]
	    public int CompanyID { get; set; }

        [DefaultValue(0)]
	    public int LocationID { get; set; }

        [DefaultValue(0)]
	    public int CostCentreID { get; set; }

        [DefaultValue(0)]
	    public long ProductID { get; set; }

        [DefaultValue("")]
	    public string ProductCode { get; set; }

        [DefaultValue(0)]
	    public int DocumentID { get; set; }

        [DefaultValue("")]
	    public string DocumentNo { get; set; } 

	    public DateTime DocumentDate { get; set; }

        [DefaultValue(0)]
	    public decimal Qty { get; set; }

        [DefaultValue(0)]
	    public decimal CurrentQty { get; set; }

        [DefaultValue(0)]
	    public decimal InvoiceQty { get; set; }

        [DefaultValue("")]
	    public string BatchNo { get; set; } 

	    public DateTime BatchExpiryDate { get; set; }

        [DefaultValue(0)]
	    public decimal AvgCost { get; set; }

        [DefaultValue(0)]
	    public decimal CostPrice { get; set; }

        [DefaultValue(0)]
	    public decimal SellingPrice { get; set; }

        [DefaultValue(0)]
	    public decimal PackSize { get; set; }

        [DefaultValue(0)]
	    public int PackID { get; set; }

        [DefaultValue("")]
	    public string UnitOfMeasure { get; set; }

        [DefaultValue(0)]
	    public decimal GrossAmount { get; set; }

        [DefaultValue(0)]
	    public decimal NetAmount { get; set; }

        [DefaultValue(0)]
	    public long LineNo { get; set; }

        [DefaultValue(0)]
	    public long InvoiceLocationID { get; set; }

        [DefaultValue(0)]
	    public int DocumentStatus { get; set; }  
    }
}
