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
    public class Tax : BaseEntity
    {
        public int TaxID { get; set; } 

        [Required]
        [MaxLength(10)]
        public string TaxCode { get; set; }

        [Required]
        [MaxLength(50)]
        public string TaxName { get; set; }

        [DefaultValue(0)]
        public decimal TaxPercentage { get; set; }

        [DefaultValue(0)]
        public decimal EffectivePercentage { get; set; } 

        [DefaultValue(0)]
        public bool Tax1 { get; set; }

        [DefaultValue(0)]
        public bool Tax2 { get; set; }

        [DefaultValue(0)]
        public bool Tax3 { get; set; }

        [DefaultValue(0)]
        public bool Tax4 { get; set; }

        [DefaultValue(0)]
        public bool Tax5 { get; set; }

        public int PrintOrder { get; set; }

        [DefaultValue(0)]
        public long LedgerID { get; set; }

        [DefaultValue(0)]
        public bool IsDelete { get; set; } 
    }
}
