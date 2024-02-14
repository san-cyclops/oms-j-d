using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class LgsProductLink : BaseEntity
    {
        public long LgsProductLinkID { get; set; } 

        [DefaultValue(0)]
        public long ProductID { get; set; }

        [DefaultValue("")]
        [MaxLength(25)]
        public string ProductLinkCode { get; set; }

        [MaxLength(50)]
        public string ProductLinkName { get; set; }

        [DefaultValue(0)]
        public bool IsDelete { get; set; }
    }
}
