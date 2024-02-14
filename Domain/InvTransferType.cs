using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class InvTransferType
    {
        public int InvTransferTypeID { get; set; }

        [DefaultValue("")]
        [MaxLength(25)]
        public string TransferType { get; set; }

        [DefaultValue(0)]
        public bool IsDelete { get; set; }

    }
}
