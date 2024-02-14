using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class InvBarcodeDetailTemp
    {
        public long InvBarcodeDetailTempID { get; set; }

        [DefaultValue(0)]
        public int CompanyID { get; set; }

        [DefaultValue(0)]
        public int LocationID { get; set; }

        [DefaultValue(0)]
        public int CostCentreID { get; set; }

        [DefaultValue(0)]
        public int DocumentID { get; set; }

        public DateTime DocumentDate { get; set; }

        [DefaultValue(0)]
        public long LineNo { get; set; }

        [DefaultValue(0)]
        public long ProductID { get; set; }

        [DefaultValue("")]
        [MaxLength(25)]
        public string ProductCode { get; set; }

        [DefaultValue("")]
        [MaxLength(50)]
        public string ProductName { get; set; }

        [DefaultValue("")]
        [MaxLength(15)]
        public string SupplierCode { get; set; }

        [DefaultValue("")]
        [MaxLength(50)]
        public string BatchNo { get; set; }
        
        public long BarCode { get; set; }

        [DefaultValue(0)]
        public long BaseUnitID { get; set; }

        [DefaultValue(0)]
        public decimal ConvertFactor { get; set; }

        public DateTime? ExpiryDate { get; set; }

        [DefaultValue(0)]
        public decimal Qty { get; set; }

        [DefaultValue(0)]
        public decimal Stock { get; set; }

        [DefaultValue(0)]
        public decimal OldCostPrice { get; set; }

        [DefaultValue(0)]
        public decimal CostPrice { get; set; }

        [DefaultValue(0)]
        public decimal OldSellingPrice { get; set; }

        [DefaultValue(0)]
        public decimal SellingPrice { get; set; }

        public long UnitOfMeasureID { get; set; }

        [DefaultValue("")]
        [MaxLength(25)]
        public string UnitOfMeasure { get; set; }

        public string UnitOfMeasureName { get; set; }
    }
}
