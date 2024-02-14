using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class InvProductExtendedPropertyAssignment : BaseEntity
    {
        public long InvProductExtendedPropertyAssignmentID { get; set; }

        [DefaultValue(0)]
	    public int CompanyID { get; set; }

        [DefaultValue(0)]
	    public int LocationID { get; set; }

        [DefaultValue(0)]
        public int CostCentreID { get; set; }

        [DefaultValue(0)]
	    public long ProductID { get; set; }

        [DefaultValue("")]
        [MaxLength(15)]
	    public string ExtendedPropertyAssignmentCode { get; set; }

        [DefaultValue("")]
        [MaxLength(50)]
	    public string ExtendedPropertyAssignmentValueData { get; set; }

        [DefaultValue(0)]
	    public bool IsDelete { get; set; }

    }
}
