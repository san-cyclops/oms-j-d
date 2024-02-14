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
    public class TransactionRights : BaseEntity
    {
        public long TransactionRightsID { get; set; }

        public int DocumentID { get; set; }

        [Required()]
        [MaxLength(15)]
        public string TransactionCode { get; set; }

        [Required()]
        [MaxLength(50)]
        public string TransactionName { get; set; }

        [DefaultValue(0)]
        /*
         * 1 - Common
         * 2 - Invenoty
         * 3 - Logistic
         * 4 - CRM
         * 5 - Accounts
         * 6 - Gift Voucher
         * 7 - POS  
         * 8 - Reports  
         * Etc.
         */
        public int TransactionTypeID { get; set; } 

        [NotMapped]
        public long UserGroupID { get; set; }

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

        //public virtual UserGroup UserGroup { get; set; }
        //public virtual ICollection<UserGroupPrivileges> UserGroupPrivileges { get; set; }
    }
}
