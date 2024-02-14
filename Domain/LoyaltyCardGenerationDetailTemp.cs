using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class LoyaltyCardGenerationDetailTemp
    {
        public long LoyaltyCardGenerationDetailID { get; set; }

        public long LoyaltyCardGenerationHeaderID { get; set; }

        [DefaultValue(0)]
        public string CardNo { get; set; }

        [DefaultValue(0)]
        public string SerialNo { get; set; }

        [DefaultValue(0)]
        public string EncodeNo { get; set; }

        [MaxLength(3)]
        public int SerialStartingNo { get; set; }

        [MaxLength(3)]
        public int CardStartingNo { get; set; }

        [MaxLength(3)]
        public int EncodeStartingNo { get; set; }

        [DefaultValue(0)]
        public bool IsIssued { get; set; }

        [DefaultValue(0)]
        public bool IsSerialExists { get; set; }

        public virtual LoyaltyCardGenerationHeader LoyaltyCardGenerationHeader { get; set; }
    }
}
