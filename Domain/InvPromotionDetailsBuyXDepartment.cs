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
    public class InvPromotionDetailsBuyXDepartment:BaseEntity
    {
        public long InvPromotionDetailsBuyXDepartmentID { get; set; }

        [DefaultValue(0)]
        public long InvPromotionMasterID { get; set; }

        [DefaultValue(0)]
        public int CompanyID { get; set; }

        [DefaultValue(0)]
        public int LocationID { get; set; }

        [DefaultValue(0)]
        public long BuyDepartmentID { get; set; }

        [DefaultValue("")]
        [MaxLength(25)]
        [Display(Name = "Department Code")]
        [NotMapped]
        public string DepartmentCode { get; set; }

        [Required]
        [MaxLength(100)]
        [NotMapped]
        public string DepartmentName { get; set; }

        [DefaultValue(0)]
        public decimal BuyQty { get; set; }

        public virtual InvPromotionMaster InvPromotionMaster { get; set; }

       
    }
}
