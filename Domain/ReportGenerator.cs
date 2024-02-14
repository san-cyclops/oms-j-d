using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class ReportGenerator:BaseEntity
    {
        public long ReportGeneratorID { get; set; }

        [DefaultValue(0)]
        public int Visible { get; set; }

        [DefaultValue("")]
        public string TableName { get; set; }
    }
}
