using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class TitleMaster : BaseEntity 
    {
        public int TitleMasterID { get; set; }

        [DefaultValue("")]
        [MaxLength(15)]
        public string TitleMasterCode { get; set; }

        [DefaultValue(0)]
        public bool IsDelete { get; set; } 
    }
}
