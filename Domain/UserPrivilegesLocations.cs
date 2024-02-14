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
    public class UserPrivilegesLocations : BaseEntity
    {
        public long UserPrivilegesLocationsID { get; set; }

        public long UserMasterID { get; set; }        

        [DefaultValue(0)]
        public long UserGroupID { get; set; }

        [DefaultValue(0)]
        public int LocationID { get; set; }

        [DefaultValue(0)]
        public bool IsSelect { get; set; }

        [MaxLength(50)]
        [NotMapped]
        public string LocationName { get; set; }

        //public virtual UserPrivileges UserPrivileges { get; set; }
        public virtual UserMaster UserName { get; set; }
    }
}
