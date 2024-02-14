using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace Domain
{
    public class LgsProductSerialNoTemp
    {
        public long LgsProductSerialNoTempID { get; set; }

        [DefaultValue(0)]
        public int DocumentID { get; set; }

        [DefaultValue(0)]
        public long LineNo { get; set; }

        [DefaultValue(0)]
        public long ProductID { get; set; }

        [DefaultValue(0)]
        public long UnitOfMeasureID { get; set; }

        [MaxLength(40)]
        public string BatchNo { get; set; }

        public DateTime? ExpiryDate { get; set; }

        [DefaultValue("")]
        [MaxLength(50)]
        public string SerialNo { get; set; }

    }
}
