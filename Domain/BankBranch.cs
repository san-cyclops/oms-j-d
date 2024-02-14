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
    public class BankBranch : BaseEntity
    {
        public int BankBranchID { get; set; }

        [Required]
        public int BankID { get; set; }

        [Required]
        [MaxLength(15)]
        public string BankBranchCode { get; set; }

        [Required]
        [MaxLength(100)]
        public string BranchName { get; set; }

        [MaxLength(50)]
        public string Address1 { get; set; }

        [MaxLength(50)]
        public string Address2 { get; set; }

        [MaxLength(50)]
        public string Address3 { get; set; }

        [NotMapped]
        public string Address { get { return Address1 + "," + Address2 + "," + Address3; } }

        [DefaultValue("")]
        public string Mobile { get; set; }

        [DefaultValue("")]
        public string FaxNo { get; set; }

        [DefaultValue(0)]
        public bool IsDelete { get; set; }
    }
}
