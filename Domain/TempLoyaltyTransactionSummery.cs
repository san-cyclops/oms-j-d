using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class TempLoyaltyTransactionSummery
    {
        public long TempLoyaltyTransactionSummeryID { get; set; } 

        [DefaultValue("")]
        public string CustomerCode { get; set; }

        [DefaultValue("")]
        public string CustomerName { get; set; }

        [DefaultValue(0)]
        public long CustomerID { get; set; }

        [DefaultValue(0)]
        public decimal EPoints { get; set; }

        [DefaultValue(0)]
        public decimal RPoints { get; set; }

        [DefaultValue(0)]
        public decimal TotalPurchaseAmount { get; set; }

        [DefaultValue(0)]
        public int TotalVisits { get; set; }

        [DefaultValue("")]
        public string CardNo { get; set; }

        [DefaultValue("")]
        public string CardName { get; set; }

        [DefaultValue("")]
        public string NicNo { get; set; }
    }
}
