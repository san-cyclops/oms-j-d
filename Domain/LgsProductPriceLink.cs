using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class LgsProductPriceLink : BaseEntity
    {
        public long LgsProductPriceLinkID { get; set; } 

        [DefaultValue(0)]
        public int CompanyID { get; set; }

        [DefaultValue(0)]
        public int LocationID { get; set; }

        [DefaultValue(0)]
        public int CostCentreID { get; set; }

        [DefaultValue(0)]
        public long ProductID { get; set; }

        [DefaultValue("")]
        [MaxLength(15)]
        public string CreatedLocation { get; set; }

        [DefaultValue(0)]
        public decimal Price { get; set; }

        [DefaultValue(0)]
        public bool IsDelete { get; set; }
    }
}
