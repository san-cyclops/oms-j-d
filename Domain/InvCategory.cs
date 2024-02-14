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
    public class InvCategory : BaseEntity
    {
        public long InvCategoryID { get; set; }

        [Required]
        public long InvDepartmentID { get; set; }

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

        public InvDepartment InvDepartment { get; set; }

        public virtual ICollection<InvSubCategory> InvSubCategories { get; set; }
        //public virtual ICollection<InvProductMaster> InvProductMasters { get; set; }

        [NotMapped]
        public string CategoryCodeAndName { get { return CategoryCode + " " + CategoryName; } }  

    }

}
