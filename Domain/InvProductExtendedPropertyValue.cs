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
    /// <summary>
    /// developed by - asanka
    /// </summary>
    public class InvProductExtendedPropertyValue : BaseEntity
    {
        public long InvProductExtendedPropertyValueID { get; set; }


        [Required]
        public long ProductID { get; set; }
        
        [Required]
        public long InvProductExtendedPropertyID { get; set; }

        [NotMapped]
        [MaxLength(50)]
        public string ExtendedPropertyName { get; set; }

        [Required]
        public long InvProductExtendedValueID { get; set; }

        [NotMapped]
        [MaxLength(50)]
        public string ValueData { get; set; }

        [DefaultValue(0)]
        public bool IsDelete { get; set; }

        //public virtual InvProductMaster InvProductMaster { get; set; }
    }
}
