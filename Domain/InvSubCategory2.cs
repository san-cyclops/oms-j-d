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
    public class InvSubCategory2 : BaseEntity
    {
        public long InvSubCategory2ID { get; set; }

        [Required]
        public long InvSubCategoryID { get; set; }

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

        public InvSubCategory InvSubCategory { get; set; }
        //public virtual ICollection<InvProductMaster> InvProductMasters { get; set; }

        [NotMapped]
        public string SubCategory2CodeAndName { get { return SubCategory2Code + " " + SubCategory2Name; } }   


    }
}
