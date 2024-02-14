using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class InvTmpSalesProductExtendedProperty : BaseEntity
    {
        public long InvTmpSalesProductExtendedPropertyID { get; set; }

        [DefaultValue(0)]
        public long UserID { get; set; }

        [DefaultValue(0)]
        public int CompanyID { get; set; }

        [Required]
        [MaxLength(15)]
        public string CompanyCode { get; set; }

        [Required]
        [MaxLength(50)]
        public string CompanyName { get; set; }

        [DefaultValue(0)]
        public int LocationID { get; set; }

        [Required]
        [MaxLength(15)]
        public string LocationCode { get; set; }

        [MaxLength(50)]
        public string LocationName { get; set; }

        [DefaultValue(0)]
        public int CostCentreID { get; set; }

        [DefaultValue(0)]
        public int DocumentID { get; set; }

        [DefaultValue("")]
        [MaxLength(20)]
        public string DocumentNo { get; set; }

        [DefaultValue("")]
        [MaxLength(20)]
        public string ReferenceNo { get; set; }

        public DateTime DocumentDate { get; set; }

        public DateTime TransactionTime { get; set; }

        [DefaultValue(0)]
        public int CustomerType { get; set; }

        [DefaultValue(0)]
        public long CustomerID { get; set; }

        [Required]
        [MaxLength(15)]
        public string CustomerCode { get; set; }

        [Required]
        [MaxLength(100)]
        public string CustomerName { get; set; }

        [DefaultValue(0)]
        public long SupplierID { get; set; }

        [DefaultValue("")]
        [MaxLength(15)]
        public string SupplierCode { get; set; }

        [DefaultValue("")]
        [MaxLength(100)]
        public string SupplierName { get; set; }

        [DefaultValue(0)]
        public long SalesPersonID { get; set; }

        [DefaultValue("")]
        [MaxLength(15)]
        public string SalesPersonCode { get; set; }

        [DefaultValue("")]
        [MaxLength(50)]
        public string SalesPersonName { get; set; }

        [DefaultValue(0)]
        public decimal GrossAmount { get; set; }

        [DefaultValue(0)]
        public decimal DiscountPercentage { get; set; }

        [DefaultValue(0)]
        public decimal DiscountAmount { get; set; }

        [DefaultValue(0)]
        public decimal NetAmount { get; set; }

        [DefaultValue(0)]
        public decimal SubTotalDiscountPercentage { get; set; }

        [DefaultValue(0)]
        public decimal SubTotalDiscountAmount { get; set; }

        [DefaultValue(0)]
        public int CurrencyID { get; set; }

        [DefaultValue(0)]
        public decimal CurrencyRate { get; set; }

        [DefaultValue(0)]
        public long DepartmentID { get; set; }

        [MaxLength(15)]
        public string DepartmentCode { get; set; }

        [MaxLength(50)]
        public string DepartmentName { get; set; }

        [DefaultValue(0)]
        public long CategoryID { get; set; }

        [MaxLength(15)]
        public string CategoryCode { get; set; }

        [MaxLength(50)]
        public string CategoryName { get; set; }

        [DefaultValue(0)]
        public long SubCategoryID { get; set; }

        [MaxLength(15)]
        public string SubCategoryCode { get; set; }

        [MaxLength(50)]
        public string SubCategoryName { get; set; }

        [DefaultValue(0)]
        public long SubCategory2ID { get; set; }

        [MaxLength(15)]
        public string SubCategory2Code { get; set; }

        [MaxLength(50)]
        public string SubCategory2Name { get; set; }

        [Required]
        public long ProductID { get; set; }

        [Required]
        [MaxLength(25)]
        public string ProductCode { get; set; }

        [MaxLength(100)]
        public string ProductName { get; set; }

        [MaxLength(25)]
        public string BarCode { get; set; }

        [Required]
        [MaxLength(40)]
        public string BatchNo { get; set; }

        [DefaultValue(0)]
        public DateTime? ExpiryDate { get; set; }

        [DefaultValue(0)]
        public decimal Qty { get; set; }

        [DefaultValue(0)]
        public long UnitOfMeasureID { get; set; }

        [Required]
        [MaxLength(50)]
        public string UnitOfMeasureName { get; set; }

        [Required]
        [MaxLength(25)]
        public string PackSize { get; set; }

        [DefaultValue(0)]
        [Required]
        public decimal SellingPrice { get; set; }

        [DefaultValue(0)]
        [Required]
        public decimal WholeSalePrice { get; set; }

        [DefaultValue(0)]
        [Required]
        public decimal CostPrice { get; set; }

        [DefaultValue(0)]
        [Required]
        public decimal AverageCost { get; set; }

        [DefaultValue(0)]
        public int DocumentStatus { get; set; }

        [DefaultValue(0)]
        public bool IsDelete { get; set; }

        [DefaultValue(0)]
        public int UnitNo { get; set; }

        [MaxLength(25)]
        public string Expr1 { get; set; }

        [MaxLength(25)]
        public string Expr2 { get; set; }

        [MaxLength(25)]
        public string Expr3 { get; set; }

        [MaxLength(25)]
        public string Expr4 { get; set; }
        
        [MaxLength(25)]
        public string Expr5 { get; set; }

        [MaxLength(25)]
        public string Expr6 { get; set; }

        [MaxLength(25)]
        public string Expr7 { get; set; }

        [MaxLength(25)]
        public string Expr8 { get; set; }

        [MaxLength(25)]
        public string Expr9 { get; set; }

        [MaxLength(25)]
        public string Expr10 { get; set; }

        [MaxLength(25)]
        public string Expr11 { get; set; }
        
        [MaxLength(25)]
        public string Expr12 { get; set; }
        
        [MaxLength(25)]
        public string Expr13 { get; set; }

        [MaxLength(25)]
        public string Expr14 { get; set; }

        [MaxLength(25)]
        public string Expr15 { get; set; }

        [MaxLength(25)]
        public string Expr16 { get; set; }

        [MaxLength(25)]
        public string Expr17 { get; set; }

        [MaxLength(25)]
        public string Expr18 { get; set; }

        [MaxLength(25)]
        public string Expr19 { get; set; }

        [MaxLength(25)]
        public string Expr20 { get; set; }

    }
}
