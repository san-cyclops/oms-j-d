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
    public class InvPosTerminalDetails : BaseEntity
    {
        public long InvPosTerminalDetailsID { get; set; }

        public int LocationID { get; set; }

        public int TerminalId { get; set; }

        public string IP { get; set; }

        public string DBNAME { get; set; }

        public string UserId { get; set; }

        public string PWD { get; set; }

    }
}
