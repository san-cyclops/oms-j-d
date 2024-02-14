﻿using System;
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
    public class LgsMaintenanceJobAssignEmployeeDetail : BaseEntity
    {
        public long LgsMaintenanceJobAssignEmployeeDetailID { get; set; }

        [DefaultValue(0)]
        public long MaintenanceJobAssignEmployeeDetailID { get; set; } 

        /// <summary>
        /// Forign key
        /// </summary>
        [DefaultValue(0)]
        public long LgsMaintenanceJobAssignHeaderID { get; set; }

        public LgsMaintenanceJobAssignHeader LgsMaintenanceJobAssignHeader { get; set; }

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
        public int DocumentStatus { get; set; }

        public long EmployeeID { get; set; }

        [Required]
        [MaxLength(15)]
        public string EmployeeCode { get; set; }        

        
    }
}
