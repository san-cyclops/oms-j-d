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
    public class Customer : BaseEntity
    {
        public long CustomerID { get; set; }
        
        [Required]
        [MaxLength(15)]
	    public string CustomerCode { get; set; }

        [Required]
        [DefaultValue(0)]
        public int CustomerTitle { get; set; } 

        [Required]
        [MaxLength(100)]
	    public string CustomerName { get; set; }

        [Required]
        [MaxLength(25)]
	    public string CustomerType { get; set; }

        [MaxLength(100)]
        [DefaultValue("")]
	    public string ContactPersonName { get; set; }

        [Required]
        [DefaultValue(0)]
	    public int Gender { get; set; }

        [MaxLength(50)]
        [DefaultValue("")]
	    public string BillingAddress1 { get; set; }

        [MaxLength(50)]
        [DefaultValue("")]
	    public string BillingAddress2 { get; set; }

        [MaxLength(50)]
        [DefaultValue("")]
	    public string BillingAddress3 { get; set; }

        [DefaultValue("")]
	    public string BillingTelephone { get; set; }

        [DefaultValue("")]
	    public string BillingMobile { get; set; }

        [DefaultValue("")]
	    public string BillingFax { get; set; }

        [DefaultValue("")]
	    public string Email { get; set; }

        [MaxLength(100)]
        [DefaultValue("")]
	    public string RepresentativeName { get; set; }

        [MaxLength(50)]
        [DefaultValue("")]
	    public string RepresentativeNICNo { get; set; }

        [MaxLength(50)]
        [DefaultValue("")]
        public string NICNo { get; set; }

        [DefaultValue("")]
	    public string RepresentativeMobileNo { get; set; }

        [MaxLength(50)]
        [DefaultValue("")]
	    public string DeliveryAddress1 { get; set; }

        [MaxLength(50)]
        [DefaultValue("")]
	    public string DeliveryAddress2 { get; set; }

        [MaxLength(50)]
        [DefaultValue("")]
	    public string DeliveryAddress3 { get; set; }

        [NotMapped]
        public string DeliveryAddress { get { return DeliveryAddress1 + "," + DeliveryAddress1 + "," + DeliveryAddress1; } }

        [DefaultValue("")]
	    public string DeliveryTelephone { get; set; }

        [DefaultValue("")]
	    public string DeliveryMobile { get; set; }

        [DefaultValue("")]
	    public string DeliveryFax { get; set; }

        [DefaultValue(0)]
	    public decimal CreditLimit { get; set; }

        [DefaultValue(0)]
	    public decimal ChequeLimit { get; set; }

        [DefaultValue(0)]
        public decimal TemporaryLimit { get; set; }

        [DefaultValue(0)]
        public decimal Outstanding { get; set; }

        [DefaultValue("")]
	    public string ReferenceNo { get; set; }

        [DefaultValue(0)]
	    public decimal BankDraft { get; set; }

       

        public byte[] CustomerImage { get; set; }

        //-- comment by sanjeewa - loyality ref no - 
        //[MaxLength(20)]
        //[DefaultValue("")]
	    //public string ReferenceCode { get; set; }

        [DefaultValue(0)]
        public decimal MaximumCashDiscount { get; set; }

        [DefaultValue(0)]
        public decimal MaximumCreditDiscount { get; set; }

        [DefaultValue(0)]
        public int CreditPeriod { get; set; }

        [DefaultValue(0)]
        public int ChequePeriod { get; set; } 

        [DefaultValue(0)]
        public int TaxID1 { get; set; }

        [MaxLength(25)]
        [DefaultValue("")]
	    public string TaxNo1 { get; set; }

        [DefaultValue(0)]
	    public int TaxID2 { get; set; }

        [MaxLength(25)]
        [DefaultValue("")]
	    public string TaxNo2 { get; set; }

        [DefaultValue(0)]
	    public int TaxID3 { get; set; }

        [MaxLength(25)]
        [DefaultValue("")]
	    public string TaxNo3 { get; set; }

        [DefaultValue(0)]
	    public int TaxID4 { get; set; }

        [MaxLength(25)]
        [DefaultValue("")]
	    public string TaxNo4 { get; set; }

        [DefaultValue(0)]
	    public int TaxID5{ get; set; }

        [MaxLength(25)]
        [DefaultValue("")]
        public string TaxNo5 { get; set; }

        [MaxLength(150)]
        [DefaultValue("")]
        public string Remark { get; set; }

        [DefaultValue(0)]
	    public bool IsFixedDiscount { get; set; }

        [DefaultValue(0)]
	    public bool IsSuspended { get; set; }

        [DefaultValue(0)]
	    public bool IsBlackListed { get; set; }

        [DefaultValue(0)]
	    public bool IsLoyalty { get; set; }

        [DefaultValue(0)]
        public bool IsCreditAllowed { get; set; }

        [DefaultValue(0)]
	    public bool IsDelete { get; set; }

        [NotMapped]
        public string BillingAddress { get { return BillingAddress1 + "," + BillingAddress2 + "," + BillingAddress3; } }

        [DefaultValue(0)]
        public long BrokerID { get; set; }

        [DefaultValue(0)]
        public int CustomerGroupID { get; set; }

        [DefaultValue(0)]
        public long AreaID { get; set; }

        [DefaultValue(0)]
        public long TerritoryID { get; set; }

        [DefaultValue(0)]
        public int PaymentMethodID { get; set; }

        [DefaultValue(0)]
        public long LedgerID { get; set; }

        [DefaultValue(0)]
        public long OtherLedgerID { get; set; }

        public virtual CustomerGroup CustomerGroup { get; set; }

        //public virtual AccLedgerAccount AccLedgerAccount { get; set; }

        //public virtual AccLedgerAccount AccOtherLedgerAccount { get; set; }

        public virtual PaymentMethod PaymentMethod { get; set; }

        public virtual Area Area { get; set; }

        public virtual Territory Territory { get; set; }

        //public virtual Broker Broker { get; set; }

        //public virtual ICollection<LoyaltyCustomer> LoyaltyCustomer{ get; set; }

    }
}
