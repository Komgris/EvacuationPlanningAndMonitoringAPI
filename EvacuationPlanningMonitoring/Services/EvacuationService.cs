using EvacuationPlanningMonitoring.Models.DTOs;
using EvacuationPlanningMonitoring.Repositorys.Interfaces;
using EvacuationPlanningMonitoring.Services.Interfaces;

namespace EvacuationPlanningMonitoring.Services
{
    public class EvacuationService : IEvacuationService
    {
        private readonly IEvacuationZoneRepository _zoneRepository;
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IPlanService _planService;
        public EvacuationService(IEvacuationZoneRepository zoneRepository, IPlanService planService, IVehicleRepository vehicleRepository) 
        {
            _zoneRepository = zoneRepository;
            _vehicleRepository = vehicleRepository;
            _planService = planService;
        }
        public async Task<List<EvacuationPlanDTO>> GeneratePlan()
        {
            var zones = await _zoneRepository.GetNotCompleteZone();
            var vehicles = await _vehicleRepository.GetAvaiableVehicle();
            var plans = _planService.GeneratePlan(vehicles, zones);
            throw new NotImplementedException();
        }
    }
}
