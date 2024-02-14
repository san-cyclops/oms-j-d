using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class InvBasketAnalysisSelectedLocationsTemp
    {
        public long InvBasketAnalysisSelectedLocationsTempID { get; set; }
        public int LocationID { get; set; }
        [MaxLength(100)]
        public string LocationDescription { get; set; }
        public string PcName { get; set; }
        [MaxLength(50)]
        public string UserName { get; set; }
    }
}
