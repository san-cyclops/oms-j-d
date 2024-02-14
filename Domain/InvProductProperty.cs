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
    public class InvProductProperty
    {
        public long InvProductPropertyID { get; set; } 

        public long ProductID { get; set; }

        public long DepartmentID { get; set; }
 
        public long CategoryID { get; set; } 

        public long SubCategoryID { get; set; }

        public long SubCategory2ID { get; set; } 

        public long SupplierID { get; set; }

        [DefaultValue(0)]
        public long CostPrice { get; set; }

        [DefaultValue(0)]
        public long SellingPrice { get; set; }

        [DefaultValue("")]
        public string Brand { get; set; }

        [DefaultValue("")]
        public string Collar { get; set; }
        
        [DefaultValue("")]
        public string Colour { get; set; }

        [DefaultValue("")]
        public string Country { get; set; }

        [DefaultValue("")]
        public string Cut { get; set; }

        [DefaultValue("")]
        public string Embelishment { get; set; }

        [DefaultValue("")]
        public string Fit { get; set; }

        [DefaultValue("")]
        public string Heel { get; set; }

        [DefaultValue("")]
        public string Length { get; set; }

        [DefaultValue("")]
        public string Material { get; set; }

        [DefaultValue("")]
        public string Neck { get; set; }

        [DefaultValue("")]
        public string PatternNo { get; set; }

        [DefaultValue("")]
        public string ProductFeature { get; set; }

        [DefaultValue("")]
        public string Shop { get; set; }

        [DefaultValue("")]
        public string Size { get; set; }

        [DefaultValue("")]
        public string Sleeve { get; set; }

        [DefaultValue("")]
        public string Texture { get; set; }

        [NotMapped]
        public string ProductCode { get; set; }

        [NotMapped]
        public string ProductName { get; set; } 

        
    }
}
