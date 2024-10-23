using EvacuationPlanningMonitoring.Models;
using EvacuationPlanningMonitoring.Models.DbModels;
using EvacuationPlanningMonitoring.Models.DTOs;
using EvacuationPlanningMonitoring.Repositorys;
using EvacuationPlanningMonitoring.Repositorys.Interfaces;
using EvacuationPlanningMonitoring.Services.Interfaces;
using EvacuationPlanningMonitoring.Validators.Interfaces;
using System.Numerics;
using System.Text.Json;

namespace EvacuationPlanningMonitoring.Services
{
    public class EvacuationService : IEvacuationService
    {
        private readonly IEvacuationZoneRepository _zoneRepository;
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IPlanService _planService;
        private readonly IEvacuationPlanRepository _evacuationPlanRepository;
        private readonly IEvacuationZoneRepository _evacuationZoneRepository;
        private readonly IRedisService _redisService;
        private readonly ILoggingRepository _loggingRepository;
        public EvacuationService(IEvacuationZoneRepository zoneRepository,
            IPlanService planService,
            IVehicleRepository vehicleRepository,
            IEvacuationPlanRepository evacuationPlanRepository,
            IEvacuationZoneRepository evacuationZoneRepository,
            IRedisService redisService,
            ILoggingRepository loggingRepository)
        {
            _zoneRepository = zoneRepository;
            _vehicleRepository = vehicleRepository;
            _planService = planService;
            _evacuationPlanRepository = evacuationPlanRepository;
            _evacuationZoneRepository = evacuationZoneRepository;
            _redisService = redisService;
            _loggingRepository = loggingRepository;
        }
        public async Task Create(List<EvacuationZoneDTO> zoneDtos)
        {
            var zones = new List<EvacuationZoneModel>();
            foreach (var zoneDto in zoneDtos)
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
                zones.Add(zone);
            }
            await _zoneRepository.Create(zones);
            await SetStatusCache();

            var zoneIDs = zones.Select(x => x.ZoneID).ToList();
            await _loggingRepository.CreateLog(ActionStatus.CreateZone, JsonSerializer.Serialize(zones), String.Join(", ", zoneIDs), string.Empty);

        }

        public async Task<List<string>> GeneratePlan()
        {
            var zones = await _zoneRepository.GetNotCompleteZone();
            var vehicles = await _vehicleRepository.GetAvaiableVehicle();
            var alreadyPlan = await _evacuationPlanRepository.GetPlanInProgress();
            var plans = _planService.GeneratePlan(vehicles, zones, alreadyPlan);
            var validatePlans = _planService.ValidateGeneratePlan(plans, zones, alreadyPlan);
            if (validatePlans.Count == 0)
            {
                await _evacuationPlanRepository.SavePlan(plans);
                await _loggingRepository.CreateLog(ActionStatus.GeneratePlan, JsonSerializer.Serialize(plans), string.Empty, string.Empty);
            }
            else
            {
                await _loggingRepository.CreateLog(ActionStatus.InCompletePlan, JsonSerializer.Serialize(plans), string.Empty, string.Empty);
            }
            return validatePlans;
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

        public async Task<List<EvacuationStatusDTO>> GetStatusCache()
        {
            var statusCahce = await _redisService.GetAsync<List<EvacuationStatusDTO>>("status");
            if (statusCahce != null && statusCahce.Count > 0)
            {
                return statusCahce;
            }
            else
            {
                var status = await GetStatus();
                await _redisService.SetAsync<List<EvacuationStatusDTO>>("status", status, TimeSpan.FromHours(1));
                return status;
            }
        }

        public async Task<List<EvacuationStatusDTO>> GetStatus()
        {
            var zones = await _evacuationZoneRepository.GetAll();
            var plans = await _evacuationPlanRepository.GetPlanInProgress();
            var status = new List<EvacuationStatusDTO>();
            foreach (var zone in zones)
            {
                var inprogressPeople = plans.Where(x => x.ZoneID == zone.ZoneID).Sum(x => x.NumberOfPeople);
                status.Add(new EvacuationStatusDTO()
                {
                    ZoneID = zone.ZoneID,
                    TotalEvacuated = zone.NumberOfPeople - zone.RemainPeople,
                    EvacuatingPeople = inprogressPeople,
                    RemainPeople = zone.RemainPeople,
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
            await SetStatusCache();
            await _loggingRepository.CreateLog(ActionStatus.UpdateStatus, JsonSerializer.Serialize(status), status.VehicleID, status.ZoneID);
        }

        public async Task Clear()
        {
            //clear plan
            await _evacuationPlanRepository.ClearPlan();
            //clear vehicle
            await _vehicleRepository.ClearVehicle();
            //clear zone
            await _evacuationZoneRepository.ClearZone();
            //clear redis
            await _redisService.RemoveAsync("status");
            await _loggingRepository.CreateLog(ActionStatus.CreateZone, JsonSerializer.Serialize(new object { }), string.Empty, string.Empty);
        }

        private async Task SetStatusCache()
        {
            var statusList = await GetStatus();
            await _redisService.SetAsync<List<EvacuationStatusDTO>>("status", statusList, TimeSpan.FromHours(1));
        }
    }
}
