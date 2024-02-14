using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class SampleOutDetail : BaseEntity
    {
        public long SampleOutDetailID { get; set; }

        [DefaultValue(0)]
        public long SampleDetailID { get; set; } 

        [DefaultValue(0)]
        public long SampleOutHeaderID { get; set; } 

        [DefaultValue(0)]
        public int CompanyID { get; set; }

        [DefaultValue(0)]
        public int LocationID { get; set; }

        [DefaultValue(0)]
        public int CostCentreID { get; set; }

        [DefaultValue(0)]
        public long ProductID { get; set; }

        [DefaultValue(0)]
        public string BatchNo { get; set; }

        [DefaultValue(0)]
        public int DocumentID { get; set; }

        [DefaultValue(0)]
        public decimal OrderQty { get; set; }

        [DefaultValue(0)]
        public decimal BalancedQty { get; set; }  

        [DefaultValue(0)]
        public decimal CostPrice { get; set; }

        [DefaultValue(0)]
        public decimal AverageCost { get; set; }

        [DefaultValue(0)]
        public long UnitOfMeasureID { get; set; }

        [DefaultValue(0)]
        public decimal ConvertFactor { get; set; }

        [DefaultValue(0)]
        public decimal GrossAmount { get; set; }

        [DefaultValue(0)]
        public long BaseUnitID { get; set; }

        [DefaultValue(0)]
        public decimal NetAmount { get; set; }

        [DefaultValue(0)]
        public long LineNo { get; set; }

        [DefaultValue(0)]
        public bool IsDelete { get; set; }

        [DefaultValue(0)]
        public int DocumentStatus { get; set; }
    }
}
