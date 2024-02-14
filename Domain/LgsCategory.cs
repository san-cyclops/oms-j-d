using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class LgsCategory : BaseEntity
    {
        public long LgsCategoryID { get; set; }

        [Required]
        public long LgsDepartmentID { get; set; }

        [Required]
        [MaxLength(15)]
        public string CategoryCode { get; set; }

        [Required]
        [MaxLength(50)]
        public string CategoryName { get; set; }

        [MaxLength(150)]
        [DefaultValue("")]
        public string Remark { get; set; }

        [DefaultValue(0)]
        public bool IsDelete { get; set; }

        public LgsDepartment LgsDepartment { get; set; }

        public virtual ICollection<LgsSubCategory> LgsSubCategories { get; set; }
        //public virtual ICollection<LgsProductMaster> LgsProductMasters { get; set; } 
    }
}
