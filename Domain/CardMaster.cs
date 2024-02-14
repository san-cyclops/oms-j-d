using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class CardMaster : BaseEntity
    {
        public long CardMasterID { get; set; }

        [DefaultValue(0)]
        public int CardType { get; set; }

        [Required]
        [MaxLength(15)]
        public string CardCode { get; set; }

        [Required]
        [MaxLength(50)]
        public string CardName { get; set; }

        [DefaultValue(0)]
        public decimal Discount { get; set; }

        [DefaultValue(0)]
        public decimal PointValue { get; set; }

        [DefaultValue(0)]
        public int MinimumPoints { get; set; }

        [DefaultValue(0)]
        public decimal ReDeemPointValue { get; set; }

        [DefaultValue("")]
        [MaxLength(150)]
        public string Remark { get; set; }

        [DefaultValue(0)]
        public bool IsDelete { get; set; }
    }
}
