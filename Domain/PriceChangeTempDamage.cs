using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class PriceChangeTempDamage
    {

        [NotMapped]
        public long ProductID { get; set; }

        [NotMapped]
        public string ProductCode { get; set; }

        [NotMapped]
        public string ProductName { get; set; }

        [NotMapped]
        public long UnitOfMeasureID { get; set; }

        [NotMapped]
        public string UnitOfMeasureName { get; set; }

        [NotMapped]
        public long NewProductID { get; set; }

        [NotMapped]
        public string NewProductCode { get; set; }

        [NotMapped]
        public string NewProductName { get; set; }
 

        [NotMapped]
        public decimal Qty { get; set; }

        [NotMapped]
        public decimal MRP { get; set; }

        [NotMapped]
        public decimal CostPrice { get; set; }

        [NotMapped]
        public decimal NewCostPrice { get; set; }

        [NotMapped]
        public decimal SellingPrice { get; set; }

        [NotMapped]
        public decimal NewSellingPrice { get; set; }

        [NotMapped]
        public long LocationId { get; set; }

        [NotMapped]
        public string LocationName { get; set; }

        [NotMapped]
        [DefaultValue(0)]
        public long LineNo { get; set; }

        [NotMapped]
        public bool Selection { get; set; }

        [NotMapped]
        public string DamageType  { get; set; }

 

    }
}
