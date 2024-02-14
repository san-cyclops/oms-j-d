using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace Domain
{
    public class InvSalesPerson : BaseEntity
    {
        public long InvSalesPersonID { get; set; }

        [Required]
        [MaxLength(15)]
	    public string SalesPersonCode { get; set; }

        [Required]
        [MaxLength(50)]
        public string SalesPersonName { get; set; }
       

        /// <summary>
        /// commnet by sanjeewa - not using in form view
        /// </summary>
        //[DefaultValue(0)]
	    //public long TerritoryID { get; set; }

        [Required]
        [DefaultValue(0)]
	    public int Gender { get; set; }

        [Required]
        [MaxLength(25)]
	    public string ReferenceNo { get; set; }

        [Required]
        [MaxLength(15)]
	    public string SalesPersonType { get; set; }

        [DefaultValue("")]
        [MaxLength(100)]
        public string Remark { get; set; }

        [Required]
        [MaxLength(100)]
	    public string Address1 { get; set; }

        [Required]
        [MaxLength(100)]
	    public string Address2 { get; set; }

        [DefaultValue("")]
        [MaxLength(100)]
	    public string Address3 { get; set; }

        [DefaultValue("")]
        //[EmailAddress]
	    public string Email { get; set; }

        [DefaultValue("")]
        //[RegularExpression(@"^\s*([\(]?)\[?\s*\d{3}\s*\]?[\)]?\s*[\-]?[\.]?\s*\d{3}\s*[\-]?[\.]?\s*\d{4}$")]
	    public string Telephone { get; set; }

        [Required]
        //[RegularExpression(@"^\s*([\(]?)\[?\s*\d{3}\s*\]?[\)]?\s*[\-]?[\.]?\s*\d{3}\s*[\-]?[\.]?\s*\d{4}$")]
        public string Mobile { get; set; }

        public byte[] Image { get; set; }

        [DefaultValue(0)]
	    public bool IsDelete { get; set; }

        [DefaultValue(0)]
        public int CommissionSchemaID { get; set; }
        
        //public virtual CommissionSchema CommissionSchema { get; set; }
    }
}
