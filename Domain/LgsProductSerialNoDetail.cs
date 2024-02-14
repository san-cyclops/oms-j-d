using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace Domain
{
    public class LgsProductSerialNoDetail : BaseEntity
    {
        public long LgsProductSerialNoDetailID { get; set; }

        [Required]
        public long ProductSerialNoDetailID { get; set; }

        [DefaultValue(0)]
        public int GroupOfCompanyID { get; set; }

        [DefaultValue(0)]
        public int CompanyID { get; set; }

        [DefaultValue(0)]
        public int LocationID { get; set; }

        [DefaultValue(0)]
        public int CostCentreID { get; set; }

        [DefaultValue(0)]
        public long LineNo { get; set; }

        [DefaultValue(0)]
        public long ProductID { get; set; }

        [DefaultValue(0)]
        public long UnitOfMeasureID { get; set; }

        public DateTime? ExpiryDate { get; set; }

        [DefaultValue(0)]
        public int ReferenceDocumentDocumentID { get; set; }

        [DefaultValue(0)]
        public long ReferenceDocumentID { get; set; }

        [Required]
        public string SerialNo { get; set; }

        [DefaultValue(0)]
        public int DocumentStatus { get; set; }

        [MaxLength(40)]
        public string BatchNo { get; set; }
    }
}
