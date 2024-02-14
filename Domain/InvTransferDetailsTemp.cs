using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class InvTransferDetailsTemp
    {
        public long InvTransferDetailsTempID { get; set; }

        [DefaultValue(0)]
        public long InvTransferDetailsHeaderID { get; set; } 

        [DefaultValue(0)]
        public int CompanyID { get; set; }

        [DefaultValue(0)]
        public int FromLocationID { get; set; }

        [DefaultValue(0)]
        public int ToLocationID { get; set; }

        [DefaultValue(0)]
        public int CostCentreID { get; set; }

        [DefaultValue(0)]
        public int DocumentID { get; set; }

        [DefaultValue("")]
        [MaxLength(20)]
        public string DocumentNo { get; set; }

        [DefaultValue(0)]
        public long LineNo { get; set; }

        [DefaultValue(0)]
        public long ProductID { get; set; }

        [DefaultValue("")]
        [MaxLength(25)]
        public string ProductCode { get; set; }

        [DefaultValue("")]
        [MaxLength(50)]
        public string ProductName { get; set; }

        public long BarCode { get; set; }
        
        [DefaultValue(0)]
        public long BaseUnitID { get; set; }

        [DefaultValue("")]
        [MaxLength(15)]
        public string UnitOfMeasure { get; set; }

        [DefaultValue(0)]
        public long UnitOfMeasureID { get; set; }

        [DefaultValue(1)]
        public decimal ConvertFactor { get; set; }

        [DefaultValue("")]
        [MaxLength(25)]
        public string BatchNo { get; set; }

        public DateTime? ExpiryDate { get; set; }        

        [DefaultValue(0)]
        public decimal Qty { get; set; }
       
        [DefaultValue(0)]
        public decimal CurrentQty { get; set; }

        [DefaultValue(0)]
        public decimal CostPrice { get; set; }

        [DefaultValue(0)]
        public decimal SellingPrice { get; set; }

        [DefaultValue(0)]
        public decimal AverageCost { get; set; }

        [DefaultValue(0)]
        public decimal NetAmount { get; set; }

        [DefaultValue(0)]
        public int DocumentStatus { get; set; }

        [DefaultValue(0)]
        public bool IsBatch { get; set; }

       
    }
}
