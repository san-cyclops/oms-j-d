using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class AccPettyCashIOUHeader:BaseEntity
    {
        public long AccPettyCashIOUHeaderID { get; set; }

        [DefaultValue(0)]
        public long PettyCashIOUHeaderID { get; set; } 

        [DefaultValue(0)]
        public int CompanyID { get; set; }

        [DefaultValue(0)]
        public int LocationID { get; set; }

        [Required]
        public long PettyCashLedgerID { get; set; }

        [Required]
        public long EmployeeID { get; set; }
        
        [Required]
        public string PayeeName { get; set; }

        public DateTime DocumentDate { get; set; }

        [DefaultValue(0)]
        public int DocumentID { get; set; }

        [DefaultValue("")]
        [MaxLength(25)]
        public string DocumentNo { get; set; }

        [DefaultValue("")]
        [MaxLength(25)]
        public string LedgerSerialNo { get; set; }

        [Required]
        public decimal Amount { get; set; }

        //[Required]
        //public decimal ReturnedAmount { get; set; }

        [DefaultValue(0)]
        public long ReferenceDocumentID { get; set; }

        [DefaultValue(0)]
        public int ReferenceDocumentDocumentID { get; set; }

        //public DateTime ReferenceDocumentDate { get; set; }

        [DefaultValue(0)]
        public int ReferenceLocationID { get; set; }

        [DefaultValue("")]
        [MaxLength(50)]
        public string Reference { get; set; }

        [DefaultValue("")]
        [MaxLength(150)]
        public string Remark { get; set; }

        [DefaultValue(0)]
        [Description("0 - Default, 1 - Issued, 2 - Bill Settled ")]
        /*
         * 0 - Default
         * 1 - Issued
         * 2 - Bill Settled
         * 3 - 
         * 4 - 
         * 5 - 
         * */
        public int IOUStatus { get; set; }

        [DefaultValue(0)]
        public int DocumentStatus { get; set; }

        [DefaultValue(0)]
        public bool IsUpLoad { get; set; }

        public AccPettyCashIOUHeader()
        {
            AccPettyCashIOUDetails = new List<AccPettyCashIOUDetail>();
        }
        
        public virtual ICollection<AccPettyCashIOUDetail> AccPettyCashIOUDetails { get; set; }
    }
}
