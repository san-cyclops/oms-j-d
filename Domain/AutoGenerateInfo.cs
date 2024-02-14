﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class AutoGenerateInfo
    {
        public long AutoGenerateInfoID { get; set; }

        [Required]
        /*
         * 1 - Common   
         * 2 - Invenoty 
         * 3 - Logistic 
         * 4 - CRM     
         * 5 - Accounts 
         * 6 - Gift Voucher 
         * 7 - POS  
         * 8 - Reports  
         * Etc.
         */
        public int ModuleType { get; set; }

        [DefaultValue(0)]
        /*
         * 1 - Common   [1- 1:500, 2 - 501:1000]
         * 2 - Invenoty [1- 1001:1500, 2 - 1501:2000]
         * 3 - Logistic [1- 2001:2500, 2 - 2501:3000]
         * 4 - CRM      [1- 3001:3500, 2 - 3501:4000]
         * 5 - Accounts [1- 4001:4500, 2 - 4501:5000]
         * 6 - Gift Voucher [1- 5001:5500, 2 - 5501:6000]
         * 7 - POS  [1- 6001:6500, 2 - 6500:7000]
         * 
         * 8 - Common Reports  [7001:8000]  ()
         * 9 - Inventory Reports  [8001:11000]  (Sales 8001:9000,  Purchase 9001:10000,  Stock 10001:10500,  Other 10501:11000)
         * 10 - Logistic Reports  [11001:13000] (Purchase 11001:12000,  Stock 12001:12500,  Other 12501:13000)
         * 11 - CRM Reports  [13001:14000]
         * 12 - Accounts Reports  [14001:16000]
         * 13 - Gift Voucher Reports  [16001:17000]
         * 14 - POS Reports  [17001:18000]
         * 15 - Common Features     [18001:19000]
         * 16 - Special User Rights     [19001:19500]
         * Etc.
         */
        public int DocumentID { get; set; }

        [Required]
        public int FormId { get; set; }

        [Required]
        [MaxLength(50)]
        public string FormName { get; set; }

        [Required]
        [MaxLength(100)]
        public string FormText { get; set; }

        [MaxLength(3)]
        public string Prefix { get; set; }

        [Required]
        public int CodeLength { get; set; }

        public int Suffix { get; set; }

        [Required]
        [DefaultValue(1)]
        public bool AutoGenerete { get; set; }

        [Required]
        [DefaultValue(1)]
        public bool AutoClear { get; set; }

        [Required]
        [DefaultValue(1)]
        public bool IsDepend { get; set; }

        [Required]
        [DefaultValue(1)]
        public bool IsDependCode { get; set; }

        [Required]
        [DefaultValue(1)]
        public bool IsSupplierProduct { get; set; }

        [Required]
        [DefaultValue(1)]
        public bool IsOverWriteQty { get; set; } 

        [DefaultValue(0)]
        public bool IsLocationCode { get; set; }

        [MaxLength(3)]
        public string ReportPrefix { get; set; }

        [Required]
        /*
         * 1 - Reference 
         * 2 - Transaction 
         * Etc.
         */
        public int ReportType { get; set; }

        [DefaultValue(0)]
        public bool PoIsMandatory { get; set; }

        [DefaultValue(0)]
        /*
         * true - Allow to recall Dispatch @ invoice
         *      - Not Allow to recall Invoice @ dispatch
         * false - not allow to recall Dispatch @ Invoice
         *       - Allow to recall Invoice @ dispatch
         */
        public bool IsDispatchRecall { get; set; }

        public bool IsBackDated{ get; set; }

        [NotMapped]
        public bool IsAccess { get; set; }

        [NotMapped]
        public bool IsPause { get; set; }

        [NotMapped]
        public bool IsSave { get; set; }

        [NotMapped]
        public bool IsModify { get; set; }

        [NotMapped]
        public bool IsView { get; set; }

        /// <summary>
        /// Remove Not Mapped 
        /// </summary>
        //[NotMapped]
        public bool IsCard { get; set; } // supplier,customer,l.Customer,Employee,

        /// <summary>
        /// Remove Not Mapped 
        /// </summary>
        //[NotMapped]
        public int CardId { get; set; } //Supplier - 1, Customer - 2, Employee - 3- , 

        /// <summary>
        /// Remove Not Mapped 
        /// </summary>
        //[NotMapped]
        public bool IsEntry { get; set; }

        /// <summary>
        /// Remove Not Mapped 
        /// </summary>
        //[NotMapped]
        public bool IsActive { get; set; }

        
        public decimal ExchangeRate { get; set; }


    }
}
