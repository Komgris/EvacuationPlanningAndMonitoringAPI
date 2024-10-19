using EvacuationPlanningMonitoring.Models.DbModels;
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
        private readonly IEvacuationPlanRepository _evacuationPlanRepository;
        public EvacuationService(IEvacuationZoneRepository zoneRepository, 
            IPlanService planService, 
            IVehicleRepository vehicleRepository,
            IEvacuationPlanRepository evacuationPlanRepository) 
        {
            _zoneRepository = zoneRepository;
            _vehicleRepository = vehicleRepository;
            _planService = planService;
            _evacuationPlanRepository= evacuationPlanRepository;
        }
        public async Task Create(EvacuationZoneDTO zoneDto)
        {
            var zone = new EvacuationZoneModel()
            {
                ZoneID = zoneDto.ZoneID,
                RemainPeople = zoneDto.NumberOfPeople,
                NumberOfPeople = zoneDto.NumberOfPeople,
                UrgencyLevel = zoneDto.UrgencyLevel,
                Latitude = zoneDto.LocationCoordinates.Latitude,
                Longitude = zoneDto.LocationCoordinates.Longitude
            };
            await _zoneRepository.Create(zone);
            await GeneratePlan();
        }

        public async Task GeneratePlan()
        {
            var zones = await _zoneRepository.GetNotCompleteZone();
            var vehicles = await _vehicleRepository.GetAvaiableVehicle();
            var plans = _planService.GeneratePlan(vehicles, zones);
            await _evacuationPlanRepository.SavePlan(plans);
        }

        public async Task<List<EvacuationPlanDTO>> GetPlan()
        {
            var plans = await _evacuationPlanRepository.GetPlan();
            var planDtos = new List<EvacuationPlanDTO>();
            foreach (var plan in plans)
            {
                planDtos.Add(new EvacuationPlanDTO()
                {
                    ZoneID = plan.ZoneID,
                    VehicleID = plan.VehicleID,
                    Message = plan.Message,
                    ETA = plan.ETAMin + " minutes",
                    NumberOfPeople = plan.NumberOfPeople,
                });
            }
            return planDtos;
        }
    }
}
