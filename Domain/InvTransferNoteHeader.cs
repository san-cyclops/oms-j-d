using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class InvTransferNoteHeader : BaseEntity
    {
        public long InvTransferNoteHeaderID { get; set; }

        [DefaultValue(0)]
        public long TransferNoteHeaderID { get; set; }

        [DefaultValue(0)]
	    public int CompanyID { get; set; }

        [DefaultValue(0)]
	    public int LocationID { get; set; }

        [DefaultValue(0)]
	    public int CostCentreID { get; set; }

        [DefaultValue(0)]
	    public int DocumentID { get; set; }

        [DefaultValue("")]
        [MaxLength(20)]
	    public string DocumentNo { get; set; }

	    public DateTime DocumentDate { get; set; }

        [DefaultValue(0)]
        public int TransferTypeID { get; set; }

        [DefaultValue(0)]
	    public int ReferenceDocumentDocumentID { get; set; } 

        [DefaultValue(0)]
        public long ReferenceDocumentID { get; set; }

        [DefaultValue("")]
        [MaxLength(20)]
	    public string ReferenceNo { get; set; }

        [DefaultValue("")]
        [MaxLength(10)]
        public string PosReferenceNo { get; set; }

        [NotMapped]
        [DefaultValue(0)]
        public int TransStatusId { get; set; } 

        [DefaultValue(0)]
	    public int ToLocationID { get; set; }

        [DefaultValue(0)]
	    public decimal GrossAmount { get; set; }

        [DefaultValue(0)]
	    public decimal NetAmount { get; set; }

        [DefaultValue("")]
        [MaxLength(150)]
	    public string Remark { get; set; }

        [DefaultValue(0)]
	    public int DocumentStatus { get; set; }

        [DefaultValue(0)]
        public bool IsDelete { get; set; }

    }
}
