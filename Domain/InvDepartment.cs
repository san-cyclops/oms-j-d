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
    public class InvDepartment : BaseEntity
    {
        public long InvDepartmentID { get; set; }

        [Required()]
        [MaxLength(15)]
        public string DepartmentCode { get; set; }

        [Required()]
        [MaxLength(50)]
        public string DepartmentName { get; set; }

        [MaxLength(150)]
        public string Remark { get; set; }

        [DefaultValue(0)]
        public bool IsDelete { get; set; }

        [System.Xml.Serialization.XmlIgnoreAttribute]
        public virtual ICollection<InvCategory> InvCategories { get; set; }
        //public virtual ICollection<InvProductMaster> InvProductMasters { get; set; }

        [NotMapped]
        public string DepartmentCodeAndName { get { return DepartmentCode + " " + DepartmentName; } } 

    }
}
