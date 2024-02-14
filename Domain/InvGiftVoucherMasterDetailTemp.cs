using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sarasa.ERP.Domain
{
    public class InvGiftVoucherMasterDetailTemp
    {
        public long InvGiftVoucherMasterDetailID { get; set; }

        public long InvGiftVoucherMasterHeaderID { get; set; }

        [DefaultValue(0)]
        public string VoucherSerial { get; set; }

        [DefaultValue(0)]
        [Description("0 - Default, 1 - Request, 2 - Reeceived, 3 - Transfered, 4 - Issued, 5 - Lost / Damage")]
        /*
         * 0 - Default
         * 1 - Request (PO Generated) 
         * 2 - Reeceived (GRN)
         * 3 - Transfered
         * 4 - Issued
         * 5 - Lost / Damage
         * */
        public int VoucherStatus { get; set; }

        [DefaultValue(0)]
        public bool IsDelete { get; set; }

        public virtual InvGiftVoucherMasterHeader InvGiftVoucherMasterHeader { get; set; }
    }
}
