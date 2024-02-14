using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class LgsSupplierProperty : BaseEntity
    {
        //public long SupplierPropertyID { get; set; }

        //[DefaultValue(0)]
        [Key]
        public long LgsSupplierID { get; set; } 

        [DefaultValue("")]
        [MaxLength(15)]
        public string SupplierCode { get; set; }

        [DefaultValue("")]
        [MaxLength(100)]
        public string PaymentCollector1Name { get; set; }

        [DefaultValue("")]
        [MaxLength(100)]
        public string PaymentCollector1Designation { get; set; }

        [DefaultValue("")]
        [MaxLength(50)]
        public string PaymentCollector1ReferenceNo { get; set; }

        [DefaultValue("")]
        [MaxLength(50)]
        public string PaymentCollector1TelephoneNo { get; set; }

        public byte[] PaymentCollector1Image { get; set; }

        [DefaultValue("")]
        [MaxLength(100)]
        public string PaymentCollector2Name { get; set; }

        [DefaultValue("")]
        [MaxLength(50)]
        public string PaymentCollector2Designation { get; set; }

        [DefaultValue("")]
        [MaxLength(50)]
        public string PaymentCollector2ReferenceNo { get; set; }

        [DefaultValue("")]
        [MaxLength(50)]
        public string PaymentCollector2TelephoneNo { get; set; }

        [DefaultValue(0)]
        public byte[] PaymentCollector2Image { get; set; }

        [DefaultValue("")]
        [MaxLength(100)]
        public string IntroducedPerson1Name { get; set; }

        [DefaultValue("")]
        [MaxLength(50)]
        public string IntroducedPerson1Address1 { get; set; }

        [DefaultValue("")]
        [MaxLength(50)]
        public string IntroducedPerson1Address2 { get; set; }

        [DefaultValue("")]
        [MaxLength(50)]
        public string IntroducedPerson1Address3 { get; set; }

        [DefaultValue("")]
        [MaxLength(50)]
        public string IntroducedPerson1TelephoneNo { get; set; }

        [DefaultValue("")]
        [MaxLength(100)]
        public string IntroducedPerson2Name { get; set; }

        [DefaultValue("")]
        [MaxLength(50)]
        public string IntroducedPerson2Address1 { get; set; }

        [DefaultValue("")]
        [MaxLength(50)]
        public string IntroducedPerson2Address2 { get; set; }

        [DefaultValue("")]
        [MaxLength(50)]
        public string IntroducedPerson2Address3 { get; set; }

        [DefaultValue("")]
        [MaxLength(50)]
        public string IntroducedPerson2TelephoneNo { get; set; }

        [DefaultValue(0)]
        public long DealingPeriod { get; set; }

        [DefaultValue(0)]
        public int YearOfBusinessCommencement { get; set; }

        [DefaultValue(0)]
        public int NoOfEmployees { get; set; }

        [DefaultValue(0)]
        public int NoOfMachines { get; set; }

        [DefaultValue(0)]
        public int PercentageOfWHT { get; set; }

        [DefaultValue("")]
        [MaxLength(50)]
        public string IncomeTaxFileNo { get; set; }

        // [DefaultValue(0)]
        // public long SupplierCustomerCategoryID { get; set; }

        [DefaultValue("")]
        [MaxLength(200)]
        public string RemarkByPurchasingDepartment { get; set; }

        [DefaultValue("")]
        [MaxLength(200)]
        public string RemarkByAccountDepartment { get; set; }

        [DefaultValue(0)]
        public bool IsVatRegistrationCopy { get; set; }
        public byte[] VatRegistrationCopy { get; set; }

        [DefaultValue(0)]
        public bool IsPassportPhotos { get; set; }
        public byte[] PassportPhotos { get; set; }

        [DefaultValue(0)]
        public bool IsCDDCertification { get; set; }
        public byte[] CDDCertificate { get; set; }

        [DefaultValue(0)]
        public bool IsBrandCertification { get; set; }
        public byte[] BrandCertificate { get; set; }

        [DefaultValue(0)]
        public bool IsRefeneceDocumentCopy { get; set; }
        public byte[] RefeneceDocumentCopy { get; set; }

        [DefaultValue(0)]
        public bool IsLicenceToImport { get; set; }
        public byte[] LicenceToImport { get; set; }

        [DefaultValue(0)]
        public bool IsLabCertification { get; set; }
        public byte[] LabCertificate { get; set; }

        [DefaultValue(0)]
        public bool IsBRCCopy { get; set; }
        public byte[] BRCCopy { get; set; }

        [DefaultValue(0)]
        public bool IsDistributorOwnerCopy { get; set; }
        public byte[] DistributorOwnerCopy { get; set; }

        [DefaultValue(0)]
        public bool IsQCCertification { get; set; }
        public byte[] QCCertificate { get; set; }

        [DefaultValue(0)]
        public bool IsSLSISOCertiification { get; set; }
        public byte[] SLSISOCertiificate { get; set; } 

        [DefaultValue(0)]
        public bool IsDelete { get; set; }

        //public virtual Supplier Supplier { get; set; }
        //public int CustomerID { get; set; }
        // public virtual Supplier Supplier { get; set; }
    }
}
