using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class CustomerFeedBack:BaseEntity
    {
        public long CustomerFeedBackID { get; set; }

        [DefaultValue("")]
        [MaxLength(20)]
        public string DocumentNo { get; set; }

        public DateTime DocumentDate { get; set; }


        [DefaultValue("")]
        [MaxLength(100)]
        public string CustomerName { get; set; }

        [DefaultValue("")]
        [MaxLength(100)]
        public string Nic { get; set; }

        [DefaultValue("")]
        [MaxLength(100)]
        public string Gender { get; set; }

        [DefaultValue("")]
        [MaxLength(100)]
        public string Age { get; set; }

        [DefaultValue("")]
        [MaxLength(100)]
        public string Occupation { get; set; }

        [DefaultValue("")]
        [MaxLength(100)]
        public string Ethinicity { get; set; }

        [DefaultValue("")]
        [MaxLength(100)]
        public string CardType { get; set; }

        [DefaultValue("")]
        [MaxLength(100)]
        public string Address1 { get; set; }

        [DefaultValue("")]
        [MaxLength(100)]
        public string Address2 { get; set; }

        [DefaultValue("")]
        [MaxLength(100)]
        public string Address3 { get; set; }

        [DefaultValue("")]
        [MaxLength(100)]
        public string ContactPerson { get; set; }

        [DefaultValue("")]
        [MaxLength(100)]
        public string Province { get; set; }

        [DefaultValue("")]
        [MaxLength(100)]
        public string District { get; set; }

        [DefaultValue("")]
        [MaxLength(100)]
        public string Email { get; set; }

        [DefaultValue("")]
        [MaxLength(100)]
        public string Telephone { get; set; }

        [DefaultValue("")]
        [MaxLength(100)]
        public string Mobile { get; set; }

        [DefaultValue("")]
        [MaxLength(100)]
        public string Fax { get; set; }

        [DefaultValue("")]
        [MaxLength(100)]
        public string RefPerson { get; set; }

        [DefaultValue("")]
        [MaxLength(100)]
        public string RefAddress1 { get; set; }

        [DefaultValue("")]
        [MaxLength(100)]
        public string RefAddress2 { get; set; }

        [DefaultValue("")]
        [MaxLength(100)]
        public string RefAddress3 { get; set; }

        [DefaultValue("")]
        [MaxLength(100)]
        public string HabitualShopping { get; set; }

        [DefaultValue("")]
        [MaxLength(100)]
        public string ItemPrice { get; set; }

        [DefaultValue("")]
        [MaxLength(100)]
        public string Quality { get; set; }

        [DefaultValue("")]
        [MaxLength(100)]
        public string Service { get; set; }

        [DefaultValue("")]
        [MaxLength(100)]
        public string SatisfiedWithAvailability { get; set; }

        [DefaultValue("")]
        [MaxLength(100)]
        public string ShoppingAt { get; set; }

        [DefaultValue("")]
        [MaxLength(100)]
        public string SatisfiedWithVariety { get; set; }

        [DefaultValue("")]
        [MaxLength(100)]
        public string StaffsQuality { get; set; }

        [DefaultValue("")]
        [MaxLength(100)]
        public string StaffsKnowledge { get; set; }

        [DefaultValue("")]
        [MaxLength(100)]
        public string InfluencedBy { get; set; }

        [DefaultValue("")]
        [MaxLength(100)]
        public string Comfortable { get; set; }

        [DefaultValue(0)]
        public bool FactQuality { get; set; }

        [DefaultValue(0)]
        public bool FactService { get; set; }

        [DefaultValue(0)]
        public bool FactPrice { get; set; }

        [DefaultValue(0)]
        public bool FactCollections { get; set; }

        [DefaultValue(0)]
        public bool FactMerchandising { get; set; }

        [DefaultValue(0)]
        public bool FactAppearance { get; set; }

        [DefaultValue("")]
        [MaxLength(100)]
        public string KnowAbtPromotion { get; set; }

        [DefaultValue("")]
        [MaxLength(100)]
        public string Regularity { get; set; }

        [DefaultValue("")]
        [MaxLength(100)]
        public string ShoppingFor { get; set; }

        [DefaultValue("")]
        [MaxLength(100)]
        public string Motivates { get; set; }

        [DefaultValue("")]
        [MaxLength(100)]
        public string Recommend4Others { get; set; }

        [DefaultValue("")]
        [MaxLength(100)]
        public string YourChanges { get; set; }

        [DefaultValue("")]
        [MaxLength(100)]
        public string Recommendations { get; set; }

        [DefaultValue("")]
        [MaxLength(100)]
        public string RateService { get; set; }
        
        [DefaultValue(0)]
        public long LocationID { get; set; }

        [DefaultValue(0)]
        public bool IsDelete { get; set; }

    }
}
