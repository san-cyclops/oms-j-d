using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class UnitOfMeasure : BaseEntity
    {
        #region Public Properties
        public long UnitOfMeasureID { get; set; }

        [Required]
        [MaxLength(15)]
        public string UnitOfMeasureCode { get; set; }

        [Required]
        [MaxLength(50)]
        public string UnitOfMeasureName { get; set; }

        [DefaultValue("")]
        [MaxLength(150)]
        public string Remark { get; set; }

        [DefaultValue(0)]
        public bool IsDelete { get; set; }


        public virtual ICollection<InvProductMaster> InvProductMasters { get; set; }

        #endregion
    }
}
