﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class InvPromotionCategoryTemp
    {
        public long InvPromotionMasterID { get; set; }

        [DefaultValue(0)]
        public int CompanyID { get; set; }

        [DefaultValue(0)]
        public int LocationID { get; set; }

        [DefaultValue(0)]
        public long CategoryID { get; set; }

        [DefaultValue("")]
        [MaxLength(25)]
        [Display(Name = "Category Code")]
        public string CategoryCode { get; set; }

        [Required]
        [MaxLength(100)]
        public string CategoryName { get; set; }

        [DefaultValue(0)]
        [Required]
        public decimal Qty { get; set; }

        [DefaultValue(0)]
        public decimal DiscPer { get; set; }

        [DefaultValue(0)]
        public decimal DiscAmount { get; set; }

        [DefaultValue(0)]
        public int Points { get; set; }
    }
}