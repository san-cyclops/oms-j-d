using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class LgsStockAdjustmentHeader : BaseEntity
    {
        public long LgsStockAdjustmentHeaderID { get; set; }

        [DefaultValue(0)]
        public long StockAdjustmentHeaderID { get; set; } 

        [DefaultValue(0)]
        public int CompanyID { get; set; }

        [DefaultValue(0)]
        public int LocationID { get; set; }

        [DefaultValue(0)]
        public int CostCentreID { get; set; }

        [DefaultValue(0)]
        public int DocumentID { get; set; }

        [DefaultValue("")]
        [MaxLength(20)]
        public string DocumentNo { get; set; }

        public DateTime DocumentDate { get; set; }

        [DefaultValue(0)]
        public decimal TotalCostValue { get; set; }

        [DefaultValue(0)]
        public decimal TotalSellingtValue { get; set; }

        [DefaultValue(0)]
        public decimal TotalQty { get; set; }

        [DefaultValue(0)]
        public int ReferenceDocumentID { get; set; }

        [DefaultValue("")]
        [MaxLength(20)]
        public string ReferenceDocumentNo { get; set; }

        [DefaultValue(0)]
        public int StockAdjustmentMode { get; set; }

        [DefaultValue(0)]
        public int StockAdjustmentLayer { get; set; }

        [DefaultValue("")]
        [MaxLength(500)]
        public string Narration { get; set; }

        [DefaultValue("")]
        [MaxLength(150)]
        public string Remark { get; set; }

        [DefaultValue(0)]
        public int DocumentStatus { get; set; }
    }
}
