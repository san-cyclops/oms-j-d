using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class LgsSubCategory2 : BaseEntity
    {
        public long LgsSubCategory2ID { get; set; }

        [Required]
        public long LgsSubCategoryID { get; set; } 

        [Required]
        [MaxLength(15)]
        public string SubCategory2Code { get; set; }

        [Required]
        [MaxLength(50)]
        public string SubCategory2Name { get; set; }

        [DefaultValue("")]
        [MaxLength(150)]
        public string Remark { get; set; }

        [DefaultValue(0)]
        public bool IsDelete { get; set; }

        public LgsSubCategory LgsSubCategory { get; set; }
        //public virtual ICollection<LgsProductMaster> LgsProductMasters { get; set; } 
    }
}
