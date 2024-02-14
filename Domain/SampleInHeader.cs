using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class SampleInHeader : BaseEntity
    {
        public long SampleInHeaderID { get; set; }

        [DefaultValue(0)]
        public long SampleHeaderID { get; set; } 

        public long SampleOutHeaderID { get; set; } 

        [DefaultValue(0)]
        public int CompanyID { get; set; }

        [DefaultValue(0)]
        public int LocationID { get; set; }

        [DefaultValue(0)]
        public int CostCentreID { get; set; }

        [DefaultValue(0)]
        public int DocumentID { get; set; }

        [MaxLength(20)]
        public string DocumentNo { get; set; } 

        [MaxLength(100)]
        public string IssuedTo { get; set; }

        [DefaultValue(0)]
        public int SampleOutType { get; set; } 

        [MaxLength(100)]
        public string DeliveryPerson { get; set; }

        public DateTime DocumentDate { get; set; }

        [DefaultValue(0)]
        public int PaymentMethodID { get; set; }

        [DefaultValue(0)]
        public decimal NetAmount { get; set; }

        [DefaultValue(0)]
        public decimal TotalQty { get; set; }

        [DefaultValue("")]
        [MaxLength(150)]
        public string Remark { get; set; }

        [DefaultValue(0)]
        public int DocumentStatus { get; set; }

        [DefaultValue(0)]
        public bool IsDelete { get; set; }

        [DefaultValue(0)]
        public int Balanced { get; set; }
    }
}
