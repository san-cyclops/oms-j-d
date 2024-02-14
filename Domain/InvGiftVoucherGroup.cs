using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class InvGiftVoucherGroup : BaseEntity
    {
        public int InvGiftVoucherGroupID { get; set; }

        [Required]
        [MaxLength(20)]
        [DisplayName("Group Code")]
        public string GiftVoucherGroupCode { get; set; }

        [Required]
        [MaxLength(50)]
        public string GiftVoucherGroupName { get; set; }

        [DefaultValue("")]
        [MaxLength(150)]
        public string Remark { get; set; }

        [DefaultValue(0)]
        public bool IsDelete { get; set; }

        public virtual ICollection<InvGiftVoucherBookCode> InvGiftVoucherMasterBooks { get; set; }
        public virtual ICollection<InvGiftVoucherMaster> InvGiftVoucherMasters { get; set; }
    }
}
