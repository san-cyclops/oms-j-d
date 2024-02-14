using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class ProductLocationInfoTemp
    {
        public string LocationName { get; set; }

        public decimal CostPrice { get; set; }

        public decimal SellingPrice { get; set; }

        public decimal WholesalePrice { get; set; }

        public decimal MinimumPrice { get; set; }

        public decimal Stock { get; set; }

        public decimal CurrentGp { get; set; } 
    }
}
