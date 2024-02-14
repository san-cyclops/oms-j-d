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
    public class InvLoyaltyTransaction
    {

        public long InvLoyaltyTransactionID { get; set; }

        [Required()]
        public long CustomerID { get; set; }

        [Required()]
        public int CustomerType { get; set; }

        [Required()]
        [DefaultValue("")]
        [MaxLength(15)]
        public string Receipt { get; set; }                  
            
        [DefaultValue(0)]
        public decimal Amount { get; set; }

        [DefaultValue(0)]
        public decimal Points { get; set; }

        [Required()]
        public int TransID { get; set; }

        [Required()]
        public int LocationID { get; set; }

        [Required()]
        public DateTime DocumentDate { get; set; }

        [Required()]
        public int UnitNo { get; set; }
        
        [Required()]
        public long CashierID { get; set; }
        
        [Required()]
        public DateTime DocumentTime { get; set; }
        
        [DefaultValue(0)]
        public decimal DiscPer { get; set; }
        
        [DefaultValue(0)]
        public decimal DiscAmt { get; set; }
        
        [DefaultValue(0)]
        public decimal PointsRate { get; set; }
        
        [Required()]
        public long Zno { get; set; }
        
        [DefaultValue("")]
        [MaxLength(15)]
        public string CardNo { get; set; }
        
        [Required()]
        public int CardType { get; set; }
           
        [Required()]
        public int LoyaltyType { get; set; }

        [Required()]
        [DefaultValue(0)]
        public bool IsGuidClaimed { get; set; }

        [DefaultValue(0)]
        public int IsSync { get; set; }

        [DefaultValue("")]
        [MaxLength(20)]
        public string CustomerCode { get; set; }

        [DefaultValue("")]
        [MaxLength(50)]
        public string NIC { get; set; }

        [DefaultValue("")]
        [MaxLength(50)]
        public string RefNo { get; set; }
    }
}
