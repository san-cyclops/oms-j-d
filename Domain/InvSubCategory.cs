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
    public class InvSubCategory : BaseEntity
    {
        public long InvSubCategoryID { get; set; }

        [Required]
        public long InvCategoryID { get; set; }

        [Required]
        [MaxLength(15)]
	    public string SubCategoryCode { get; set; }

        [Required]
        [MaxLength(50)]
	    public string SubCategoryName { get; set; }

        [DefaultValue("")]
        [MaxLength(150)]
	    public string Remark { get; set; }

        [DefaultValue(0)]
	    public bool IsDelete { get; set; }

        public InvCategory InvCategory { get; set; }

        public virtual ICollection<InvSubCategory2> InvSubCategories2 { get; set; }
        //public virtual ICollection<InvProductMaster> InvProductMasters { get; set; }

        [NotMapped]
        public string SubCategoryCodeAndName { get { return SubCategoryCode + " " + SubCategoryName; } }  


    }
}
