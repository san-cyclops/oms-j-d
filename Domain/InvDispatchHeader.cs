using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class InvDispatchHeader : BaseEntity
    {
        [DefaultValue(0)]
        public long InvDispatchHeaderID { get; set; } 

        [DefaultValue(0)]
	    public int CompanyID { get; set; }

        [DefaultValue(0)]
	    public int LocationID { get; set; } 

        [DefaultValue(0)]
	    public int CostCentreID { get; set; }

        [DefaultValue(0)]
	    public int DocumentID { get; set; }

        [DefaultValue("")]
	    public string DocumentNo { get; set; } 

	    public DateTime DocumentDate { get; set; } 

        [DefaultValue(0)]
	    public long CustomerID { get; set; }

        [DefaultValue(0)]
	    public decimal GrossAmount { get; set; }

        [DefaultValue(0)]
	    public decimal Addition { get; set; }

        [DefaultValue(0)]
	    public decimal Deduction { get; set; }

        [DefaultValue(0)]
	    public decimal NetAmount { get; set; }

        [DefaultValue("")]
	    public string RequestedBy { get; set; }

        [DefaultValue("")]
	    public string Remark { get; set; }

        [DefaultValue(0)]
	    public int ReferenceDocumentID { get; set; }

        [DefaultValue("")]
	    public string ReferenceDocumentNo { get; set; }

        [DefaultValue(0)]
	    public long SalesmanID { get; set; }

        [DefaultValue(0)]
	    public int DocumentStatus { get; set; }
    }
}
