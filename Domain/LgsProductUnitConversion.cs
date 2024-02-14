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
    public class LgsProductUnitConversion : BaseEntity
    {
        public long LgsProductUnitConversionID { get; set; } 

        [Required]
        public long ProductID { get; set; }

        [Required]
        [MaxLength(100)]
        public string Description { get; set; }

        [Required]
        public long UnitOfMeasureID { get; set; }

        [NotMapped]
        public string UnitOfMeasureName { get; set; }

        [DefaultValue(0)]
        [Required]
        public decimal ConvertFactor { get; set; }

        [DefaultValue(0)]
        [Required]
        public decimal CostPrice { get; set; }

        [DefaultValue(0)]
        [Required]
        public decimal SellingPrice { get; set; }

        [DefaultValue(0)]
        [Required]
        public decimal MinimumPrice { get; set; }

        [DefaultValue(0)]
        public bool IsDelete { get; set; }

        [DefaultValue(0)]
        public int LineNo { get; set; }

        //public virtual LgsProductMaster LgsProductMaster { get; set; } 
    }
}
