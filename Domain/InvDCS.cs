using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class InvDCS : BaseEntity
    {
        public long InvDCSID { get; set; }

        [DefaultValue(0)]
        public long DcsCode { get; set; }

        [DefaultValue(0)]
        public long ColorID { get; set; }

        [DefaultValue(0)]
        public long SizeID { get; set; }

        [DefaultValue("")]
        [MaxLength(150)]
	    public string Remark { get; set; }

        [DefaultValue(0)]
	    public bool IsDelete { get; set; }
    }
}
