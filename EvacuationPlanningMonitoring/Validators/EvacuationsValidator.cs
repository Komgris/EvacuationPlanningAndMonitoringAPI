using EvacuationPlanningMonitoring.Models;
using EvacuationPlanningMonitoring.Models.DbModels;
using EvacuationPlanningMonitoring.Models.DTOs;
using EvacuationPlanningMonitoring.Repositorys.Interfaces;
using EvacuationPlanningMonitoring.Validators.Interfaces;

namespace EvacuationPlanningMonitoring.Validators
{
    public class EvacuationsValidator : BaseValidator,IEvacuationsValidator
    {
        private readonly IEvacuationZoneRepository _zoneRepository;
        private readonly IVehicleRepository _vehicleRepository;
        public EvacuationsValidator(IEvacuationZoneRepository zoneRepository, IVehicleRepository vehicleRepository) 
        {
            _zoneRepository= zoneRepository;
            _vehicleRepository= vehicleRepository;
        }

        public async Task<List<string>> IsValidUpdateStatus(UpdateEvcuationStatusDto status)
        {
            var errorList = new List<string>();
            if (string.IsNullOrEmpty(status.ZoneID))
            {
                errorList.Add("ZoneID Must be Provide");
            }
            if (string.IsNullOrEmpty(status.VehicleID))
            {
                errorList.Add("VehicleID Must be Provide");
            }
            if (errorList.Count > 0)
            {
                return errorList;
            }
            else
            {
                var statusList = new List<string>()
                {
                    EvacuationPlanStatus.InProgress,
                    EvacuationPlanStatus.Ready,
                    EvacuationPlanStatus.Done
                };
                var zone = await _zoneRepository.FindFirstOrDefaultAsync(x => x.ZoneID == status.ZoneID);
                var vehicle = await _vehicleRepository.FindFirstOrDefaultAsync(x => x.VehicleID == status.VehicleID);
                if (zone == null)
                {
                    errorList.Add("ZoneID Not Found");
                }
                if (vehicle == null)
                {
                    errorList.Add("VehicleID Not Found");
                }
                if (!statusList.Contains(status.Status))
                {
                    errorList.Add("Status Must be " + String.Join(", ", statusList));
                }
            }
            return errorList;
        }

        public async Task<List<string>> IsValidZones(List<EvacuationZoneDTO> zoneDtos)
        {
            var zoneIds = zoneDtos.Select(x => x.ZoneID).ToList();
            var zones = await _zoneRepository.GetAll();
            var errorList = new List<string>();
            var index = 0;
            foreach (var zoneDto in zoneDtos)
            {
                errorList.AddRange(IsValidZone(zoneDto, zones, index));
                index++;
            }
            return errorList;
        }

        private List<string> IsValidZone(EvacuationZoneDTO zoneDto, List<EvacuationZoneModel> zones, int index)
        {
            var errorList = new List<string>();
            var zoneDuplicate = zones.Any(x => x.ZoneID == zoneDto.ZoneID);
            if (string.IsNullOrEmpty(zoneDto.ZoneID.Trim()))
            {
                errorList.Add("Invalid ZoneID");
                return errorList;
            }
            if (zoneDuplicate)
            {
                errorList.Add(zoneDto.ZoneID + " : ZoneID is Duplicate");
            }
            if (zoneDto.NumberOfPeople <= 0)
            {
                errorList.Add(zoneDto.ZoneID + " : NumberOfPeople must more than 0");
            }
            if (zoneDto.UrgencyLevel>5 || zoneDto.UrgencyLevel<1)
            {
                errorList.Add(zoneDto.ZoneID + " : UrgencyLevel must be between 1 and 5");
            }
            if (!IsValidCoordinates(zoneDto.LocationCoordinates.Latitude, zoneDto.LocationCoordinates.Longitude))
            {
                errorList.Add(zoneDto.ZoneID + " : Invalid Latitude Or Longitude");
            }
            return errorList;
        }
    }
}
