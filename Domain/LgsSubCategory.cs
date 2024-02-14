using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class LgsSubCategory : BaseEntity
    {
        public long LgsSubCategoryID { get; set; } 

        [Required]
        public long LgsCategoryID { get; set; } 

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

        public LgsCategory LgsCategory { get; set; }

        public virtual ICollection<LgsSubCategory2> LgsSubCategories2 { get; set; }
        //public virtual ICollection<LgsProductMaster> LgsProductMasters { get; set; } 
    }
}
