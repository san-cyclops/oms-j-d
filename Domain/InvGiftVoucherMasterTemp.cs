using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class InvGiftVoucherMasterTemp
    {
        public long InvGiftVoucherMasterTempID { get; set; }

        public int InvGiftVoucherBookCodeID { get; set; }

        public int InvGiftVoucherGroupID { get; set; }

        public int CompanyID { get; set; }

        public int LocationID { get; set; }

        [MaxLength(15)]
        public string VoucherNo { get; set; }

        public int VoucherNoSerial { get; set; }

        [MaxLength(4)]
        public string VoucherPrefix { get; set; }

        public int SerialLength { get; set; }

        public decimal GiftVoucherValue { get; set; }

        public decimal GiftVoucherPercentage { get; set; }

        public int StartingNo { get; set; }
        
        public int VoucherCount { get; set; }

        public int PageCount { get; set; }

        public int CurrentSerial { get; set; }

        public string VoucherSerial { get; set; }

        public int VoucherSerialNo { get; set; }

        public int VoucherType { get; set; }

        /*
         * 0 - Default
         * 1 - Request (PO Generated) 
         * 2 - Reeceived (GRN)
         * 3 - Transfered
         * 4 - Issued
         * 5 - Lost / Damage
         * */
        public int VoucherStatus { get; set; }

        public int ToLocationID { get; set; }

        public bool IsDelete { get; set; }
    }
}
