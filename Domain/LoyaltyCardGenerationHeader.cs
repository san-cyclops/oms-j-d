using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class LoyaltyCardGenerationHeader : BaseEntity
    {
        public long LoyaltyCardGenerationHeaderID { get; set; }

        [DefaultValue(0)]
        public long CardGenerationHeaderID { get; set; } 

        [MaxLength(3)]
        public string CardPrefix { get; set; }

        [DefaultValue(0)]
        public int CardLength { get; set; }

        [DefaultValue(0)]
        public int CardStartingNo { get; set; }

        [DefaultValue(0)]
        public int SerialLength { get; set; }

        [DefaultValue(0)]
        public int SerialStartingNo { get; set; }

        [MaxLength(3)]
        public string SerialPrefix { get; set; }

        [DefaultValue(0)]
        public int EncodeLength { get; set; }

        [DefaultValue(0)]
        public int EncodeStartingNo { get; set; }

        [MaxLength(3)]
        public string EncodePrefix { get; set; }

        public DateTime GeneratedDate { get; set; }


        public int LocationID { get; set; }

        public long CardMasterID { get; set; }


        [DefaultValue(0)]
        public bool IsDelete { get; set; }

        public virtual ICollection<LoyaltyCardGenerationDetail> LoyaltyCardGenerationDetails { get; set; }
    }
}
