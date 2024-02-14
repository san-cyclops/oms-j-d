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
    public class InvProductBatchNoTemp
    {
        public long InvProductBatchNoExpiaryDetailID { get; set; }

        [DefaultValue(0)]
        public int CompanyID { get; set; }

        [DefaultValue(0)]
        public int LocationID { get; set; }

        [DefaultValue(0)]
        public int CostCentreID { get; set; }

        [DefaultValue(0)]
        public long ProductID { get; set; }

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
        public bool IsDelete { get; set; }

        [NotMapped]
        public bool Selection { get; set; }

        [NotMapped]
        [MaxLength(40)]
        public string LocationCode { get; set; }

        [NotMapped]
        [MaxLength(40)]
        public string LocationName { get; set; }

    }
}
