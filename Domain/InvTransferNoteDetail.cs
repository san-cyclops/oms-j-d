using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class InvTransferNoteDetail : BaseEntity
    {
        public long InvTransferNoteDetailID { get; set; }

        [DefaultValue(0)]
        public long TransferNoteDetailID { get; set; }

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
        public long LineNo { get; set; }

        [DefaultValue(0)]
	    public long ProductID { get; set; }

        [DefaultValue("")]
        [MaxLength(50)]
        public string BatchNo { get; set; }

        public DateTime BatchExpiryDate { get; set; }

        [DefaultValue(0)]
        public decimal AvgCost { get; set; }

        [DefaultValue(0)]
        public decimal CostPrice { get; set; }

        [DefaultValue(0)]
        public decimal SellingPrice { get; set; }

        [DefaultValue(0)]
        public int PackID { get; set; }

        [DefaultValue(0)]
	    public decimal Qty { get; set; }

        [DefaultValue(0)]
	    public decimal OrderQty { get; set; }

        [DefaultValue(0)]
	    public decimal CurrentQty { get; set; }

        [DefaultValue(0)]
        public decimal QtyConvertFactor { get; set; }

        [DefaultValue(0)]
        public long BaseUnitID { get; set; }

        [DefaultValue(1)]
        public decimal ConvertFactor { get; set; }

        [DefaultValue(0)]
        public long UnitOfMeasureID { get; set; }

        [DefaultValue(0)]
	    public decimal GrossAmount { get; set; }

        [DefaultValue(0)]
	    public decimal NetAmount { get; set; }        

        [DefaultValue(0)]
	    public int ToLocationID { get; set; }

        [DefaultValue(0)]
	    public int DocumentStatus { get; set; }

        [DefaultValue(0)]
        public bool IsBatch { get; set; }
    }
}
