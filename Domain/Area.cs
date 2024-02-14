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
    public class Area : BaseEntity
    {
        public long AreaID { get; set; }

        [Required]
        [MaxLength(15)]
        public string AreaCode { get; set; }
        
        [Required]
        [MaxLength(100)]
        public string AreaName { get; set; }

        [MaxLength(150)]
        [DefaultValue("")]
        public string Remark { get; set; }

        [DefaultValue(0)]
        public bool IsDelete { get; set; }

        public virtual ICollection<Territory> Terriotories { get; set; }
        public virtual ICollection<Customer> Customers { get; set; }

        
    }
}
