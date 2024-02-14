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
    public class InvTmpReportDetail : BaseEntity
    {
        public long InvTmpReportDetailID { get; set; }

        [DefaultValue(0)]
        public int CompanyID { get; set; }

        [DefaultValue(0)]
        public int LocationID { get; set; }

        [DefaultValue(0)]
        public long UserID { get; set; }

        public DateTime DocumentDate { get; set; }

        [DefaultValue("")]
        [MaxLength(20)]
        public string DocumentNo { get; set; }


        [DefaultValue(0)]
        public int UnitNo { get; set; }


        [DefaultValue(0)]
        public long ZNo { get; set; }


        [DefaultValue(0)]
        public long SupplierID { get; set; }

        [DefaultValue("")]
        [MaxLength(20)]
        public string SupplierCode { get; set; }

        [DefaultValue("")]
        [MaxLength(100)]
        public string SupplierName { get; set; }

        [DefaultValue(0)]
        public long CustomerID { get; set; }

        [DefaultValue("")]
        [MaxLength(20)]
        public string CustomerCode { get; set; }

        [DefaultValue("")]
        [MaxLength(100)]
        public string CustomerName { get; set; }

        [DefaultValue(0)]
        public long ProductID { get; set; }

        [DefaultValue("")]
        [MaxLength(20)]
        public string ProductCode { get; set; }

        [DefaultValue("")]
        [MaxLength(100)]
        public string ProductName { get; set; }

        [DefaultValue(0)]
        public long CategoryID { get; set; }

        [DefaultValue("")]
        [MaxLength(20)]
        public string CategoryCode { get; set; }

        [DefaultValue("")]
        [MaxLength(100)]
        public string CategoryName { get; set; }

        [DefaultValue(0)]
        public long DepartmentID { get; set; }

        [DefaultValue("")]
        [MaxLength(20)]
        public string DepartmentCode { get; set; }

        [DefaultValue("")]
        [MaxLength(100)]
        public string DepartmentName { get; set; }

        [DefaultValue(0)]
        public long SubCategoryID { get; set; }

        [DefaultValue("")]
        [MaxLength(20)]
        public string SubCategoryCode { get; set; }

        [DefaultValue("")]
        [MaxLength(100)]
        public string SubCategoryName { get; set; }

        [DefaultValue(0)]
        public long SubCategory2ID { get; set; }

        [DefaultValue("")]
        [MaxLength(20)]
        public string SubCategory2Code { get; set; }

        [DefaultValue("")]
        [MaxLength(100)]
        public string SubCategory2Name { get; set; }

        [DefaultValue("")]
        [MaxLength(25)]
        public string BatchNo { get; set; }

        [DefaultValue(0)]
        public decimal CostPrice { get; set; }

        [DefaultValue(0)]
        public decimal SellingPrice { get; set; }

        [DefaultValue(0)]
        public decimal AverageCost { get; set; }

        [DefaultValue(0)]
        public decimal GrossProfit { get; set; }

        [DefaultValue(0)]
        public decimal Qty01 { get; set; }

        [DefaultValue(0)]
        public decimal Value01 { get; set; }

        [DefaultValue(0)]
        public decimal Qty02 { get; set; }

        [DefaultValue(0)]
        public decimal Value02 { get; set; }

        [DefaultValue(0)]
        public decimal Qty03 { get; set; }

        [DefaultValue(0)]
        public decimal Value03 { get; set; }

        [DefaultValue(0)]
        public decimal Qty04 { get; set; }

        [DefaultValue(0)]
        public decimal Value04 { get; set; }

        [DefaultValue(0)]
        public decimal Qty05 { get; set; }

        [DefaultValue(0)]
        public decimal Value05 { get; set; }

        [DefaultValue(0)]
        public decimal Qty06 { get; set; }

        [DefaultValue(0)]
        public decimal Value06 { get; set; }

        [DefaultValue(0)]
        public decimal Qty07 { get; set; }

        [DefaultValue(0)]
        public decimal Value07 { get; set; }

        [DefaultValue(0)]
        public decimal Qty08 { get; set; }

        [DefaultValue(0)]
        public decimal Value08 { get; set; }

        [DefaultValue(0)]
        public decimal Qty09 { get; set; }

        [DefaultValue(0)]
        public decimal Value09 { get; set; }

        [DefaultValue(0)]
        public decimal Qty10 { get; set; }

        [DefaultValue(0)]
        public decimal Value10 { get; set; }

        [DefaultValue(0)]
        public decimal Qty11 { get; set; }

        [DefaultValue(0)]
        public decimal Value11 { get; set; }

        [DefaultValue(0)]
        public decimal Qty12 { get; set; }

        [DefaultValue(0)]
        public decimal Value12 { get; set; }

        [DefaultValue(0)]
        public decimal Qty13 { get; set; }

        [DefaultValue(0)]
        public decimal Value13 { get; set; }

        [DefaultValue(0)]
        public decimal Qty14 { get; set; }

        [DefaultValue(0)]
        public decimal Value14 { get; set; }

        [DefaultValue(0)]
        public decimal Qty15 { get; set; }

        [DefaultValue(0)]
        public decimal Value15 { get; set; }

        [DefaultValue(0)]
        public decimal Qty16 { get; set; }

        [DefaultValue(0)]
        public decimal Value16 { get; set; }

        [DefaultValue(0)]
        public decimal Qty17 { get; set; }

        [DefaultValue(0)]
        public decimal Value17 { get; set; }

        [DefaultValue(0)]
        public decimal Qty18 { get; set; }

        [DefaultValue(0)]
        public decimal Value18 { get; set; }

        [DefaultValue(0)]
        public decimal Qty19 { get; set; }

        [DefaultValue(0)]
        public decimal Value19 { get; set; }

        [DefaultValue(0)]
        public decimal Qty20 { get; set; }

        [DefaultValue(0)]
        public decimal Value20 { get; set; }

        [DefaultValue(0)]
        public decimal Qty21 { get; set; }

        [DefaultValue(0)]
        public decimal Value21 { get; set; }

        [DefaultValue(0)]
        public decimal Qty22 { get; set; }

        [DefaultValue(0)]
        public decimal Value22 { get; set; }

        [DefaultValue(0)]
        public decimal Qty23 { get; set; }

        [DefaultValue(0)]
        public decimal Value23 { get; set; }

        [DefaultValue(0)]
        public decimal Qty24 { get; set; }

        [DefaultValue(0)]
        public decimal Value24 { get; set; }

        [DefaultValue(0)]
        public decimal Qty25 { get; set; }

        [DefaultValue(0)]
        public decimal Value25 { get; set; }

        [DefaultValue(0)]
        public decimal Qty26 { get; set; }

        [DefaultValue(0)]
        public decimal Value26 { get; set; }

        [DefaultValue(0)]
        public decimal Qty27 { get; set; }

        [DefaultValue(0)]
        public decimal Value27 { get; set; }

        [DefaultValue(0)]
        public decimal Qty28 { get; set; }

        [DefaultValue(0)]
        public decimal Value28 { get; set; }

        [DefaultValue(0)]
        public decimal Qty29 { get; set; }

        [DefaultValue(0)]
        public decimal Value29 { get; set; }

        [DefaultValue(0)]
        public decimal Qty30 { get; set; }

        [DefaultValue(0)]
        public decimal Value30 { get; set; }

        [DefaultValue("")]
        [MaxLength(50)]
        public string Ext1 { get; set; }

        [DefaultValue("")]
        [MaxLength(50)]
        public string Ext2 { get; set; }

        [DefaultValue("")]
        [MaxLength(50)]
        public string Ext3 { get; set; }

        [DefaultValue("")]
        [MaxLength(50)]
        public string Ext4 { get; set; }

        [DefaultValue("")]
        [MaxLength(50)]
        public string Ext5 { get; set; }

    }
}
