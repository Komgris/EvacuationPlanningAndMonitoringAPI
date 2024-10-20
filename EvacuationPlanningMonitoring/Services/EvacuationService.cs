using EvacuationPlanningMonitoring.Models;
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
        private readonly IEvacuationZoneRepository _evacuationZoneRepository;
        public EvacuationService(IEvacuationZoneRepository zoneRepository, 
            IPlanService planService, 
            IVehicleRepository vehicleRepository,
            IEvacuationPlanRepository evacuationPlanRepository,
            IEvacuationZoneRepository evacuationZoneRepository) 
        {
            _zoneRepository = zoneRepository;
            _vehicleRepository = vehicleRepository;
            _planService = planService;
            _evacuationPlanRepository= evacuationPlanRepository;
            _evacuationZoneRepository= evacuationZoneRepository;
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
            var alreadyPlan = await _evacuationPlanRepository.GetPlanInProgress();
            var plans = _planService.GeneratePlan(vehicles, zones, alreadyPlan);
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

        public async Task<List<EvacuationStatusDTO>> GetStatus()
        {
            var zones = await _evacuationZoneRepository.GetAll();
            var status = new List<EvacuationStatusDTO>();
            foreach (var zone in zones)
            {
                status.Add(new EvacuationStatusDTO()
                {
                    ZoneID= zone.ZoneID,
                    EvacuatedPeople = zone.RemainPeople - zone.NumberOfPeople,
                    RemainPeople= zone.NumberOfPeople,
                    IsEvacuatedComplete = zone.RemainPeople == 0
                });
            }
            return status;
        }

        public async Task UpdateStatus(UpdateEvcuationStatusDto status)
        {
           switch (status.Status)
            {
                case EvacuationPlanStatus.InProgress:
                    //change status plan to inprogress
                    await _evacuationPlanRepository.ChangeStatusPlan(status.ZoneID, status.VehicleID, EvacuationPlanStatus.InProgress);
                    //change status vehicle Assigned
                    await _vehicleRepository.ChangeVehicleStatus(status.VehicleID, VehicleStatus.Assigned);
                    break;
                case EvacuationPlanStatus.Done:
                    //change status plan to done
                    var plan = await _evacuationPlanRepository.ChangeStatusPlan(status.ZoneID, status.VehicleID, EvacuationPlanStatus.Done);
                    //change status vehicle Avaiable
                    await _vehicleRepository.ChangeVehicleStatus(status.VehicleID, VehicleStatus.Available);
                    //remove remain people
                    await _evacuationZoneRepository.EvcuationDone(plan);
                    break;
            }
        }
    }
}
