using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class InvDamageType
    {
        public int InvDamageTypeID { get; set; }

        [DefaultValue("")]
        [MaxLength(30)]
        public string DamageType { get; set; }

        [DefaultValue(0)]
        public bool IsDelete { get; set; }

    }
}
