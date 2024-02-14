using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class TransactionDet
    {
        public long TransactionDetID { get; set; }

        [Required]
        public Int64 ProductID { get; set; }

        [DefaultValue("")]
        [MaxLength(25)]
        [Required]
        public string ProductCode { get; set; }

        [DefaultValue("")]
        [MaxLength(25)]
        [Required]
        public string RefCode { get; set; }

        [Required]
        [DefaultValue(0)]
        public Int64 BarCodeFull { get; set; }
        
        [Required]
        [DefaultValue("")]
        [MaxLength(50)]
        public string Descrip { get; set; }

        [DefaultValue("")]
        [MaxLength(50)]
        [Required]
        public string BatchNo { get; set; }

        [DefaultValue("")]
        [MaxLength(50)]
        [Required]
        public string SerialNo { get; set; }

        public DateTime ?ExpiryDate { get; set; }

        [Required]
        [DefaultValue(0)]
        public decimal Cost { get; set; }

        [Required]
        [DefaultValue(0)]
        public decimal AvgCost { get; set; }

        [Required]
        [DefaultValue(0)]
        public decimal Price { get; set; }

        [Required]
        [DefaultValue(0)]
        public decimal Qty { get; set; }

        [DefaultValue(0)]
        public decimal BalanceQty { get; set; }

        [Required]
        [DefaultValue(0)]
        public decimal Amount { get; set; } //Gross Amount

        public long UnitOfMeasureID { get; set; }

        [MaxLength(10)]
        public string  UnitOfMeasureName { get; set; }
        
        [Required]
        [DefaultValue(0)]
        public decimal ConvertFactor { get; set; }

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
        public Int64 IDI1CashierID { get; set; }// Line Discount 1 Given Cashier

        [Required]
        [DefaultValue(0)]
        public int IDI2 { get; set; }

        [Required]
        [DefaultValue(0)]
        public decimal IDis2 { get; set; }
        
        [Required]
        [DefaultValue(0)]
        public decimal IDiscount2 { get; set; }

        [DefaultValue(0)]
        public Int64 IDI2CashierID { get; set; }

        [Required]
        [DefaultValue(0)]
        public int IDI3 { get; set; }

        [Required]
        [DefaultValue(0)]
        public decimal IDis3 { get; set; }

        [Required]
        [DefaultValue(0)]
        public decimal IDiscount3 { get; set; }

        [DefaultValue(0)]
        public Int64 IDI3CashierID { get; set; }

        [Required]
        [DefaultValue(0)]
        public decimal IDI4 { get; set; }

        [Required]
        [DefaultValue(0)]
        public decimal IDis4 { get; set; }

        [Required]
        [DefaultValue(0)]
        public decimal IDiscount4 { get; set; }

        [DefaultValue(0)]
        public Int64 IDI4CashierID { get; set; }

        [Required]
        [DefaultValue(0)]
        public int IDI5 { get; set; }

        [Required]
        [DefaultValue(0)]
        public decimal IDis5 { get; set; }

        [Required]
        public decimal IDiscount5 { get; set; }

        [DefaultValue(0)]
        public Int64 IDI5CashierID { get; set; }

        [Required]
        [DefaultValue(0)]
        public decimal Rate { get; set; }

        [Required]
        [DefaultValue(0)]
        public bool  IsSDis { get; set; } // SubTotal Discount Applied

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
        public Int64 DDisCashierID { get; set; } // SubTotal Discount Given Cashier

        [Required]
        [DefaultValue(0)]
        public decimal Nett { get; set; } //Net Amount

        [Required]
        [DefaultValue(0)]
        public int LocationID { get; set; }

        [Required]
        [DefaultValue(0)]
        public int DocumentID { get; set; }

        [Required]
        [DefaultValue(0)]
        public int BillTypeID { get; set; }

        [Required]
        [DefaultValue(0)]
        public int SaleTypeID { get; set; }

        [MaxLength(10)]
        [Required]
        public string Receipt { get; set; } // Invoice No

        [DefaultValue(0)]
        public Int64 SalesmanID { get; set; }

        [MaxLength(15)]
        [DefaultValue("")]
        public string Salesman { get; set; }

        [Required]
        [DefaultValue(0)]
        public Int64 CustomerID { get; set; }

        [MaxLength(150)]
        [DefaultValue("")]
        public string Customer { get; set; }

        [DefaultValue(0)]
        public Int64 CashierID { get; set; }

        [MaxLength(15)]
        [DefaultValue("")]
        public string Cashier { get; set; }

        [Required]
        public DateTime StartTime { get; set; }

        [Required]
        public DateTime EndTime { get; set; }

        [Required]
        public DateTime RecDate { get; set; }

        [Required]
        [DefaultValue(0)]
        public Int64 BaseUnitID { get; set; }

        [Required]
        [DefaultValue(0)]
        public int UnitNo { get; set; }

        [Required]
        [DefaultValue(0)]
        public int RowNo { get; set; }

        [Required]
        [DefaultValue(0)]
        public bool IsRecall { get; set; }

        [MaxLength(20)]
        public string RecallNO { get; set; }


        public bool RecallAdv { get; set; }

        [Required]
        [DefaultValue(0)]
        public decimal TaxAmount { get; set; }

        [Required]
        [DefaultValue(0)]
        public bool IsTax { get; set; }

        [Required]
        [DefaultValue(0)]
        public decimal TaxPercentage { get; set; }

        [Required]
        [DefaultValue(0)]
        public bool IsStock { get; set; }
            
        [Required]
        [DefaultValue(0)]
        public Int64 UpdateBy { get; set; }

        [Required]
        [DefaultValue(0)]
        public int Status { get; set; }

        [Required]
        [DefaultValue(0)]
        public Int64 ZNo { get; set; }

        [DefaultValue(0)]
        public int GroupOfCompanyID { get; set; }
        
        [DefaultValue(0)]
        public int DataTransfer { get; set; }

        public int CustomerType { get; set; }

        public int TransStatus { get; set; }

        public DateTime? ZDate { get; set; } 
    }
}
