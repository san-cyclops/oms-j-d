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
    public class InvPromotionDetailsAllowLocations:BaseEntity
    {
        public long InvPromotionDetailsAllowLocationsID { get; set; }

        [DefaultValue(0)]
        public long InvPromotionMasterID { get; set; }

        [DefaultValue(0)]
        public int CompanyID { get; set; }

        [DefaultValue(0)]
        public int LocationID { get; set; }

        [NotMapped]
        public string LocationName { get; set; }

        [NotMapped]
        public bool IsSelect { get; set; }


        [DefaultValue(0)]
        public int PromotionLocationID { get; set; }

        public virtual InvPromotionMaster InvPromotionMaster { get; set; }
    }
}
