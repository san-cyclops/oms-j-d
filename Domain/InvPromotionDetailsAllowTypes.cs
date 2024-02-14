using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class InvPromotionDetailsAllowTypes:BaseEntity
    {
        public long InvPromotionDetailsAllowTypesID { get; set; }

        [DefaultValue(0)]
        public long InvPromotionMasterID { get; set; }

        [DefaultValue(0)]
        public int CompanyID { get; set; }

        [DefaultValue(0)]
        public int LocationID { get; set; }

        [DefaultValue(0)]
        public int TypeID { get; set; }

        public virtual InvPromotionMaster InvPromotionMaster { get; set; }
    }
}
