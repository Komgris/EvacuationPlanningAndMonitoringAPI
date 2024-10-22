using EvacuationPlanningMonitoring.Models;
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

        public List<string> IsValidZones(List<EvacuationZoneDTO> zones)
        {
            var errorList = new List<string>();
            var index = 0;
            foreach (var zone in zones)
            {
                errorList.AddRange(IsValidZone(zone, index));
                index++;
            }
            return errorList;
        }

        private List<string> IsValidZone(EvacuationZoneDTO zone, int index)
        {
            var errorList = new List<string>();
            if (zone.NumberOfPeople <= 0)
            {
                errorList.Add(index + " : NumberOfPeople must more than 0");
            }
            if (zone.UrgencyLevel>5 || zone.UrgencyLevel<1)
            {
                errorList.Add(index + " : UrgencyLevel must be between 1 and 5");
            }
            if (string.IsNullOrEmpty(zone.ZoneID.Trim()))
            {
                errorList.Add(index + " : Invalid ZoneID");
            }
            if (IsValidCoordinates(zone.LocationCoordinates.Latitude, zone.LocationCoordinates.Longitude))
            {
                errorList.Add(index + " : Invalid Latitude Or Longitude");
            }
            return errorList;
        }
    }
}
