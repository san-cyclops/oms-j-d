using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class InvProductExpiaryTemp
    {
        public long InvProductExpiaryTempID { get; set; }

        [DefaultValue(0)]
        public int DocumentID { get; set; }

        [DefaultValue(0)]
        public long LineNo { get; set; }

        [DefaultValue(0)]
        public long ProductID { get; set; }

        [DefaultValue(0)]
        public long UnitOfMeasureID { get; set; }

        public DateTime? ExpiryDate { get; set; }
    }
}
