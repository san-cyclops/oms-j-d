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
    public class HrmEmployee : BaseEntity
    {
        public long HrmEmployeeID { get; set; }

        [DefaultValue(0)]
	    public int GroupOfCompany { get; set; }

        [DefaultValue("")]
	    public string EmployeeCode { get; set; }

        [DefaultValue("")]
	    public string EmployeeTitle { get; set; }

        [DefaultValue("")]
	    public string EmployeeSurName { get; set; }

        [DefaultValue("")]
	    public string EmployeeName { get; set; }

        [DefaultValue("")]
	    public string EmployeeLastName { get; set; } 

	    public DateTime DateOfBirth { get; set; }

        [DefaultValue(0)]
	    public long DesignationID { get; set; }

        [DefaultValue(0)]
	    public bool IsDelete { get; set; }
    }
}
