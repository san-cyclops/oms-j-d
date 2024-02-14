using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Sarasa.ERP.Domain
{
    public class InvProductExpiary : BaseEntity
    {
        public long InvProductExpiaryID { get; set; }


        [DefaultValue(0)]
	    public int CompanyID { get; set; }

        [DefaultValue(0)]
	    public int LocationID { get; set; }

        [DefaultValue(0)]
	    public int CostCentreID { get; set; }

        [DefaultValue(0)]
	    public long ProductID { get; set; }

        [DefaultValue(0)]
        public long UnitOfMeasureID { get; set; }

        [DefaultValue(0)]
        public long LineNo { get; set; }

        [DefaultValue(0)]
	    public DateTime ExpiaryDate { get; set; }

        [DefaultValue(0)]
	    public decimal Qty { get; set; }

        [DefaultValue(0)]
	    public decimal BalanceQty { get; set; }

        [DefaultValue(0)]
	    public bool IsDelete { get; set; }   
    }
}
