using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class ReferenceType : BaseEntity
    {
        public int ReferenceTypeID { get; set; }

        [DefaultValue("")]
        [MaxLength(25)]
        public string LookupType { get; set; }

        [DefaultValue(0)]
        public int LookupKey { get; set; }

        [DefaultValue("")]
        [MaxLength(100)]
        public string LookupValue { get; set; }

        [DefaultValue("")]
        [MaxLength(100)]
        public string Remark { get; set; }

        [DefaultValue(0)]
	    public bool IsDelete { get; set; }
    }
}
