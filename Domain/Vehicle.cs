using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Vehicle : BaseEntity
    {
        public long VehicleID { get; set; }

        [DefaultValue("")]
        [MaxLength(50)]
        [Required]
	    public string RegistrationNo { get; set; }

        [DefaultValue("")]
        [MaxLength(50)]
        [Required]
        public string VehicleName { get; set; } 

        [DefaultValue("")]
        [MaxLength(50)]
	    public string EngineNo { get; set; }
        
        [DefaultValue("")]
        [MaxLength(50)]
	    public string ChassesNo { get; set; }

        [DefaultValue("")]
        [MaxLength(50)]
	    public string VehicleType { get; set; }

        [DefaultValue("")]
        [MaxLength(25)]
        [Required]
	    public string FuelType { get; set; }

        [DefaultValue("")]
        [MaxLength(50)]
	    public string Make { get; set; }

        [DefaultValue("")]
        [MaxLength(50)]
	    public string Model { get; set; }

        [DefaultValue("")]
	    public string EngineCapacity { get; set; }

        [DefaultValue("")]
	    public string SeatingCapacity { get; set; }

        [DefaultValue(0)]
	    public int Weight { get; set; }

        [DefaultValue("")]
        [MaxLength(150)]
	    public string Remark { get; set; }

        [DefaultValue(0)]
	    public bool IsDelete { get; set; }
    }
}
