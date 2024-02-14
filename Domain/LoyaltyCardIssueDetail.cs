using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class LoyaltyCardIssueDetail:BaseEntity
    {
        public long LoyaltyCardIssueDetailID { get; set; }

        [DefaultValue(0)]
        public long CardIssueDetailID { get; set; }
        
        //[DefaultValue("")]
        //[MaxLength(4)]
        //public string CardPrefix { get; set; }
         
        //[Required]
        //[DefaultValue(0)]
        //public int CardLength { get; set; }

        public DateTime IssueDate { get; set; }

        //[Required]
        //[MaxLength(50)]
        //public string SerialNo { get; set; }

        [DefaultValue(0)]
        public bool IsDelete { get; set; }

        public long LoyaltyCardIssueHeaderID { get; set; }        
        public virtual LoyaltyCardIssueHeader LoyaltyCardIssueHeader { get; set; }

        public long LoyaltyCardGenerationDetailID { get; set; }
        public virtual LoyaltyCardGenerationDetail LoyaltyCardGenerationDetail { get; set; }

    }
}
