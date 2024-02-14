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
    public class ColorPalate  :   BaseEntity
    {
        public long ColorPalateID { get; set; }

        [Required]
        [MaxLength(50)]
	    public string ColorCode { get; set; }

        [Required]
        [MaxLength(50)]
	    public string ColorName { get; set; }

        [Required]
        [MaxLength(50)]
        public string StandardColorCode { get; set; }

        [Required]
        [MaxLength(50)]
        public string StandardColorName { get; set; }

        [MaxLength(150)]
        [DefaultValue("")]
	    public string Remark { get; set; }

        [DefaultValue(0)]
	    public bool IsDelete { get; set; } 
        
    }
}
