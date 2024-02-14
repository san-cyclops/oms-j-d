using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class InvProductColorSizeDetail : BaseEntity
    {
        public long InvProductColorSizeDetailID { get; set; } 

        [DefaultValue(0)]
	    public int CompanyID { get; set; }

        [DefaultValue(0)]
	    public int LocationID { get; set; }

        [DefaultValue(0)]
	    public int CostCentreID { get; set; }

        [DefaultValue(0)]
	    public long DocumentID { get; set; }

        [DefaultValue(0)]
	    public long ProductID { get; set; }

        [DefaultValue(0)]
	    public long ReferenceDocumentID { get; set; }
        
        [DefaultValue("")]
        [MaxLength(20)]
	    public string ReferenceDocumentNo { get; set; }

        [DefaultValue(0)]
	    public long ColorSizeID { get; set; }

        [DefaultValue(0)]
	    public decimal Qty { get; set; }

        [DefaultValue(0)]
	    public decimal BalanceQty { get; set; }

        [DefaultValue(0)]
	    public bool IsDelete { get; set; } 

    }
}
