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
    public class UserGroupPrivileges : BaseEntity
    {
        public long UserGroupPrivilegesID { get; set; }

        public long TransactionRightsID { get; set; }

        [MaxLength(50)]
        [NotMapped]
        public string TransactionName { get; set; }

        public long UserGroupID { get; set; }

        [DefaultValue(0)]
        public int TransactionTypeID { get; set; } // 1- inventory, 2 - Distribution, 3 - Manufacturing, 4 - Finance, 5 - HR, 6 - Logistic, 7- CRM

        [DefaultValue(0)]
        public bool IsAccess { get; set; }

        [DefaultValue(0)]
        public bool IsPause { get; set; }

        [DefaultValue(0)]
        public bool IsSave { get; set; }

        [DefaultValue(0)]
        public bool IsModify { get; set; }

        [DefaultValue(0)]
        public bool IsView { get; set; }

        [DefaultValue(0)]
        public bool IsDelete { get; set; }

        public virtual UserGroup UserGroup { get; set; }
        //public virtual ICollection<UserGroupPrivilegesLocations> UserGroupPrivilegesLocations { get; set; }
    }
}
