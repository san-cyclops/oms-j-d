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
    public class Location : BaseEntity
    {
        public int LocationID { get; set; }

        [Required]
        public int CompanyID { get; set; }

        [Required]
        [MaxLength(15)]
        public string LocationCode { get; set; }

        [Required]
        [MaxLength(50)]
        public string LocationName { get; set; } 

        [Required]
        [MaxLength(50)]
	    public string Address1 { get; set; }

        [Required]
        [MaxLength(50)]
	    public string Address2 { get; set; }

        [MaxLength(50)]
	    public string Address3 { get; set; }

        [MaxLength(50)]
        public string Telephone { get; set; }

        [MaxLength(50)]
        public string Mobile { get; set; }

        [MaxLength(50)]
        public string FaxNo { get; set; }

        [MaxLength(50)]
        public string Email { get; set; }

        [MaxLength(100)]
        public string ContactPersonName { get; set; }

        [DefaultValue("")]
        [MaxLength(100)]
        public string OtherBusinessName { get; set; }

        [MaxLength(3)]
        public string LocationPrefixCode { get; set; }

        [MaxLength(50)]
        public string TypeOfBusiness { get; set; }

        [MaxLength(50)]
        public string CostingMethod { get; set; }

        [DefaultValue(0)]
        public int CostCentreID { get; set; }

        [DefaultValue(0)]
        public bool IsVat { get; set; }

        [DefaultValue(0)]
	    public bool IsStockLocation { get; set; }

        [DefaultValue(0)]
	    public bool IsDelete { get; set; }

        [DefaultValue(0)]
        public bool IsHeadOffice { get; set; }

        [DefaultValue(0)]
        public string UploadPath { get; set; }

        [DefaultValue(0)]
        public string DownloadPath { get; set; }

        [DefaultValue(0)]
        public string LocalUploadPath { get; set; }

        [DefaultValue(0)]
        public string BackupPath { get; set; } 

        public Company Company { get; set; }
        //public virtual ICollection<InvProductMaster> InvProductMasters { get; set; }

        [NotMapped]
        [DefaultValue(0)]
        public bool IsSelect { get; set; }

        [DefaultValue(0)]
        public bool IsActive { get; set; }

        [MaxLength(50)]
        public string LocationIP { get; set; }  

    }
}
