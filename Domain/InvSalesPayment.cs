using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class InvSalesPayment : BaseEntity
    {
        public long InvSalesPaymentID { get; set; } 

        [Required]
        public long RowNo { get; set; }

        [DefaultValue(0)]
        public int PayTypeID { get; set; }

        [DefaultValue(0)]
        public decimal Amount { get; set; }

        [DefaultValue(0)]
        public decimal Balance { get; set; }

        public DateTime SDate { get; set; }

        [DefaultValue(0)]
        public string Receipt { get; set; }

        [DefaultValue(0)]
        public long LocationID { get; set; }

        [DefaultValue(0)]
        public long CashierID { get; set; } 

        [Required]
        public Int64 UnitNo { get; set; } 

        [DefaultValue(0)]
        public Int64 BillTypeID { get; set; }

        [DefaultValue(0)]
        public string RefNo { get; set; }

        [DefaultValue(0)]
        public long BankId { get; set; }

        
        public DateTime ChequeDate { get; set; }

        [DefaultValue(0)]
        public bool IsRecallAdv { get; set; }

        [DefaultValue(0)]
        public string RecallNo { get; set; }

        [DefaultValue(0)]
        public string Descrip { get; set; }

        [DefaultValue(0)]
        public string EnCodeName { get; set; }

        [DefaultValue(0)]
        public Int64 UpdatedBy { get; set; }

        [DefaultValue(0)]
        public int Status { get; set; }

        [DefaultValue(0)]
        public long CustomerID { get; set; }

        [DefaultValue(0)]
        public int CustomerType { get; set; }

        [DefaultValue(0)]
        public long ZNo { get; set; }



    }
}
