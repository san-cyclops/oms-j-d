﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class InvProductPriceChangeDetail : BaseEntity
    {
        public long InvProductPriceChangeDetailID { get; set; }

        [DefaultValue(0)]
        public long InvProductPriceChangeHeaderID { get; set; }

        [DefaultValue(0)]
	    public int LocationID { get; set; }

        [DefaultValue(0)]
	    public long ProductID { get; set; } 

        [DefaultValue("")]
        [MaxLength(25)]
	    public string ProductCode { get; set; }

        [DefaultValue("")]
        [MaxLength(100)]
	    public string ProductName { get; set; }

        [DefaultValue("")]
        [MaxLength(20)]
	    public string DocumentNo { get; set; } 

	    public DateTime DocumentDate { get; set; }

        [DefaultValue(0)]
	    public decimal OldCostPrice { get; set; }

        [DefaultValue(0)]
	    public decimal NewCostPrice { get; set; }

        [DefaultValue(0)]
	    public decimal OldSellingPrice { get; set; }

        [DefaultValue(0)]
	    public decimal NewSellingPrice { get; set; }

        [DefaultValue(0)]
	    public decimal CurrentStock { get; set; }

        [DefaultValue(0)]
        public long UnitOfMeasureID { get; set; }

        [DefaultValue("")]
        [MaxLength(50)]
	    public string BatchNo { get; set; } 

	    public DateTime EffectFromDate { get; set; }

        [DefaultValue(0)]
	    public decimal Percentage { get; set; }

        [DefaultValue(0)]
	    public decimal MRP { get; set; }

        [DefaultValue(0)]
	    public bool IsDelete { get; set; }

        [DefaultValue(0)]
        public int DocumentID { get; set; }

        [DefaultValue(0)]
        public long LineNo { get; set; }

        [DefaultValue(0)]
        public int Process { get; set; }

        [DefaultValue(0)]
        public int DocumentStatus { get; set; }
    }
}