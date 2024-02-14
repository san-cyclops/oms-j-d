using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class LoyaltyCardIssueHeader : BaseEntity
    {
        public long LoyaltyCardIssueHeaderID { get; set; }

        [DefaultValue(0)]
        public long CardIssueHeaderID { get; set; } 

        [DefaultValue("")]
        [MaxLength(4)]
        public string CardPrefix { get; set; }

        [Required]
        [DefaultValue(0)]
        public int CardLength { get; set; }

        [DefaultValue("")]
        [MaxLength(50)]
        public string CardFormat { get; set; }

        public DateTime IssueDate { get; set; }

        [Required]
        [DefaultValue(0)]
        public int StartingNo { get; set; }

        [Required]
        [DefaultValue(0)]
        public int ToLocationId { get; set; }
        
        [DefaultValue("")]
        [MaxLength(50)]
        public string DocumentNo { get; set; }

        [DefaultValue("")]
        [MaxLength(50)]
        public string Remark { get; set; }

        [DefaultValue("")]
        [MaxLength(50)]
        public string ReferenceNo { get; set; }

        [Required]
        [DefaultValue(0)]
        public long EmployeeId { get; set; }


        [Required]
        [DefaultValue(0)]
        public long CardTypeId { get; set; }


        [DefaultValue(0)]
        public bool IsDelete { get; set; }

        public virtual ICollection<LoyaltyCardIssueDetail> LoyaltyCardIssueDetails { get; set; }

    }
}
