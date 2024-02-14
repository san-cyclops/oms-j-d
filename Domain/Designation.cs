using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Designation : BaseEntity
    {
        public long DesignationID { get; set; }

        [DefaultValue("")]
	    public string DesignationCode { get; set; }

        [DefaultValue("")]
	    public string DesinationName { get; set; }

        [DefaultValue(0)]
	    public bool IsDelete { get; set; }
    }
}
