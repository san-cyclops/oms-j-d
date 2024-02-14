using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class SystemConfig
    {
        public long SystemConfigID { get; set; }

        [DefaultValue(0)]
        public int ProductID { get; set; }

        [DefaultValue("")]
        [MaxLength(50)]       
        public string ProductName { get; set; }

        [DefaultValue("")]
        [MaxLength(10)]
        public string Version { get; set; }

        [DefaultValue("")]
        [MaxLength(50)]
        public string CompanyName { get; set; }

        [DefaultValue("")]
        [MaxLength(50)]
        public string CompanyAddress1 { get; set; }

        [DefaultValue("")]
        [MaxLength(50)]
        public string CompanyAddress2 { get; set; }

        [DefaultValue("")]
        [MaxLength(50)]
        public string CompanyAddress3 { get; set; }

        [DefaultValue("")]
        [MaxLength(35)]
        public string CompanyTelephone { get; set; }

        [DefaultValue("")]
        [MaxLength(35)]
        public string CompanyFax { get; set; }

        [DefaultValue("")]
        [MaxLength(20)]
        public string CompanyEmail { get; set; }

        [DefaultValue("")]
        [MaxLength(20)]
        public string CompanyWeb { get; set; }

        [DefaultValue("")]
        [MaxLength(50)]
        public string LicensedTo { get; set; }

        [DefaultValue(0)]
        public string BarcodeTextPath { get; set; } 
    }
}
