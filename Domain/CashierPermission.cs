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
    public class CashierPermission : BaseEntity
    {
        public long CashierPermissionID { get; set; }

        [Required]
        [DefaultValue(0)]
        public int LocationID { get; set; }

        [Required]
        [DefaultValue(0)]
        public long CashierID { get; set; }

        [Required]
        [DefaultValue(0)]
        public long EmployeeID { get; set; }

        [NotMapped]
        public string EmployeeCode { get; set; }

        [NotMapped]
        public string EmployeeName { get; set; }

        [NotMapped]
        public string Designation { get; set; }

        [Required]
        [MaxLength(20)]
        public string Password { get; set; }

        [MaxLength(50)]
        public string JournalName { get; set; }

        [MaxLength(50)]
        public string EnCode { get; set; }

        [Required]
        [MaxLength(100)]
        public string FunctionName { get; set; }

        [Required]
        [MaxLength(250)]
        public string FunctionDescription { get; set; }

        [DefaultValue(0)]
        public long Order { get; set; }

        [Required]
        [DefaultValue(0)]
        public decimal Value { get; set; }

        public string Type { get; set; }

        public int TypeID { get; set; }  

        [DefaultValue(0)]
        public bool IsActive { get; set; }

        [DefaultValue(0)]
        public bool IsAccess { get; set; }

        [MaxLength(500)]
        public string Remarks { get; set; } 

        [DefaultValue(0)]
        public bool IsDelete { get; set; }

        [DefaultValue(1)]
        public bool IsValue { get; set; }  
    }
}
