using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class AccTransactionTypeHeader : BaseEntity
    {
        public int AccTransactionTypeHeaderID { get; set; } //AccTransactionTypeLink

        [DefaultValue(0)]
        public int TransactionTypeHeaderID { get; set; }

        [Required]
        [MaxLength(15)]
        public string TransactionCode { get; set; }

        [Required]
        [MaxLength(100)]
        public string TransactionName { get; set; }
        
        [DefaultValue(0)]
        public bool IsDelete { get; set; }
    }
}
