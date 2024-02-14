using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class InvSales : BaseEntity
    {
        public long InvSalesID { get; set; }

        [Required]
        public long SalesID { get; set; }

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
        public bool IsFreeIssue { get; set; }

        [MaxLength(10)]
        public string TerminalNo { get; set; }

        [DefaultValue(0)]
        public bool IsDispatch { get; set; }

        [DefaultValue(0)]
        public bool IsUpLoad { get; set; }

        [DefaultValue(0)]
        public bool IsDelete { get; set; }

        [DefaultValue(0)]
        public int UnitNo { get; set; }

        [DefaultValue(0)]
        public bool IsBackOffice { get; set; }

        [DefaultValue(0)]
        public long Zno { get; set; }

    }
}
