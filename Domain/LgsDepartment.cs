using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class LgsDepartment : BaseEntity
    {
        public long LgsDepartmentID { get; set; } 

        [Required]
        [MaxLength(15)]
        public string DepartmentCode { get; set; }

        [Required]
        [MaxLength(50)]
        public string DepartmentName { get; set; }

        [MaxLength(150)]
        public string Remark { get; set; }

        [DefaultValue(0)]
        public bool IsDelete { get; set; }

        public virtual ICollection<LgsCategory> LgsCategories { get; set; } 
    }
}
