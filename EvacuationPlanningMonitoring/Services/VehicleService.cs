using EvacuationPlanningMonitoring.Models.DbModels;
using EvacuationPlanningMonitoring.Models.DTOs;
using EvacuationPlanningMonitoring.Repositorys.Interfaces;
using EvacuationPlanningMonitoring.Services.Interfaces;

namespace EvacuationPlanningMonitoring.Services
{
    public class VehicleService : IVehicleService
    {
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IEvacuationService _evacuationService;
        public VehicleService(IVehicleRepository vehicleRepository, IEvacuationService evacuationService)
        {
            _vehicleRepository = vehicleRepository;
            _evacuationService= evacuationService;
        }

        public async Task Create(VehicleDTO vehicleDTO)
        {
            var vehicle = new VehicleModel()
            {
                VehicleID= vehicleDTO.VehicleID,
                Speed =vehicleDTO.Speed,
                Capacity= vehicleDTO.Capacity,
                Type=vehicleDTO.Type,
                Latitude = vehicleDTO.LocationCoordinates.Latitude,
                Longitude = vehicleDTO.LocationCoordinates.Longitude
            };
            await _vehicleRepository.Create(vehicle);
            await _evacuationService.GeneratePlan();
        }
    }
}
