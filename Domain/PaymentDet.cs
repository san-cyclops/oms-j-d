    using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{

    public class PaymentDet
    {
        public long PaymentDetID { get; set; }

        [Required]
        [DefaultValue(0)]
        public Int64  RowNo { get; set; }

        [Required]
        [DefaultValue(0)]
        public Int64  PayTypeID { get; set; }

        [Required]
        [DefaultValue(0)]
        public decimal  Amount { get; set; }

        [Required]
        [DefaultValue(0)]
        public decimal  Balance { get; set; }

        [Required]
        public DateTime SDate { get; set; }

        [Required]
        [MaxLength(10)]
        [DefaultValue("")]
        public string  Receipt { get; set; }

        [Required]
        [DefaultValue(0)]
        public int  LocationID { get; set; }

        [Required]
        [DefaultValue(0)]
        public Int64  CashierID { get; set; }

        [Required]
        [DefaultValue(0)]
        public int   UnitNo { get; set; }

        [Required]
        [DefaultValue(0)]
        public int  BillTypeID { get; set; }

        [Required]
        [DefaultValue(0)]
        public int SaleTypeID { get; set; }

        [Required]
        [MaxLength(30)]
        [DefaultValue("")]
        public string   RefNo { get; set; }

        [Required]
        [DefaultValue(0)]
        public Int64  BankId { get; set; }

        public DateTime  ?ChequeDate { get; set; }

        [Required]
        [DefaultValue(0)]
        public bool  IsRecallAdv { get; set; }

        [Required]
        [MaxLength(10)]
        [DefaultValue("")]
        public string  RecallNo { get; set; }

        [Required]
        [MaxLength(20)]
        [DefaultValue(0)]
        public string  Descrip { get; set; }

        [Required]
        [MaxLength(50)]
        [DefaultValue("")]
        public string  EnCodeName { get; set; }

        [Required]
        [DefaultValue(0)]
        public Int64  UpdatedBy { get; set; }

        [Required]
        [DefaultValue(0)]
        public int  Status { get; set; }

        [Required]
        [DefaultValue(0)]
        public long ZNo { get; set; }

        public int CustomerId { get; set; }

        public int CustomerType { get; set; }

        [DefaultValue(0)]
        [MaxLength(25)]
        public string CustomerCode { get; set; }

        [DefaultValue(0)]
        public int GroupOfCompanyID { get; set; }

        [DefaultValue(0)]
        public int Datatransfer { get; set; }

        public DateTime? ZDate { get; set; }

        [DefaultValue(0)]
        public int TerminalID { get; set; }

        [DefaultValue(0)]
        public int LoyaltyType { get; set; }  
    }
}
