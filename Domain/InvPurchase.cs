using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class InvPurchase : BaseEntity
    {
        public long InvPurchaseID { get; set; }

        [Required]
        public long PurchaseID { get; set; }

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

        
        [DefaultValue(0)]
        public long SupplierID { get; set; }

        [DefaultValue("")]
        [MaxLength(15)]
        public string SupplierCode { get; set; }

        [DefaultValue("")]
        [MaxLength(100)]
        public string SupplierName { get; set; }


        [DefaultValue(0)]
        public decimal GrossAmount { get; set; }

        [DefaultValue(0)]
        public decimal DiscountPercentage { get; set; }

        [DefaultValue(0)]
        public decimal DiscountAmount { get; set; }

        [DefaultValue(0)]
        public decimal NetAmount { get; set; }

        [DefaultValue(0)]
        public int CurrencyID { get; set; }

        [DefaultValue(0)]
        public decimal CurrencyRate { get; set; }

        [MaxLength(15)]
        public string DepartmentCode { get; set; }

        [MaxLength(50)]
        public string DepartmentName { get; set; }

        [MaxLength(15)]
        public string CategoryCode { get; set; }

        [MaxLength(50)]
        public string CategoryName { get; set; }

        [MaxLength(15)]
        public string SubCategoryCode { get; set; }

        [MaxLength(50)]
        public string SubCategoryName { get; set; }

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

        [DefaultValue(0)]
        public bool IsDispatch { get; set; }

        [DefaultValue(0)]
        public bool IsUpLoad { get; set; }

        [DefaultValue(0)]
        public bool IsDelete { get; set; }
    }
}
