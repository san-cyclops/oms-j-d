using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class InvReturnType
    {
        public int InvReturnTypeID { get; set; } 

        [MaxLength(50)]
        public string ReturnType { get; set; } 

        [DefaultValue(0)]
        public int RelatedFormID { get; set; } 

        [DefaultValue(0)]
        public bool IsDelete { get; set; }
    }
}
