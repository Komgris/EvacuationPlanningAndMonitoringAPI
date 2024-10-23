using EvacuationPlanningMonitoring.Models.DbModels;
using EvacuationPlanningMonitoring.Models.DTOs;
using EvacuationPlanningMonitoring.Repositorys.Interfaces;
using EvacuationPlanningMonitoring.Validators.Interfaces;

namespace EvacuationPlanningMonitoring.Validators
{
    public class VehiclesValidator : BaseValidator, IVehiclesValidator
    {
        private readonly IVehicleRepository _vehicleRepository;
        public VehiclesValidator(IVehicleRepository vehicleRepository) 
        { 
            _vehicleRepository = vehicleRepository;
        }
        public async Task<List<string>> IsValidVehicles(List<VehicleDTO> vehicleDtos)
        {
            var vehicles = await _vehicleRepository.GetAll();

            var errResponse = new List<string>();
            var index = 0;
            foreach (var vehicleDto in vehicleDtos)
            {
                errResponse.AddRange(IsValidVehicle(vehicleDto, vehicles, index));
                index++;
            }
            return errResponse;
        }

        public List<string> IsValidVehicle(VehicleDTO vehicleDto, List<VehicleModel> vehicles, int index)
        {
            var errResponse = new List<string>();
            var vehicleDuplicate = vehicles.Any(x => x.VehicleID == vehicleDto.VehicleID);
            if (string.IsNullOrEmpty(vehicleDto.VehicleID.Trim()))
            {
                errResponse.Add("Invalid VehicleID");
                return errResponse;
            }
            if (vehicleDuplicate)
            {
                errResponse.Add(vehicleDto.VehicleID + " : ZoneID is Duplicate");
            }
            if (vehicleDto.Capacity <= 0)
            {
                errResponse.Add(vehicleDto.VehicleID + " : Capacity must more than 0");
            }
            if (vehicleDto.Speed <= 0)
            {
                errResponse.Add(vehicleDto.VehicleID + " : Speed must more than 0");
            }
            if (!IsValidCoordinates(vehicleDto.LocationCoordinates.Latitude, vehicleDto.LocationCoordinates.Longitude))
            {
                errResponse.Add(vehicleDto.VehicleID + " : Invalid Latitude Or Longitude");
            }
            return errResponse;
        }
    }
}
