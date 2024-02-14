using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class AccChequeNoRegisty:BaseEntity
    {
        public long AccChequeNoRegistyID { get; set; }

        [DefaultValue(0)]
        public int CompanyID { get; set; }

        [DefaultValue(0)]
        public int LocationID { get; set; }

        [DefaultValue(0)]
        public int CostCentreID { get; set; }

        [DefaultValue("")]
        [MaxLength(25)]
        public string DocumentNo { get; set; }

        [DefaultValue(0)]
        public int DocumentID { get; set; }

        public DateTime DocumentDate { get; set; }

        [DefaultValue("")]
        [MaxLength(15)]
        [Description("Cheque No/ Card No")]
        public string CardNo { get; set; }

        [DefaultValue(0)]
        public long BankBookID { get; set; }
        

        [DefaultValue(0)]
        [Description("0 - Cheque Registered, 1 - Cheque Canceled, 2 - Cheque Issued")]
        public int ChequeStatus { get; set; }

        [DefaultValue(0)]
        public bool IsUpLoad { get; set; }
    }
}
