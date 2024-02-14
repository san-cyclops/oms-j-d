using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class LostAndRenew : BaseEntity
    {
        public long LostAndRenewID { get; set; }

        public long LoyaltyCustomerID { get; set; }

        [MaxLength(50)]
        public string OldCardNo { get; set; }

        [MaxLength(50)]
        public string NewCardNo { get; set; }  

        [MaxLength(50)]
        public string OldCustomerCode { get; set; }

        [MaxLength(50)]
        public string NewCustomerCode { get; set; } 

        [MaxLength(50)]
        public string OldEncodeNo { get; set; }

        [MaxLength(50)]
        public string NewEncodeNo { get; set; } 

        [MaxLength(50)]
        public string Remark { get; set; }

        public DateTime RenewedDate { get; set; } 


    }
}
