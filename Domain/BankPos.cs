using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class BankPos : BaseEntity
    {
        public long BankPosID { get; set; } 

        [Required]
        [DefaultValue(0)]
        public long BankID { get; set; }

        [Required]
        [MaxLength(20)]
        [DefaultValue("")]
        public string Bank { get; set; }

        [Required]
        [DefaultValue(0)]
        public decimal Rate { get; set; }

        [DefaultValue(0)]
        public bool IsTerminal { get; set; }
         
        [DefaultValue(0)]
        public bool IsDelete { get; set; }
    }
}
