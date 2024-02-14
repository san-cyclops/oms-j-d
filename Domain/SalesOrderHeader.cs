using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class SalesOrderHeader : BaseEntity
    {
        public long SalesOrderHeaderID { get; set; }

        [DefaultValue(0)]
        public long OrderHeaderID { get; set; } 

        [DefaultValue(0)]
        public int CompanyID { get; set; }

        [DefaultValue(0)]
        public int LocationID { get; set; }

        [DefaultValue(0)]
        public int CostCentreID { get; set; }

        [DefaultValue(0)]
        public int DocumentID { get; set; }

        [MaxLength(20)]
        public string DocumentNo { get; set; }

        [MaxLength(20)]
        public string ReferenceDocumentNo { get; set; }

        [DefaultValue(0)]
        public long CustomerID { get; set; } 

        public long InvSalesPersonID { get; set; }

        public DateTime DocumentDate { get; set; }

        public DateTime DeliverDate { get; set; }


        [DefaultValue(0)]
        public int PaymentMethodID { get; set; }

        [DefaultValue(0)]
        public decimal GrossAmount { get; set; }

        [DefaultValue(0)]
        public decimal DiscountAmount { get; set; }

        [DefaultValue(0)]
        public decimal OtherCharges { get; set; }

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

        [DefaultValue(0)]
        public decimal LineDiscountTotal { get; set; }

        [DefaultValue(0)]
        public int ReferenceDocumentID { get; set; }

        [DefaultValue("")]
        [MaxLength(150)]
        public string Remark { get; set; }

        [DefaultValue("")]
        [MaxLength(20)]
        public string ReferenceNo { get; set; }

        [DefaultValue(0)]
        public int DocumentStatus { get; set; }
        
        [DefaultValue("")]
        [MaxLength(50)]
        public string Unit { get; set; }

        [DefaultValue("")]
        [MaxLength(50)]
        public string Currency { get; set; }

        [DefaultValue("")]
        [MaxLength(50)]
        public string Company { get; set; }

        [DefaultValue("")]
        [MaxLength(50)]
        public string Colour { get; set; }


        [Required]
        public long DepartmentID { get; set; }

        [Required]
        public long CategoryID { get; set; }

        [Required]
        public long SubCategoryID { get; set; }

        [Required]
        public long SubCategory2ID { get; set; }

        [DefaultValue(0)]
        public bool IsDelete { get; set; }

        
    }
}
