using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class CardGenerationSetting :BaseEntity
    {
        public long CardGenerationSettingID { get; set; }

        [DefaultValue(0)]
        public int CardLength { get; set; }

        [DefaultValue(0)]
        public int CardStartingNo { get; set; }

        [MaxLength(3)]
        public string CardNoPrefix { get; set; }

        [DefaultValue(0)]
        public bool IsCardNoOtherPrefix { get; set; }

        [DefaultValue(0)]
        public int SerialLength { get; set; }

        [DefaultValue(0)]
        public int SerialStartingNo { get; set; }

        [MaxLength(3)]
        public string SerialPrefix { get; set; }

        [DefaultValue(0)]
        public bool IsSerialOtherPrefix { get; set; }

        [DefaultValue(0)]
        public int EncodeLength { get; set; }

        [DefaultValue(0)]
        public int EncodeStartingNo { get; set; }

        [MaxLength(3)]
        public string EncodePrefix { get; set; }

        [DefaultValue(0)]
        public bool IsEncodeOtherPrefix { get; set; }

        [DefaultValue(0)]
        public bool IsDelete { get; set; }

    }
}
