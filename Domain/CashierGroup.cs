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
    public class CashierGroup : BaseEntity
    {
        public long CashierGroupID { get; set; }

        [Required]
        public int EmployeeDesignationTypeID { get; set; } 

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
        public bool IsAccess { get; set; }

        [DefaultValue(1)]
        public bool IsValue { get; set; } 
    }
}
