using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Supplier : BaseEntity
    {
        public long SupplierID { get; set; }

        [Required]
        [DefaultValue("")]
        [MaxLength(15)]
	    public string SupplierCode { get; set; }

        [Required]
        [DefaultValue(0)]
	    public int SupplierTitle { get; set; }

        [Required]
        [DefaultValue("")]
        [MaxLength(100)]
	    public string SupplierName { get; set; }

        [Required]
        [DefaultValue(0)]
	    public int SupplierType { get; set; }

        [DefaultValue("")]
        [MaxLength(100)]
	    public string ContactPersonName { get; set; }

        [DefaultValue(0)]
        public int Gender { get; set; }

        [DefaultValue("")]
        [MaxLength(50)]
	    public string BillingAddress1 { get; set; }

        [DefaultValue("")]
        [MaxLength(50)]
	    public string BillingAddress2 { get; set; }

        [DefaultValue("")]
        [MaxLength(50)]
	    public string BillingAddress3 { get; set; }

        [Required]
        [DefaultValue("")]
        [MaxLength(50)]
	    public string BillingTelephone { get; set; }

        [DefaultValue("")]
        [MaxLength(50)]
	    public string BillingMobile { get; set; }

        [DefaultValue("")]
        [MaxLength(50)]
	    public string BillingFax { get; set; }

        [DefaultValue("")]
        [MaxLength(100)]
        //[EmailAddress]
	    public string Email { get; set; }

        [MaxLength(100)]
        [DefaultValue("")]
        public string RepresentativeName { get; set; }

        [MaxLength(50)]
        [DefaultValue("")]
        public string RepresentativeNICNo { get; set; }

        [DefaultValue("")]
        [MaxLength(50)]
	    public string DeliveryAddress1 { get; set; }

        [DefaultValue("")]
        [MaxLength(50)]
	    public string DeliveryAddress2 { get; set; }

        [DefaultValue("")]
        [MaxLength(50)]
	    public string DeliveryAddress3 { get; set; }

        [NotMapped]
        public string DeliveryAddress { get { return DeliveryAddress1 + "," + DeliveryAddress2 + "," + DeliveryAddress3; } }

        [DefaultValue("")]
        [MaxLength(50)]
	    public string DeliveryTelephone { get; set; }

        [DefaultValue("")]
        [MaxLength(50)]
	    public string DeliveryMobile { get; set; }

        [DefaultValue("")]
        [MaxLength(50)]
	    public string DeliveryFax { get; set; }

	    public byte[] SupplierImage { get; set; }

        [DefaultValue("")]
        [MaxLength(20)]
	    public string ReferenceNo { get; set; }

        [DefaultValue("")]
        [MaxLength(20)]
	    public string ReferenceSerial { get; set; }

        [DefaultValue(" ")]
        [MaxLength(20)]
	    public string PostalCode { get; set; }

        [NotMapped]
        public string BillingAddress { get { return BillingAddress1 + "," + BillingAddress2 + "," + BillingAddress3 + "." + PostalCode; } }

        [DefaultValue(0)]
        public int TaxID1 { get; set; }

        [DefaultValue("")]
        [MaxLength(25)]
	    public string TaxNo1 { get; set; }

        [DefaultValue(0)]
	    public int TaxID2 { get; set; }

        [DefaultValue("")]
        [MaxLength(25)]
	    public string TaxNo2 { get; set; }

        [DefaultValue(0)]
	    public int TaxID3 { get; set; }

        [DefaultValue("")]
        [MaxLength(25)]
	    public string TaxNo3 { get; set; }

        [DefaultValue(0)]
	    public int TaxID4 { get; set; }

        [DefaultValue("")]
        [MaxLength(25)]
	    public string TaxNo4 { get; set; }

        [DefaultValue(0)]
	    public int TaxID5 { get; set; }

        [DefaultValue("")]
        [MaxLength(25)]
        public string TaxNo5 { get; set; }

        [DefaultValue("")]
        [MaxLength(50)]
	    public string TaxRegistrationNo { get; set; }

        [DefaultValue("")]
        [MaxLength(100)]
	    public string TaxRegistrationName { get; set; }

        [DefaultValue(0)]
        public int PaymentMethod { get; set; }

        [DefaultValue(0)]
        public decimal CreditLimit { get; set; }

        [DefaultValue(0)]
        public int CreditPeriod { get; set; }

        [DefaultValue(0)]
        public decimal ChequeLimit { get; set; }

        [DefaultValue(0)]
        public int ChequePeriod { get; set; }  

        [DefaultValue("")]
        [MaxLength(100)]
	    public string Remark { get; set; }

        [DefaultValue("")]
        [MaxLength(200)]
	    public string ProductBusinessType { get; set; }

        [DefaultValue("")]
        [MaxLength(200)]
	    public string SuppliedProducts { get; set; }

        [DefaultValue(0)]
        public int OrderCircle { get; set; }

        [DefaultValue(0)]
        public int SupplierGroupID { get; set; }

      //  [DefaultValue(0)]
        public long LedgerID { get; set; }

     //   [DefaultValue(0)]
        public long OtherLedgerID { get; set; }

        [DefaultValue("")]
        [MaxLength(50)]
	    public string TaxIdNo { get; set; }

        [DefaultValue(0)]
        public bool IsUpload { get; set; }

        [DefaultValue(0)]
        public bool IsBlocked { get; set; } // not allow to do transactions, view only

        [DefaultValue(0)]
	    public bool IsSuspended { get; set; } // not allow to do transactions, view only

        [DefaultValue(0)]
	    public bool IsDelete { get; set; }

        //public virtual AccLedgerAccount AccLedgerAccount { get; set; }
        //public virtual AccLedgerAccount AccOtherLedgerAccount { get; set; }

        public virtual SupplierGroup SupplierGroup { get; set; }
        public virtual SupplierProperty SupplierProperty { get; set; }
        public virtual ICollection<InvProductMaster> InvProductMasters { get; set; }

    }
}
