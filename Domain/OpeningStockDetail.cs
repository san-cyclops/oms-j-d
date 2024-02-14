using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class OpeningStockDetail : BaseEntity
    {
        public long OpeningStockDetailID { get; set; }

        [DefaultValue(0)]
        public long StockDetailID { get; set; }   

        [DefaultValue(0)]
        public long OpeningStockHeaderID { get; set; } 

        [DefaultValue(0)]
        public int CompanyID { get; set; }

        [DefaultValue(0)]
        public int LocationID { get; set; }

        [DefaultValue(0)]
        public int CostCentreID { get; set; }

        [DefaultValue(0)]
        public long ProductID { get; set; }

        [DefaultValue(0)]
        public int DocumentID { get; set; }

        public DateTime DocumentDate { get; set; }

        //[DefaultValue(0)]
        //public int OpeningStockType { get; set; }

        [DefaultValue(0)]
        public decimal OrderQty { get; set; }

        [DefaultValue("")]
        [MaxLength(50)]
        public string BatchNo { get; set; }

        [DefaultValue(0)]
        public long BaseUnitID { get; set; }

        [DefaultValue(0)]
        public decimal ConvertFactor { get; set; }

        public DateTime? ExpiryDate { get; set; }

        [DefaultValue(0)]
        public decimal CostPrice { get; set; }

        [DefaultValue(0)]
        public decimal SellingPrice { get; set; }

        [DefaultValue(0)]
        public long UnitOfMeasureID { get; set; }

        [DefaultValue(0)]
        public decimal CostValue { get; set; }

        [DefaultValue(0)]
        public decimal SellingValue { get; set; }

        [DefaultValue(0)]
        public long LineNo { get; set; }

        [DefaultValue(0)]
        public int DocumentStatus { get; set; }
    }
}
