using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    /// <summary>
    ///  Developed by asanka
    /// </summary>
    public class LgsMaterialRequestDetail : BaseEntity
    {
        public long LgsMaterialRequestDetailID { get; set; }

        [DefaultValue(0)]
        public long MaterialRequestDetailID { get; set; } 

        /// <summary>
        /// Forign key
        /// </summary>
        [DefaultValue(0)]
        public long LgsMaterialRequestHeaderID { get; set; }

        public LgsMaterialRequestHeader LgsMaterialRequestHeader { get; set; }

        [DefaultValue(0)]
        public int CompanyID { get; set; }

        [DefaultValue(0)]
        public int LocationID { get; set; }

        [DefaultValue(0)]
        public int CostCentreID { get; set; }

        [DefaultValue(0)]
        public long ProductID { get; set; }

        [DefaultValue("")]
        [MaxLength(25)]        
        public string ProductCode { get; set; }

        [DefaultValue(0)]
        public int DocumentID { get; set; }

        [DefaultValue("")]
        [MaxLength(20)]
        public string DocumentNo { get; set; }

        public DateTime DocumentDate { get; set; }
        
        [DefaultValue(0)]
        public decimal OrderQty { get; set; }

        [DefaultValue(0)]
        public decimal CurrentQty { get; set; }

        [DefaultValue(0)]
        public decimal BalanceQty { get; set; }
                                
        [DefaultValue(0)]
        public decimal PackSize { get; set; }

        [DefaultValue(0)]
        public int PackID { get; set; }

        [DefaultValue("")]
        [MaxLength(5)]
        public string UnitOfMeasure { get; set; }

        [DefaultValue(0)]
        public long UnitOfMeasureID { get; set; }

        [DefaultValue(0)]
        public long BaseUnitID { get; set; }

        [DefaultValue(0)]
        public decimal ConvertFactor { get; set; }

        [DefaultValue(0)]
        public long LineNo { get; set; }

        [DefaultValue(0)]
        public int DocumentStatus { get; set; }
    }
}
