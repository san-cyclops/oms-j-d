using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Domain
{
    public class InvScanner 
    {
        public long InvScannerID { get; set; }

        [DefaultValue("")]
        [MaxLength(10)]
        public string ScannerPrefix { get; set; }

        [DefaultValue("")]
        [MaxLength(25)]
        public string ScannerName { get; set; }

        [DefaultValue(1)]
        public bool IsActive { get; set; }
        
        [DefaultValue(0)]
        public int ScannerStatus { get; set; }

        [DefaultValue(0)]
        public long DocumentNo { get; set; }
    }
}
