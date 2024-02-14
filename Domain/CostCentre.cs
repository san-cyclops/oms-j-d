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
    public class CostCentre : BaseEntity
    {
        public int CostCentreID { get; set; }

        [Required]
        public string CostCentreCode { get; set; }

        [Required]
        public string CostCentreName { get; set; }

        [DefaultValue(0)]
        public bool IsDelete { get; set; }

        [NotMapped]
        public bool Select { get; set; }

        public virtual ICollection<Company> Companies { get; set; }
    }
}
