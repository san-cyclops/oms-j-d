using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class LgsMinusProductDetailsTemp
    {
        public long LgsMinusProductDetailsTempID { get; set; } 

        [DefaultValue(0)]
        public int CompanyID { get; set; }

        [DefaultValue(0)]
        public int LocationID { get; set; }

        [DefaultValue(0)]
        public int CostCentreID { get; set; }

        [DefaultValue(0)]
        public long ProductID { get; set; }

        [MaxLength(25)]
        public string ProductCode { get; set; }

        [MaxLength(100)]
        public string ProductName { get; set; }

        [MaxLength(15)]
        public string UnitOfMeasure { get; set; }

        [DefaultValue(0)]
        public long LineNo { get; set; }

        [DefaultValue(0)]
        public decimal Qty { get; set; }

        [DefaultValue(0)]
        public long UnitOfMeasureID { get; set; }

        [DefaultValue(0)]
        public bool IsDelete { get; set; }   
    }
}
