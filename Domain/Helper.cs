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
    public class Helper : BaseEntity
    {
        public long HelperID { get; set; } 

        [Required()]
        [MaxLength(15)]
	    public string HelperCode { get; set; }

        [Required()]
        [MaxLength(100)]
	    public string HelperName { get; set; }

        [Required()]
        [MaxLength(15)]
	    public string Gender { get; set; }

        [DefaultValue(0)]
	    public long ReferenceNo { get; set; }

        [MaxLength(100)]
        [DefaultValue("")]
	    public string Address1 { get; set; }

        [MaxLength(100)]
        [DefaultValue("")]
	    public string Address2 { get; set; }

        [MaxLength(100)]
        [DefaultValue("")]
	    public string Address3 { get; set; }

        [NotMapped]
        public string Address { get { return Address1 + "," + Address2 + "," + Address3; } }

        [EmailAddress]
        [DefaultValue("")]
	    public string Email { get; set; }

        [RegularExpression(@"^\s*([\(]?)\[?\s*\d{3}\s*\]?[\)]?\s*[\-]?[\.]?\s*\d{3}\s*[\-]?[\.]?\s*\d{4}$")]
        [DefaultValue("")]
	    public string Telephone { get; set; }

        [RegularExpression(@"^\s*([\(]?)\[?\s*\d{3}\s*\]?[\)]?\s*[\-]?[\.]?\s*\d{3}\s*[\-]?[\.]?\s*\d{4}$")]
        [DefaultValue("")]
	    public string Mobile { get; set; }

	    public byte HelperImage { get; set; }

        [DefaultValue(0)]
	    public bool IsDelete { get; set; }
    }
}
