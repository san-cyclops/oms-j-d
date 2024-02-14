using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class LoyaltyCardGenerationDetail
    {
        public long LoyaltyCardGenerationDetailID { get; set; }

        [DefaultValue(0)]
        public long CardGenerationDetailID { get; set; } 

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

        [Required]
        [MaxLength(50)]
        public string CardNo { get; set; }

        [Required]
        [MaxLength(50)]
        public string SerialNo { get; set; }

        [MaxLength(50)]
        public string EncodeNo { get; set; }

        [DefaultValue(0)]
        public bool IsIssued { get; set; }

        /// <summary>
        /// Card in use or not
        /// </summary>
        [DefaultValue(0)]
        public bool IsActive { get; set; }

        [DefaultValue(0)]
        public bool IsDelete { get; set; }

        public long LoyaltyCardGenerationHeaderID { get; set; }
        
        public virtual LoyaltyCardGenerationHeader LoyaltyCardGenerationHeader { get; set; }
    }
}
