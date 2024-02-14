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
    public class InvTmpProductStockDetails : BaseEntity
    {
        public long InvTmpProductStockDetailsID { get; set; }

        [DefaultValue(0)]
        public int CompanyID { get; set; }

        [DefaultValue(0)]
        public int LocationID { get; set; }

        [DefaultValue("")]
        [MaxLength(50)]
        public string ToLocationName { get; set; }

        public DateTime GivenDate { get; set; }

        [DefaultValue(0)]
        public long ProductID { get; set; }

        [DefaultValue("")]
        [MaxLength(20)]
        public string ProductCode { get; set; }

        [DefaultValue("")]
        [MaxLength(100)]
        public string ProductName { get; set; }

        [DefaultValue(0)]
        public int TransactionType { get; set; }

        [DefaultValue("")]
        [MaxLength(20)]
        public string TransactionNo { get; set; }

        [DefaultValue("")]
        [MaxLength(25)]
        public string BatchNo { get; set; }

        public DateTime TransactionDate { get; set; }

        [DefaultValue(0)]
        public decimal CostPrice { get; set; }

        [DefaultValue(0)]
        public decimal SellingPrice { get; set; }

        [DefaultValue(0)]
        public decimal AverageCost { get; set; }

        [DefaultValue(0)]
        public long DepartmentID { get; set; }

        [DefaultValue(0)]
        public long CategoryID { get; set; }

        [DefaultValue(0)]
        public long SubCategoryID { get; set; }

        [DefaultValue(0)]
        public long SubCategory2ID { get; set; }

        [DefaultValue(0)]
        public long SupplierID { get; set; }

        [DefaultValue(0)]
        public long CustomerID { get; set; } 

        [DefaultValue(0)]
        public decimal StockQty { get; set; }

        [DefaultValue(0)]
        public decimal Qty1 { get; set; }

        [DefaultValue(0)]
        public decimal Qty2 { get; set; }

        [DefaultValue(0)]
        public decimal Qty3 { get; set; }

        [DefaultValue(0)]
        public decimal Qty4 { get; set; }

        [DefaultValue(0)]
        public decimal Qty5 { get; set; }

        [DefaultValue(0)]
        public decimal Qty6 { get; set; }

        [DefaultValue(0)]
        public decimal Qty7 { get; set; }

        [DefaultValue(0)]
        public decimal Qty8 { get; set; }

        [DefaultValue(0)]
        public decimal Qty9 { get; set; }

        [DefaultValue(0)]
        public decimal Qty10 { get; set; }  

        [DefaultValue(0)]
        public long UserID { get; set; }

        [DefaultValue(0)]
        public long UniqueID { get; set; }

        [DefaultValue(0)]
        public decimal GrossProfit { get; set; }

        [DefaultValue(0)]
        public bool IsDelete { get; set; }

    }
}
