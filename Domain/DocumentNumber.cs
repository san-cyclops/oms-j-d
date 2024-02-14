using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class DocumentNumber : BaseEntity
    {
        public long DocumentNumberID { get; set; }

        [DefaultValue(0)]
	    public int DocumentID { get; set; }

        [DefaultValue("")]
	    public string DocumentName { get; set; }

        [DefaultValue(0)] 
	    public int CompanyID { get; set; }

        [DefaultValue(0)]
	    public int LocationID { get; set; }

        [DefaultValue(0)]
	    public long DocumentNo { get; set; }

        [DefaultValue(0)]
	    public long TempDocumentNo { get; set; }

        [DefaultValue(0)]
	    public int DocumentYear { get; set; }

        [DefaultValue("")]
	    public string PrefixCode { get; set; } 
    }
}
