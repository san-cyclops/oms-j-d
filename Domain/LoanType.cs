using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class LoanType : BaseEntity
    {
        public int LoanTypeID { get; set; }

        [Required()]
        [MaxLength(15)]
        public string LoanTypeCode { get; set; }

        [Required()]
        [MaxLength(50)]
        public string LoanTypeName { get; set; }

        [DefaultValue("")]
        [MaxLength(150)]
        public string Remark { get; set; }

        [DefaultValue(0)]
        public bool IsDelete { get; set; }
    }
}
