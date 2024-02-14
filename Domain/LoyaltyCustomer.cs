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
    public class LoyaltyCustomer : BaseEntity
    {
        public long LoyaltyCustomerID { get; set; }

        [Required()]
        public string CardNo { get; set; }

        [Required()]
        [MaxLength(15)]
        public string CustomerCode { get; set; }

        [Required()]
        [DefaultValue(0)]
        public int CustomerTitle { get; set; }

        [Required()]
        [MaxLength(100)]
        public string CustomerName { get; set; }

        [Required()]
        [DefaultValue(0)]
        public int Gender { get; set; }

        [MaxLength(50)]
        [DefaultValue("")]
        public string NicNo { get; set; }

        [MaxLength(50)]
        [DefaultValue("")]
        public string ReferenceNo { get; set; }

        [Required()]
        [DefaultValue(0)]
        public int Nationality { get; set; }

        [DefaultValue(0)]
        public long CustomerId { get; set; }

        public DateTime DateOfBirth { get; set; }

        [DefaultValue(0)]
        public int Age { get; set; }

        [DefaultValue(0)]
        public int Religion { get; set; }

        [NotMapped]
        public int Race { get; set; }

        [MaxLength(50)]
        [DefaultValue("")]
        public string Address1 { get; set; }

        [MaxLength(50)]
        [DefaultValue("")]
        public string Address2 { get; set; }


        [MaxLength(50)]
        [DefaultValue("")]
        public string Address3 { get; set; }

        [DefaultValue(0)]
        public int District { get; set; }

        [MaxLength(50)]
        [DefaultValue("")]
        public string LandMark { get; set; }

        [MaxLength(50)]
        [DefaultValue("")]
        public string Email { get; set; }

        [MaxLength(50)]
        [DefaultValue("")]
        public string Telephone { get; set; }

        [MaxLength(50)]
        [DefaultValue("")]
        public string Mobile { get; set; }

        [MaxLength(50)]
        [DefaultValue("")]
        public string Fax { get; set; }

        [MaxLength(50)]
        [DefaultValue("")]
        public string Organization { get; set; }

        [MaxLength(50)]
        [DefaultValue("")]
        public string Occupation { get; set; }

        [MaxLength(50)]
        [DefaultValue("")]
        public string WorkAddres1 { get; set; }

        [MaxLength(50)]
        [DefaultValue("")]
        public string WorkAddres2 { get; set; }

        [MaxLength(50)]
        [DefaultValue("")]
        public string WorkAddres3 { get; set; }

        [MaxLength(50)]
        [DefaultValue("")]
        public string WorkEmail { get; set; }

        [MaxLength(50)]
        [DefaultValue("")]
        public string WorkTelephone { get; set; }

        [MaxLength(50)]
        [DefaultValue("")]
        public string WorkMobile { get; set; }

        [MaxLength(50)]
        [DefaultValue("")]
        public string WorkFax { get; set; }

        [MaxLength(50)]
        [DefaultValue("")]
        public string NameOnCard { get; set; }

        [DefaultValue(0)]
        public long CardMasterID { get; set; }

        public bool CardIssued { get; set; }

        public DateTime IssuedOn { get; set; }

        public DateTime ExpiryDate { get; set; }

        public DateTime RenewedOn { get; set; }

        [MaxLength(50)]
        [DefaultValue("")]
        public string SpouseName { get; set; }

        [DefaultValue(0)]
        public int CivilStatus { get; set; }

        [DefaultValue(0)]
        public int FemaleAdults { get; set; }

        [DefaultValue(0)]
        public int MaleAdults { get; set; }

        [DefaultValue(0)]
        public int Childrens { get; set; }

        public DateTime SpouseDateOfBirth { get; set; }

        public DateTime Anniversary { get; set; }

        [DefaultValue(0)]
        public bool SinhalaHinduNewYear { get; set; }

        [DefaultValue(0)]
        public bool ThaiPongal { get; set; }

        [DefaultValue(0)]
        public bool Wesak { get; set; }

        [DefaultValue(0)]
        public bool HajFestival { get; set; }

        [DefaultValue(0)]
        public bool Ramazan { get; set; }

        [DefaultValue(0)]
        public bool Xmas { get; set; }

        [DefaultValue(0)]
        public int FavoriteTvChannel { get; set; }

        [DefaultValue(0)]
        public int FavoriteNewsPapers { get; set; }

        [DefaultValue(0)]
        public int FavoriteRadioChannels { get; set; }

        [DefaultValue(0)]
        public int FavoriteMagazines { get; set; }

        [DefaultValue(0)]
        public long LedgerId { get; set; }

        [DefaultValue(0)]
        public long LedgerId2 { get; set; }

        [DefaultValue(0)]
        public decimal CreditLimit { get; set; }

        [DefaultValue(0)]
        public decimal CreditPeriod { get; set; }

        [DefaultValue(0)]
        public int DeliverTo { get; set; }

        [MaxLength(50)]
        [DefaultValue(0)]
        public String DeliverToAddress { get; set; }

        [DefaultValue(0)]
        public DateTime CustomerSince { get; set; }

        [DefaultValue(0)]
        public bool SendUpdatesViaEmail { get; set; }

        [DefaultValue(0)]
        public bool SendUpdatesViaSms { get; set; }

        [DefaultValue(0)]
        public bool IsSuspended { get; set; }

        [DefaultValue(0)]
        public bool IsBlackListed { get; set; }

        [DefaultValue(0)]
        public bool IsCreditAllowed { get; set; }

        [DefaultValue(0)]
        public bool IsDelete { get; set; }

        [DefaultValue(0)]
        public bool Active { get; set; }

        public byte[] CustomerImage { get; set; }

        [DefaultValue(0)]
        public decimal CPoints { get; set; }

        [DefaultValue(0)]
        public decimal EPoints { get; set; }

        [DefaultValue(0)]
        public decimal RPoints { get; set; }

        [DefaultValue(1)]
        public bool IsReDimm { get; set; }
 
        public DateTime AcitiveDate { get; set; }

        [Required]
        [DefaultValue(0)]
        public int LocationID { get; set; }

        [DefaultValue(0)]
        public bool IsAbeyance { get; set; }

        [DefaultValue(0)]
        public bool IsRegByPOS { get; set; }

        [DefaultValue(0)]
        public long CashierID { get; set; }

        [DefaultValue(0)]
        public int LoyaltyType { get; set; }

        [MaxLength(200)]
        [DefaultValue("")]
        public string Remark { get; set; }

        [MaxLength(15)]
        [DefaultValue("")]
        public string SystemGeneratedCode { get; set; }

        [DefaultValue(0)]
        public decimal ExpiryPoints { get; set; } 

        public virtual LoyaltySuplimentary LoyaltySuplimentary { get; set; }

    }
}
