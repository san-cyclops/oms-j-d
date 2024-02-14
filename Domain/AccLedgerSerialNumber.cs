using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class AccLedgerSerialNumber:BaseEntity
    {
        public long AccLedgerSerialNumberID { get; set; }

        [DefaultValue(0)]
        public int DocumentID { get; set; }

        [DefaultValue("")]
        public string DocumentName { get; set; }

        [DefaultValue(0)]
        public int CompanyID { get; set; }

        [DefaultValue(0)]
        public int LocationID { get; set; }

        [DefaultValue(0)]
        public long LedgerID { get; set; }

        [DefaultValue(0)]
        public long DocumentNo { get; set; }

        [DefaultValue(0)]
        public long TempDocumentNo { get; set; }

        public DateTime financialBeginDate { get; set; }

        public DateTime financialEndingDate { get; set; }

        [DefaultValue("")]
        public string PrefixCode { get; set; } 
    }
}
