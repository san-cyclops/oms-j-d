using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class LgsProductMaster : BaseEntity
    {
        public long LgsProductMasterID { get; set; } 

        [Required]
        [MaxLength(25)]
        public string ProductCode { get; set; }

        [MaxLength(25)]
        public string BarCode { get; set; }

        [MaxLength(25)]
        public string ReferenceCode1 { get; set; }

        [MaxLength(25)]
        public string ReferenceCode2 { get; set; }

        [Required]
        [MaxLength(100)]
        public string ProductName { get; set; }

        [Required]
        [MaxLength(50)]
        public string NameOnInvoice { get; set; }

        [Required]
        public long DepartmentID { get; set; }

        [Required]
        public long CategoryID { get; set; }

        [Required]
        public long SubCategoryID { get; set; }

        [Required]
        public long SubCategory2ID { get; set; }

        [Required]
        public long LgsSupplierID { get; set; } 

        [Required]
        public long UnitOfMeasureID { get; set; }

        [Required]
        [MaxLength(25)]
        public string PackSize { get; set; }

        public byte[] ProductImage { get; set; }

        [MaxLength(50)]
        public string CostingMethod { get; set; }

        [DefaultValue(0)]
        [Required]
        public decimal CostPrice { get; set; }

        [DefaultValue(0)]
        [Required]
        public decimal OrderPrice { get; set; } 

        [DefaultValue(0)]
        [Required]
        public decimal AverageCost { get; set; }

        [DefaultValue(0)]
        [Required]
        public decimal SellingPrice { get; set; }

        [DefaultValue(0)]
        [Required]
        public decimal WholesalePrice { get; set; }

        [DefaultValue(0)]
        [Required]
        public decimal MinimumPrice { get; set; }

        [DefaultValue(0)]
        [Required]
        public decimal FixedDiscount { get; set; }

        [DefaultValue(0)]
        [Required]
        public decimal MaximumDiscount { get; set; }

        [DefaultValue(0)]
        [Required]
        public decimal MaximumPrice { get; set; }

        [DefaultValue(0)]
        [Required]
        public decimal FixedDiscountPercentage { get; set; }

        [DefaultValue(0)]
        [Required]
        public decimal MaximumDiscountPercentage { get; set; }

        [DefaultValue(0)]
        [Required]
        public decimal ReOrderLevel { get; set; }

        [DefaultValue(0)]
        [Required]
        public decimal ReOrderQty { get; set; }

        [DefaultValue(0)]
        [Required]
        public decimal ReOrderPeriod { get; set; }

        [DefaultValue(0)]
        public bool IsActive { get; set; }

        [DefaultValue(0)]
        public bool IsBatch { get; set; }

        [DefaultValue(0)]
        public bool IsPromotion { get; set; }

        [DefaultValue(0)]
        public bool IsBundle { get; set; }

        [DefaultValue(0)]
        public bool IsFreeIssue { get; set; }

        [DefaultValue(0)]
        public bool IsDrayage { get; set; }

        [DefaultValue(0)]
        public decimal DrayagePercentage { get; set; }

        [DefaultValue(0)]
        public bool IsExpiry { get; set; }

        [DefaultValue(0)]
        public bool IsConsignment { get; set; }

        [DefaultValue(0)]
        public bool IsCountable { get; set; }

        [DefaultValue(0)]
        public bool IsDCS { get; set; }

        [DefaultValue(0)]
        public long DcsID { get; set; }

        [DefaultValue(0)]
        public bool IsTax { get; set; }

        [DefaultValue(0)]
        public bool IsSerial { get; set; }

        [DefaultValue(0)]
        public bool IsDelete { get; set; }

        [DefaultValue(0)]
        public bool IsService { get; set; }

        [MaxLength(500)]
        public string Remarks { get; set; }  

        [DefaultValue(0)]
        public long PackSizeUnitOfMeasureID { get; set; }

        [DefaultValue(0)]
        public decimal Margin { get; set; }

        [DefaultValue(0)]
        public decimal WholesaleMargin { get; set; }

        [DefaultValue(0)]
        [Required]
        public decimal FixedGP { get; set; }

        [DefaultValue(0)]
        public long PurchaseLedgerID { get; set; }

        [DefaultValue(0)]
        public long SalesLedgerID { get; set; }

        [DefaultValue(0)]
        public long OtherPurchaseLedgerID { get; set; }

        [DefaultValue(0)]
        public long OtherSalesLedgerID { get; set; } 

        //public virtual ICollection<LgsProductStockMaster> LgsProductStockMastes { get; set; }
        //public virtual ICollection<LgsProductUnitConversion> LgsProductUnitConversions { get; set; }
        //public virtual LgsDepartment LgsDepartment { get; set; }
        //public virtual LgsCategory LgsCategory { get; set; }
        //public virtual LgsSubCategory LgsSubCategory { get; set; }
        //public virtual LgsSubCategory2 LgsSubCategory2 { get; set; }
        public virtual LgsSupplier LgsSupplier { get; set; } 
        public virtual UnitOfMeasure UnitOfMeasure { get; set; }
        //public virtual Location Location { get; set; }
    }
}
