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
    public class Driver : BaseEntity
    {
        public long DriverID { get; set; }

        [Required]
        [MaxLength(15)]
	    public string DriverCode { get; set; }

        [Required]
        [MaxLength(100)]
	    public string DriverName { get; set; }

        [MaxLength(15)]
        [DefaultValue("")]
	    public string Gender { get; set; }

        [MaxLength(25)]
        [DefaultValue("")]
	    public string ReferenceNo { get; set; }

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

        [DefaultValue("")]
	    public string Telephone { get; set; }

        [DefaultValue("")]
	    public string Mobile { get; set; }

	    public byte DriverImage { get; set; }

        [DefaultValue(0)]
	    public bool IsDelete { get; set; } 
    }
}
