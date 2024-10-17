using EvacuationPlanningMonitoring.Models.DbModels;
using EvacuationPlanningMonitoring.Models.DTOs;
using EvacuationPlanningMonitoring.Services.Interfaces;

namespace EvacuationPlanningMonitoring.Services
{
    public class PlanService : IPlanService
    {
        public List<EvacuationPlanDTO> GeneratePlan(List<VehicleModel> vehicles, List<EvacuationZoneModel> evacuationZones)
        {
            var helperService = new HelperService();
            var plans = new List<EvacuationPlanDTO>();
            var sortedZones = evacuationZones.OrderByDescending(x => x.UrgencyLevel).ToList();
            var sortedVehicle = vehicles.OrderByDescending(x => x.Capacity).ToList();
            foreach (var zone in sortedZones)
            {
                var timeBetweenZoneVehicle = new Dictionary<string, double>();
                foreach (var vehicle in sortedVehicle)
                {
                    if (zone.NumberOfPeople > 0 && vehicle.Capacity > 0)
                    {
                        // Store Distance between Zone And Vehicle
                        var distance = helperService.GetDistanceFromLatLonInKm(zone.Latitude, zone.Longitude, vehicle.Latitude, vehicle.Longitude);
                        var estTime = helperService.GetTimeFromVelocityAndDistancInMinute(distance, vehicle.Speed);
                        timeBetweenZoneVehicle.Add(vehicle.VehicleID, estTime);
                    }
                }
                var recursivePlans = RecursiveAssign(timeBetweenZoneVehicle, zone.NumberOfPeople, vehicles, zone.ZoneID);
                if (recursivePlans.Count > 0)
                {
                    var usedVehicle = recursivePlans.Select(plan => plan.VehicleID).ToList();
                    sortedVehicle.RemoveAll(x => usedVehicle.Contains(x.VehicleID));
                }
                plans.AddRange(recursivePlans);
            }
            return plans;
        }

        private List<EvacuationPlanDTO> RecursiveAssign(Dictionary<string, double> timeBetweenZoneVehicle, int people, List<VehicleModel> vehicles, string zoneId)
        {
            var plans = new List<EvacuationPlanDTO>();
            if (timeBetweenZoneVehicle.Count > 0)
            {
                var nearestVehiclePair = timeBetweenZoneVehicle.OrderBy(x => x.Value).First();
                var nearestVehicle = vehicles.FirstOrDefault(x => x.VehicleID == nearestVehiclePair.Key);
                var remainPeople = people - nearestVehicle.Capacity;
                var eta = (int)nearestVehiclePair.Value;
                plans.Add(new EvacuationPlanDTO
                {
                    ZoneID = zoneId,
                    NumberOfPeople = remainPeople > 0 ? remainPeople : nearestVehicle.Capacity,
                    VehicleID = nearestVehiclePair.Key,
                    ETA = eta + " minutes"
                });
                timeBetweenZoneVehicle.Remove(nearestVehiclePair.Key);
                if (remainPeople > 0)
                {
                    var recursivePlans = RecursiveAssign(timeBetweenZoneVehicle, remainPeople, vehicles, zoneId);
                    plans.AddRange(recursivePlans);
                }
            }
            return plans;
        }
    }
}
