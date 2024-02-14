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
    public class CashierFunction : BaseEntity
    {
        public long CashierFunctionID { get; set; } 

        [Required]
        [MaxLength(15)]
        public string FunctionName { get; set; }  

        [Required]
        [MaxLength(100)]
        public string FunctionDescription { get; set; }

        [NotMapped]
        public long RowNo { get; set; }

        public long Order { get; set; }

        public int TypeID { get; set; }    

        [DefaultValue(0)]
        public bool IsDelete { get; set; }

        [DefaultValue(1)]
        public bool IsValue { get; set; }
    }
}
