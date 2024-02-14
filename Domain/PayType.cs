using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class PayType : BaseEntity
    {
        public long  PayTypeID { get; set; }

        [Required]
        [DefaultValue(0)]
        public long PaymentID { get; set; }

        [Required]
        [MaxLength(15)]
        [DefaultValue("")]
        public string Descrip { get; set; }

        [Required]
        [DefaultValue(0)]
        public bool IsSwipe { get; set; }

        [Required]
        [DefaultValue(0)]
        public int Type { get; set; }

        [Required]
        [DefaultValue(0)]
        public decimal Rate { get; set; }

        [Required]
        [DefaultValue(0)]
        public bool IsRefundable { get; set; }

        [Required]
        [DefaultValue(0)]
        public bool IsActive { get; set; }

        [Required]
        [DefaultValue(0)]
        public bool IsBillCopy { get; set; }

        [Required]
        [MaxLength(12)]
        [DefaultValue("")]
        public string PrintDescrip { get; set; }

        [Required]
        [MaxLength(5)]
        [DefaultValue("")]
        public string PreFix { get; set; }

        [Required]
        [DefaultValue(0)]
        public int MaxLength { get; set; }

        [DefaultValue(0)]
        public bool IsDelete { get; set; }
    }
}
