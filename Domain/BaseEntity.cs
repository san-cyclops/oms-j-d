using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Utility;

namespace Domain
{
    /// <summary>
    ///  Created by - asanka
    ///  Base class for the doamin
    /// </summary>
    public class BaseEntity : IBaseEntity
    {

        public BaseEntity( )
        {
            GroupOfCompanyID = Common.GroupOfCompanyID;
            CreatedUser = Common.LoggedUser;
            CreatedDate = DateTime.Now;
            ModifiedUser = Common.LoggedUser;
            ModifiedDate = DateTime.Now;
            DataTransfer = 0;
        }

        public int GroupOfCompanyID { get; set; }
        [MaxLength(50)]
        public string CreatedUser { get; set; }
        public DateTime CreatedDate { get; set; }
        [MaxLength(50)]
        public string ModifiedUser { get; set; }
        public DateTime ModifiedDate { get; set; }
         [DefaultValue(0)]
        public int DataTransfer { get; set; }
 
    }
}
