using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class InvSalesDetailTemp
    {
        public long InvSalesDetailTempID { get; set; }

        public long InvSalesDetailID { get; set; }
        [DefaultValue(0)]
        public int CompanyID { get; set; }

        [DefaultValue(0)]
        public int LocationID { get; set; }

        [DefaultValue(0)]
        public int CostCentreID { get; set; }

        [DefaultValue(0)]
        public int DocumentID { get; set; }

        [DefaultValue("")]
        [MaxLength(20)]
        public string DocumentNo { get; set; }

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

        [DefaultValue(0)]
        public long UnitOfMeasureID { get; set; }

        [DefaultValue(0)]
        public long BaseUnitID { get; set; }

        [DefaultValue("")]
        [MaxLength(25)]
        public string UnitOfMeasure { get; set; }


        [DefaultValue(1)]
        public decimal ConvertFactor { get; set; }

        [DefaultValue("")]
        [MaxLength(25)]
        public string BatchNo { get; set; }

        [DefaultValue(0)]
        public bool IsBatch { get; set; }

        public DateTime? ExpiryDate { get; set; }

  
        [DefaultValue(0)]
        public decimal Qty { get; set; }

        [DefaultValue(0)]
        public decimal BalanceQty { get; set; }

        [DefaultValue(0)]
        public decimal IssuedQty { get; set; }

        [DefaultValue(0)]
        public decimal DispatchQty { get; set; }

        [DefaultValue(0)]
        public decimal FreeQty { get; set; }

        [DefaultValue(0)]
        public decimal CurrentQty { get; set; }

        [DefaultValue(0)]
        public decimal Rate { get; set; }

        [DefaultValue(0)]
        public decimal AverageCost { get; set; }

        [DefaultValue(0)]
        public decimal GrossAmount { get; set; }

        [DefaultValue(0)]
        public decimal DiscountPercentage { get; set; }

        [DefaultValue(0)]
        public decimal DiscountAmount { get; set; }

        [DefaultValue(0)]
        public decimal SubTotalDiscount { get; set; }


        [DefaultValue(0)]
        public decimal TaxAmount1 { get; set; }

        [DefaultValue(0)]
        public decimal TaxAmount2 { get; set; }

        [DefaultValue(0)]
        public decimal TaxAmount3 { get; set; }

        [DefaultValue(0)]
        public decimal TaxAmount4 { get; set; }

        [DefaultValue(0)]
        public decimal TaxAmount5 { get; set; }

        [DefaultValue(0)]
        public decimal NetAmount { get; set; }

        [DefaultValue(0)]
        public int DispatchLocationID { get; set; }

        [DefaultValue(0)]
        public int DocumentStatus { get; set; }

        [DefaultValue("")]
        [MaxLength(15)]
        public string DispatchLocation { get; set; }

        [DefaultValue("")]
        [MaxLength(50)]
        public string Size { get; set; }

        [DefaultValue("")]
        [MaxLength(50)]
        public string Gauge { get; set; }


        [DefaultValue(0)]
        public bool IsDeliver { get; set; }


        [DefaultValue(0)]
        public decimal Waight { get; set; }

        [DefaultValue(0)]
        public decimal Height { get; set; }

    }
}
