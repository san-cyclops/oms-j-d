using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class AccTransactionTypeDetail : BaseEntity
    {
        public int AccTransactionTypeDetailID { get; set; } //AccTransactionTypeLink

        [DefaultValue(0)]
        public int TransactionTypeDetailID { get; set; }

        public long AccTransactionTypeHeaderID { get; set; }

        [Required]
        /*
         * Transcation
         * Discount
         * Tax
         * Reference Transaction
         * Etc.
         */
        public int TransactionDefinition { get; set; }

        [Required]
        public int TransactionModeID { get; set; }

        public int DrCr { get; set; } // Dr - 1, Cr - 2

        [Required]
        public long AccLedgerAccountID { get; set; }

        [NotMapped]
        public string LedgerCode { get; set; }

        [NotMapped]
        public string LedgerName { get; set; }

        [Required]
        [DefaultValue(0)]
        public decimal LedgerPercentage { get; set; } 

        [DefaultValue(0)]
        public bool IsDelete { get; set; }
    }
}
