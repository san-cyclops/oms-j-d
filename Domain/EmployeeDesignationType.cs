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
    public class EmployeeDesignationType : BaseEntity
    {
        public int EmployeeDesignationTypeID { get; set; }

        [Required]
        [MaxLength(15)]
        public string DesignationCode { get; set; } 

        [Required]
        [MaxLength(100)]
        public string Designation { get; set; }

        [MaxLength(500)]
        public string Remarks { get; set; }

        [DefaultValue(0)]
        public bool IsDelete { get; set; } 
    }
}
