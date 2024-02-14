using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class BankBin : BaseEntity
    {
        public long BankBinID { get; set; }

        [MaxLength(10)]
        [DefaultValue("")]
        public string CardPfx { get; set; }

        //[MaxLength(75)]
        [MaxLength(10)]
        [DefaultValue("")]
        public string CardName { get; set; }

        //[MaxLength(75)]
        [MaxLength(10)]
        [DefaultValue("")]
        public string CardType { get; set; }

        [DefaultValue(0)]
        public long CardID { get; set; } 

        [DefaultValue(0)]
        public long BankID { get; set; }

        //[MaxLength(75)]
        [MaxLength(20)]
        [DefaultValue("")]
        public string BankName { get; set; } 

        [DefaultValue(0)]
        public decimal Rate { get; set; }

        [DefaultValue(0)]
        public decimal DiscountPercentage { get; set; }

        public DateTime DateFrom { get; set; }

        public DateTime DateTo { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        [DefaultValue(0)]
        public decimal ValueFrom { get; set; }

        [DefaultValue(0)]
        public decimal ValueTo { get; set; }

        [DefaultValue(0)]
        public decimal DiscountAmount { get; set; }

        [DefaultValue(0)]
        public bool IsValidForGVSales { get; set; }

        [DefaultValue(0)]
        public bool IsCombined { get; set; }  
    }
}
