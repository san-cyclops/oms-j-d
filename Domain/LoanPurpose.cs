using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    class LoanPurpose : BaseEntity
    {
        public int LoanPurposeID { get; set; }

        [Required()]
        [MaxLength(15)]
        public string LoanPurposeCode { get; set; }

        [Required()]
        [MaxLength(50)]
        public string LoanPurposeName { get; set; }

        [DefaultValue("")]
        [MaxLength(150)]
        public string Remark { get; set; }

        [DefaultValue(0)]
        public bool IsDelete { get; set; }
    }
}
