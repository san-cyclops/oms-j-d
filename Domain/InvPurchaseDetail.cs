using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class InvPurchaseDetail : BaseEntity
    {
        public long InvPurchaseDetailID { get; set; }

        [Required]
        public long PurchaseDetailID { get; set; }

        [Required]
        public long InvPurchaseHeaderID { get; set; }

        [DefaultValue(0)]
        public int CompanyID { get; set; }

        [DefaultValue(0)]
        public int LocationID { get; set; }

        [DefaultValue(0)]
        public int CostCentreID { get; set; }

        [DefaultValue(0)]
        public int DocumentID { get; set; }


        [DefaultValue(0)]
        public long LineNo { get; set; }

        [DefaultValue(0)]
        public long ProductID { get; set; }

        [DefaultValue(0)]
        public long UnitOfMeasureID { get; set; }

        [DefaultValue(0)]
        public long BaseUnitID { get; set; }

        [DefaultValue(0)]
        public decimal ConvertFactor { get; set; }

        public DateTime? ExpiryDate { get; set; }

        [DefaultValue(0)]
        public decimal OrderQty { get; set; }

        [DefaultValue(0)]
        public decimal Qty { get; set; }

        [DefaultValue(0)]
        public decimal FreeQty { get; set; }

        [DefaultValue(0)]
        public decimal CurrentQty { get; set; }

        [DefaultValue(0)]
        public decimal QtyConvertFactor { get; set; }

        [DefaultValue(0)]
        public decimal FreeQtyConvertFactor { get; set; } 

        [DefaultValue(0)]
        public decimal BalanceQty { get; set; }

        [DefaultValue(0)]
        public decimal CostPrice { get; set; }

        [DefaultValue(0)]
        public decimal SellingPrice { get; set; }

        [DefaultValue(0)]
        public decimal AvgCost { get; set; }

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
        public int DocumentStatus { get; set; }

        [MaxLength(50)]
        public string BatchNo { get; set; }

        [DefaultValue(0)]
        public bool IsBatch { get; set; }

        ////public virtual InvPurchaseHeader InvPurchaseHeader { get; set; }

    }
}
