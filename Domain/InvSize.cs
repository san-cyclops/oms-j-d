using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class InvSize : BaseEntity
    {
        public long InvSizeID { get; set; }

        [Required]
        [MaxLength(15)]
	    public string SizeCode { get; set; }

        [Required]
        [MaxLength(50)]
	    public string SizeName { get; set; }

        [DefaultValue("")]
        [MaxLength(150)]
	    public string Remark { get; set; }

        [DefaultValue(0)]
	    public bool IsDelete { get; set; }
    }
}
