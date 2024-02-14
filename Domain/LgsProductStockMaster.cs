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
    public class LgsProductStockMaster : BaseEntity
    {
        public long LgsProductStockMasterID { get; set; } 

        [DefaultValue(0)]
        public int CompanyID { get; set; }

        [DefaultValue(0)]
        public int LocationID { get; set; }

        [NotMapped]
        public string LocationName { get; set; }

        [DefaultValue(0)]
        public int CostCentreID { get; set; }

        [DefaultValue(0)]
        public long ProductID { get; set; }

        [DefaultValue(0)]
        public decimal Stock { get; set; }

        [DefaultValue(0)]
        public decimal CostPrice { get; set; }

        [DefaultValue(0)]
        public decimal SellingPrice { get; set; }

        [DefaultValue(0)]
        public decimal MinimumPrice { get; set; }

        [DefaultValue(0)]
        public decimal ReOrderLevel { get; set; }

        [DefaultValue(0)]
        public decimal ReOrderQuantity { get; set; }

        [DefaultValue(0)]
        public decimal ReOrderPeriod { get; set; }

        [DefaultValue(0)]
        public bool IsDelete { get; set; }

        //public virtual LgsProductMaster LgsProductMaster { get; set; } 
    }
}
