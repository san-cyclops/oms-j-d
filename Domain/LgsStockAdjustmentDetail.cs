using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class LgsStockAdjustmentDetail : BaseEntity 
    {
        public long LgsStockAdjustmentDetailID { get; set; }

        [DefaultValue(0)]
        public long StockAdjustmentDetailID { get; set; }  

        [DefaultValue(0)]
        public long LgsStockAdjustmentHeaderID { get; set; } 

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

        public DateTime? DocumentDate { get; set; }

        [DefaultValue(0)]
        public int StockAdjustmentMode { get; set; }

        [DefaultValue(0)]
        public decimal CurrentQty { get; set; }

        [DefaultValue(0)]
        public decimal BatchQty { get; set; }

        [DefaultValue(0)]
        public decimal OrderQty { get; set; }

        [DefaultValue(0)]
        public decimal ExcessQty { get; set; }

        [DefaultValue(0)]
        public decimal ShortageQty { get; set; }

        [DefaultValue(0)]
        public decimal OverWriteQty { get; set; }

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
