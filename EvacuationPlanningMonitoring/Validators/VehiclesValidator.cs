using EvacuationPlanningMonitoring.Models.DTOs;
using EvacuationPlanningMonitoring.Validators.Interfaces;

namespace EvacuationPlanningMonitoring.Validators
{
    public class VehiclesValidator : BaseValidator, IVehiclesValidator
    {
        public VehiclesValidator() { }
        public List<string> IsValidVehicles(List<VehicleDTO> vehicles)
        {
            var errResponse = new List<string>();
            var index = 0;
            foreach (var vehicle in vehicles)
            {
                errResponse.AddRange(IsValidVehicle(vehicle, index));
                index++;
            }
            return errResponse;
        }

        public List<string> IsValidVehicle(VehicleDTO vehicle, int index)
        {
            var errResponse = new List<string>();
            if (string.IsNullOrEmpty(vehicle.VehicleID.Trim()))
            {
                errResponse.Add(index + " : Invalid VehicleID");
            }
            if (vehicle.Capacity <= 0)
            {
                errResponse.Add(index + " : Capacity must more than 0");
            }
            if (vehicle.Speed <= 0)
            {
                errResponse.Add(index + " : Speed must more than 0");
            }
            if (IsValidCoordinates(vehicle.LocationCoordinates.Latitude, vehicle.LocationCoordinates.Longitude))
            {
                errResponse.Add(index + " : Invalid Latitude Or Longitude");
            }
            return errResponse;
        }
    }
}
