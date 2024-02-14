using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class InvSalesHeader : BaseEntity
    {
        public long InvSalesHeaderID { get; set; }

        [Required]
        public long SalesHeaderID { get; set; }

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
        public long CustomerID { get; set; }

        [DefaultValue(0)]
        public long SalesPersonID { get; set; }

        [DefaultValue(0)]
        public decimal GrossAmount { get; set; }

        [DefaultValue(0)]
        public decimal ExchangeRate { get; set; }

        [DefaultValue(0)]
        public decimal DiscountAmount { get; set; }

        [DefaultValue(0)]
        public decimal OtherChargers { get; set; }

        [DefaultValue(0)]
        public decimal DiscountPercentage { get; set; }

        [DefaultValue(0)]
        public decimal TaxAmount1 { get; set; }

        [DefaultValue(0)]
        public decimal TaxAmount2 { get; set; }

        [DefaultValue(0)]
        public decimal TaxAmount3 { get; set; }

        [DefaultValue(0)]
        public decimal TaxAmount4 { get; set; }

        [DefaultValue(0)]
        public decimal TaxAmount5 { get; set; }

        [DefaultValue(0)]
        public decimal TaxAmount { get; set; }

        [DefaultValue(0)]
        public decimal NetAmount { get; set; }

        [DefaultValue("")]
        [MaxLength(150)]
        public string Remark { get; set; }

        [DefaultValue(0)]
        public decimal LineDiscountTotal { get; set; }

        [DefaultValue(0)]
        public int PaymentMethodID { get; set; }

        [DefaultValue(0)]
        public int CurrencyID { get; set; }

        [DefaultValue(0)]
        public decimal CurrencyRate { get; set; }

        [DefaultValue(0)]
        public int ReferenceDocumentDocumentID { get; set; }

        [DefaultValue(0)]
        public long ReferenceDocumentID { get; set; }

        [DefaultValue("")]
        [MaxLength(20)]
        public string ReferenceNo { get; set; }

        [DefaultValue(0)]
        public int InvReturnTypeID { get; set; }

        [DefaultValue(0)]
        public long CashierID { get; set; }

        [DefaultValue(0)]
        public int BillTypeID { get; set; }

        [DefaultValue(0)]
        public int CustomerType { get; set; }

        [DefaultValue(0)]
        public int SaleTypeID { get; set; }

        [DefaultValue(0)]
        public int UnitNo { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public long ZNo { get; set; }

        [DefaultValue(0)]
        public bool IsDispatch { get; set; }
        
        public int TransStatus { get; set; }

        [DefaultValue(0)]
        /*
         @ Invoice
         * // 0 - Invoice, 1 - Quotation, 2 - Performa Invoice, 3 - Dispatch
         * @ Dispatch
         * // 4 - dispatch, 5 - Invoice, 6 - Sales Order
         */
        public int RecallDocumentStatus { get; set; } 

        [DefaultValue(0)]
        public bool IsUpLoad { get; set; }

        [DefaultValue(0)]
        public int DocumentStatus { get; set; } // 0 - paused , 1 - saved , 3 - cancelled
 
        //[DefaultValue(0)]
        //public bool IsDelete { get; set; }
        [DefaultValue(0)]
        public bool IsDeliver { get; set; }
    }
}
