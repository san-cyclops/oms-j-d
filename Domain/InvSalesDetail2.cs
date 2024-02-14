using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class InvSalesDetail2 : BaseEntity
    {
        public long InvSalesDetail2ID { get; set; }

        [Required]
        public long SalesDetail2ID { get; set; }

        [Required]
        public long InvSalesHeader2ID { get; set; }

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

        [Required]
        [DefaultValue("")]
        [MaxLength(50)]
        public string Descrip { get; set; } //*

        [DefaultValue("")]
        [MaxLength(50)]
        public string ProductCode { get; set; } //*

        [Required]
        [DefaultValue("")]
        [MaxLength(50)]
        public string ProductName { get; set; } //*



        [Required]
        [DefaultValue(0)]
        public long BarCodeFull { get; set; } //*

        [DefaultValue(0)]
        public long UnitOfMeasureID { get; set; }

        [DefaultValue(0)]
        public long BaseUnitID { get; set; }

        [DefaultValue(0)]
        public decimal ConvertFactor { get; set; }

        [DefaultValue("")]
        [MaxLength(25)]
        public string BatchNo { get; set; }

        public DateTime? ExpiryDate { get; set; }

        [DefaultValue("")]
        [MaxLength(50)]
        [Required]
        public string SerialNo { get; set; } //*

        [DefaultValue(0)]
        public decimal OrderQty { get; set; }

        [DefaultValue(0)]
        public decimal Qty { get; set; }

        [DefaultValue(0)]
        public decimal FreeQty { get; set; }

        [DefaultValue(0)]
        public decimal CurrentQty { get; set; }

        [DefaultValue(0)]
        public decimal IssuedQty { get; set; }

        [DefaultValue(0)]
        public decimal BalanceQty { get; set; }

        [DefaultValue(0)]
        public decimal BalanceFreeQty { get; set; }

        [DefaultValue(0)]
        public decimal CostPrice { get; set; }

        [DefaultValue(0)]
        public decimal SellingPrice { get; set; }

        [DefaultValue(0)]
        public decimal AvgCost { get; set; } //*

        [Required]
        [DefaultValue(0)]
        public int IDI1 { get; set; }  // Line Discount 1 Type Id

        [Required]
        [DefaultValue(0)]
        public decimal IDis1 { get; set; } // Line Discount 1 Percentage

        [Required]
        [DefaultValue(0)]
        public decimal IDiscount1 { get; set; } // Line Discount 1 Amount

        [DefaultValue(0)]
        public long IDI1CashierID { get; set; }// Line Discount 1 Given Cashier

        [Required]
        [DefaultValue(0)]
        public int IDI2 { get; set; } //*

        [Required]
        [DefaultValue(0)]
        public decimal IDis2 { get; set; } //*

        [Required]
        [DefaultValue(0)]
        public decimal IDiscount2 { get; set; } //*

        [DefaultValue(0)]
        public long IDI2CashierID { get; set; } //*

        [Required]
        [DefaultValue(0)]
        public int IDI3 { get; set; } //*

        [Required]
        [DefaultValue(0)]
        public decimal IDis3 { get; set; } //*

        [Required]
        [DefaultValue(0)]
        public decimal IDiscount3 { get; set; } //*

        [DefaultValue(0)]
        public long IDI3CashierID { get; set; } //*

        [Required]
        [DefaultValue(0)]
        public decimal IDI4 { get; set; } //*

        [Required]
        [DefaultValue(0)]
        public decimal IDis4 { get; set; } //*

        [Required]
        [DefaultValue(0)]
        public decimal IDiscount4 { get; set; } //*

        [DefaultValue(0)]
        public long IDI4CashierID { get; set; } //*

        [Required]
        [DefaultValue(0)]
        public int IDI5 { get; set; } //*

        [Required]
        [DefaultValue(0)]
        public decimal IDis5 { get; set; } //*

        [Required]
        public decimal IDiscount5 { get; set; } //*

        [DefaultValue(0)]
        public long IDI5CashierID { get; set; } //*

        [DefaultValue(0)]
        public decimal GrossAmount { get; set; } //*

        [Required]
        [DefaultValue(0)]
        public bool IsSDis { get; set; } // SubTotal Discount Applied

        [DefaultValue(0)]
        public int SDNo { get; set; } // SubTotal Discount No

        [DefaultValue(0)]
        public int SDID { get; set; } // SubTotal Discount ID

        [Required]
        [DefaultValue(0)]
        public decimal SDIs { get; set; } // SubTotal Discount Percentage

        [Required]
        [DefaultValue(0)]
        public decimal SDiscount { get; set; } // SubTotal Discount Amount

        [DefaultValue(0)]
        public long DDisCashierID { get; set; } // SubTotal Discount Given Cashier

        [Required]
        [DefaultValue(0)]
        public int BillTypeID { get; set; } //*

        [Required]
        [DefaultValue(0)]
        public int SaleTypeID { get; set; } //*

        [DefaultValue(0)]
        public long SalesmanID { get; set; } //*

        //[Required]
        //[DefaultValue(0)]
        //public long CustomerID { get; set; } //*

        [DefaultValue(0)]
        public long CashierID { get; set; } //*

        [Required]
        public DateTime StartTime { get; set; } //*

        [Required]
        public DateTime EndTime { get; set; } //*

        [Required]
        [DefaultValue(0)]
        public int UnitNo { get; set; } // Terminal No

        [Required]
        [DefaultValue(0)]
        public int ShiftNo { get; set; } // Shift No

        [Required]
        [DefaultValue(0)]
        public bool IsRecall { get; set; } //*

        [MaxLength(20)]
        public string RecallNo { get; set; } //*

        public bool RecallAdv { get; set; } //Is Advanced Invoice

        [Required]
        [DefaultValue(0)]
        public decimal TaxAmount { get; set; } //*

        [Required]
        [DefaultValue(0)]
        public bool IsTax { get; set; } //*

        [Required]
        [DefaultValue(0)]
        public decimal TaxPercentage { get; set; } //*

        [Required]
        [DefaultValue(0)]
        public bool IsStock { get; set; } //*

        [Required]
        [DefaultValue(0)]
        public long UpdateBy { get; set; } //*

        [Required]
        [DefaultValue(0)]
        public int Status { get; set; } // 1 - Valid ******** 0 - Cancelled

        [Required]
        [DefaultValue(0)]
        public long ZNo { get; set; } //*

        public int CustomerType { get; set; } // 1 - Customer // 2 - Staff // 3 - Loyalty Customer

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
        public int CurrencyID { get; set; }

        [DefaultValue(0)]
        public decimal CurrencyAmount { get; set; }

        [DefaultValue(0)]
        public decimal CurrencyRate { get; set; }

        [DefaultValue(0)]
        public bool IsUpLoad { get; set; }

        [DefaultValue(0)]
        public int DocumentStatus { get; set; }

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
