using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
namespace Domain
{
    public class InvScannerData : BaseEntity
    {

        public long InvScannerDataID { get; set; }

        [DefaultValue(0)]
        public int CompanyID { get; set; }

        [DefaultValue(0)]
        public int LocationID { get; set; }

        [DefaultValue(0)]
        public int CostCentreID { get; set; }

        [DefaultValue(0)]
        public int DocumentID { get; set; }

        [DefaultValue("")]
        [MaxLength(25)]
        public string DocumentNo { get; set; }

        public DateTime DocumentDate { get; set; }

        [DefaultValue(0)]
        public int ScannerID { get; set; }

        [NotMapped]
        [DefaultValue(0)]        
        public long ProductID { get; set; }

        [NotMapped]
        [DefaultValue("")]
        [MaxLength(25)]
        public string ProductCode { get; set; }

        [NotMapped]
        [DefaultValue("")]
        [MaxLength(50)]
        public string ProductName { get; set; }

        public long BarCode { get; set; }
        

        [DefaultValue(0)]
        public int DocumentStatus { get; set; }

        [NotMapped]
        [DefaultValue("")]
        [MaxLength(25)]
        public string OriginalDocumentNo { get; set; }

        [DefaultValue(0)]
        public int OriginalDocumentID { get; set; }
    }
}
