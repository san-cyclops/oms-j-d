using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class LocationAssignedCostCentre : BaseEntity
    {
        public int LocationAssignedCostCentreID { get; set; }

        [Required]
        public int LocationID { get; set; }

        [Required]
        public int CostCentreID { get; set; }

        [Required]
        public bool Select { get; set; }

        [DefaultValue(0)] 
        public bool IsDelete { get; set; } 
    }
}
