using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class GroupOfCompany : BaseEntity
    {
        [DefaultValue(0)]
	    public string GroupOfCompanyCode { get; set; } 

        [DefaultValue("")]
        public string GroupOfCompanyName { get; set; }

        [DefaultValue(0)]
        public bool IsInventory { get; set; }

        [DefaultValue(0)]
        public bool IsPos { get; set; }

        [DefaultValue(0)]
        public bool IsManufacture { get; set; }

        [DefaultValue(0)]
        public bool IsHirePurchase { get; set; }

        [DefaultValue(0)]
        public bool IsCrm { get; set; }

        [DefaultValue(0)]
        public bool IsGiftVoucher { get; set; }

        [DefaultValue(0)]
        public bool IsGeneralLedger { get; set; }        

        [DefaultValue(0)]
        public bool IsLogistic { get; set; }        

        [DefaultValue(0)]
        public bool IsHrManagement { get; set; }     

        [DefaultValue(0)]
        public bool IsHospitalManagement { get; set; }

        [DefaultValue(0)]
        public bool IsHotelManagement { get; set; }        

        [DefaultValue(0)]
        public bool IsActive { get; set; }

        [DefaultValue(0)]
        public bool IsDelete { get; set; }

        public virtual ICollection<Company> Companies { get; set; }


    }
}
