using EvacuationPlanningMonitoring.Models;
using EvacuationPlanningMonitoring.Models.DbModels;
using EvacuationPlanningMonitoring.Models.DTOs;
using EvacuationPlanningMonitoring.Repositorys.Interfaces;
using EvacuationPlanningMonitoring.Services.Interfaces;
using System.Text.Json;

namespace EvacuationPlanningMonitoring.Services
{
    public class VehicleService : IVehicleService
    {
        private readonly IVehicleRepository _vehicleRepository;
        private readonly ILoggingRepository _loggingRepository;
        public VehicleService(IVehicleRepository vehicleRepository, ILoggingRepository loggingRepository)
        {
            _vehicleRepository = vehicleRepository;
            _loggingRepository = loggingRepository;
        }

        public async Task Create(List<VehicleDTO> vehicleDTOs)
        {
            var vehicles = new List<VehicleModel>();
            foreach (var vehicleDTO in vehicleDTOs)
            {
                var vehicle = new VehicleModel()
                {
                    VehicleID = vehicleDTO.VehicleID,
                    Speed = vehicleDTO.Speed,
                    Capacity = vehicleDTO.Capacity,
                    Type = vehicleDTO.Type,
                    Latitude = vehicleDTO.LocationCoordinates.Latitude,
                    Longitude = vehicleDTO.LocationCoordinates.Longitude
                };
                vehicles.Add(vehicle);
            }
            await _vehicleRepository.Create(vehicles);
            //LOGGING
            var vehicleIDs= vehicles.Select(x => x.VehicleID).ToList();
            await _loggingRepository.CreateLog(ActionStatus.CreateVehicle, JsonSerializer.Serialize(vehicles), String.Join(", ", vehicleIDs),"");
        }
    }
}
