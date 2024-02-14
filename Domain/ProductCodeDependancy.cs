using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class ProductCodeDependancy : BaseEntity
    {
        public int ProductCodeDependancyID { get; set; }

        public string FormName { get; set; }

        [DefaultValue(0)]
        public bool DependOnDepartment { get; set; }

        [DefaultValue(0)]
        public bool DependOnCategory { get; set; }

        [DefaultValue(0)]
        public bool DependOnSubCategory { get; set; }  

        [DefaultValue(0)]
        public bool DependOnSubCategory2 { get; set; }   
    }
}
