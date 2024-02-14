using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class LoyaltySuplimentary : BaseEntity
    {
        public long LoyaltySuplimentaryID { get; set; }

        
        [DefaultValue(0)]
        public long LoyaltyCustomerID { get; set; }

        [DefaultValue(0)]
        public long  CardTypeId { get; set; }

        [Required]
        [DefaultValue(0)]
        public int RelationShipId { get; set; }

        [MaxLength(50)]
        public string CardNo { get; set; }

        [MaxLength(50)]
        public string Name { get; set; }

        [DefaultValue(0)]
        public int Status { get; set; }

        [DefaultValue(0)]
        public bool IsDelete { get; set; }

        public virtual ICollection<LoyaltyCustomer> LoyaltyCustomers { get; set; }

    }
}
