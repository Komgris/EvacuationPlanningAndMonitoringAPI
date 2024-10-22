using EvacuationPlanningMonitoring.Models;
using EvacuationPlanningMonitoring.Models.DTOs;
using EvacuationPlanningMonitoring.Repositorys.Interfaces;
using EvacuationPlanningMonitoring.Validators.Interfaces;

namespace EvacuationPlanningMonitoring.Validators
{
    public class EvacuationsValidator : IEvacuationsValidator
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
            var errorStr = string.Empty;
            if (string.IsNullOrEmpty(status.ZoneID))
            {
                errorStr += "ZoneID Must be Provide" + Environment.NewLine;
            }
            if (string.IsNullOrEmpty(status.VehicleID))
            {
                errorStr += "VehicleID Must be Provide" + Environment.NewLine;
            }
            if (!string.IsNullOrEmpty(errorStr))
            {
                return errorStr;
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
                    errorStr += "ZoneID Not Found" + Environment.NewLine;
                }
                if (vehicle == null)
                {
                    errorStr += "VehicleID Not Found" + Environment.NewLine;
                }
                if (!statusList.Contains(status.Status))
                {
                    errorStr += "Status Must be" + String.Join(", ", statusList) + Environment.NewLine;
                }
            }
            return errorStr;
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

        public bool IsValidCoordinates(double latitude, double longitude)
        {
            return (latitude >= -90 && latitude <= 90) && (longitude >= -180 && longitude <= 180);
        }
    }
}
