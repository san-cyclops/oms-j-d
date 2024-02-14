using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class LoyaltyCardAllocationLog :BaseEntity
    {
        public int LoyaltyCardAllocationLogID { get; set; }

        public int LoyaltyCardAllocationID { get; set; }

        [Required]
        [DefaultValue(0)]
        public long CardTypeId { get; set; }

        [Required]
        [DefaultValue(0)]
        public long CustomerId { get; set; }

        [DefaultValue("")]
        [MaxLength(150)]
        public string SerialNo { get; set; }

        [DefaultValue("")]
        [MaxLength(150)]
        public string CardNo { get; set; }

        [DefaultValue("")]
        [MaxLength(150)]
        public string EncodeNo { get; set; }

        [DefaultValue(0)]
        public bool IsDelete { get; set; }
    }
}
