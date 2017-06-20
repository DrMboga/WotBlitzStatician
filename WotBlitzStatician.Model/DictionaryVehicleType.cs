namespace WotBlitzStatician.Model
{
    using System.ComponentModel.DataAnnotations;

    public class DictionaryVehicleType
    {
		[Key]
		public string VehicleTypeId { get; set; }

		public string VehicleTypeName { get; set; }

	}
}
