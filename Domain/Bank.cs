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
    public class Bank : BaseEntity
    {
        public int BankID { get; set; } 

        [Required]
        [MaxLength(15)]
        public string BankCode { get; set; }

        [Required]
        [MaxLength(100)]
        public string BankName { get; set; }

        [DefaultValue(0)]
        public bool IsDelete { get; set; }
    }
}
