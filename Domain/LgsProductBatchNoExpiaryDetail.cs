using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class LgsProductBatchNoExpiaryDetail : BaseEntity
    {
        public long LgsProductBatchNoExpiaryDetailID { get; set; }

        public long ProductBatchNoExpiaryDetailID { get; set; } 

        [DefaultValue(0)]
        public int CompanyID { get; set; }

        [DefaultValue(0)]
        public int LocationID { get; set; }

        [DefaultValue(0)]
        public int CostCentreID { get; set; }

        [DefaultValue(0)]
        public int ReferenceDocumentDocumentID { get; set; }

        [DefaultValue(0)]
        public long ReferenceDocumentID { get; set; } 

        [DefaultValue(0)]
        public long ProductID { get; set; }

        [Required]
        public long BarCode { get; set; }


        [Required]
        [MaxLength(40)]
        public string BatchNo { get; set; }


        [DefaultValue(0)]
        public DateTime? ExpiryDate { get; set; }


        [DefaultValue(0)]
        public long LineNo { get; set; }

        [DefaultValue(0)]
        public decimal Qty { get; set; }

        [DefaultValue(0)]
        public long UnitOfMeasureID { get; set; }

        [DefaultValue(0)]
        public decimal BalanceQty { get; set; }

        [DefaultValue(0)]
        public decimal CostPrice { get; set; }

        [DefaultValue(0)]
        public decimal SellingPrice { get; set; }

        [DefaultValue(0)]
        public long SupplierID { get; set; }

        [DefaultValue(0)]
        public bool IsDelete { get; set; }   
    }
}
