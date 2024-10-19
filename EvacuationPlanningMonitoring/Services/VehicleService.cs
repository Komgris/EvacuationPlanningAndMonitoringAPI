using EvacuationPlanningMonitoring.Models.DbModels;
using EvacuationPlanningMonitoring.Models.DTOs;
using EvacuationPlanningMonitoring.Repositorys.Interfaces;
using EvacuationPlanningMonitoring.Services.Interfaces;

namespace EvacuationPlanningMonitoring.Services
{
    public class VehicleService : IVehicleService
    {
        private readonly IVehicleRepository _vehicleRepository;
        public VehicleService(IVehicleRepository vehicleRepository)
        {
            _vehicleRepository = vehicleRepository;
        }

        public void Create(VehicleDTO vehicleDTO)
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
            _vehicleRepository.Add(vehicle);
            _vehicleRepository.Save();
        }
    }
}
