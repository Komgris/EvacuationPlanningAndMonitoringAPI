using EvacuationPlanningMonitoring.Models.DbModels;
using EvacuationPlanningMonitoring.Models.DTOs;
using EvacuationPlanningMonitoring.Services.Interfaces;

namespace EvacuationPlanningMonitoring.Services
{
    public class PlanService : IPlanService
    {
        public List<EvacuationPlanModel> GeneratePlan(List<VehicleModel> vehicles, List<EvacuationZoneModel> evacuationZones, List<EvacuationPlanModel> alreadyPlan)
        {
            var helperService = new HelperService();
            var plans = new List<EvacuationPlanModel>();
            var sortedZones = evacuationZones.OrderByDescending(x => x.UrgencyLevel).ToList();
            var sortedVehicle = vehicles.OrderByDescending(x => x.Capacity).ToList();
            foreach (var zone in sortedZones)
            {
                var timeBetweenZoneVehicle = new Dictionary<string, double>();
                var inprogressPeople = alreadyPlan.Where(x => x.ZoneID == zone.ZoneID).Sum(x => x.NumberOfPeople);
                //not include plan that inprogress
                var unplanPeople = zone.RemainPeople - inprogressPeople;
                foreach (var vehicle in sortedVehicle)
                {
                    if (unplanPeople > 0 && vehicle.Capacity > 0)
                    {
                        // Store Distance between Zone And Vehicle
                        var distance = helperService.GetDistanceFromLatLonInKm(zone.Latitude, zone.Longitude, vehicle.Latitude, vehicle.Longitude);
                        var estTime = helperService.GetTimeFromVelocityAndDistancInMinute(distance, vehicle.Speed);
                        timeBetweenZoneVehicle.Add(vehicle.VehicleID, estTime);
                    }
                }
                var recursivePlans = RecursiveAssign(timeBetweenZoneVehicle, unplanPeople, vehicles, zone.ZoneID);
                if (recursivePlans.Count > 0)
                {
                    var usedVehicle = recursivePlans.Select(plan => plan.VehicleID).ToList();
                    sortedVehicle.RemoveAll(x => usedVehicle.Contains(x.VehicleID));
                }
                plans.AddRange(recursivePlans);
            }
            return plans;
        }

        private List<EvacuationPlanModel> RecursiveAssign(Dictionary<string, double> timeBetweenZoneVehicle, int people, List<VehicleModel> vehicles, string zoneId)
        {
            var plans = new List<EvacuationPlanModel>();
            if (timeBetweenZoneVehicle.Count > 0)
            {
                var nearestVehiclePair = timeBetweenZoneVehicle.OrderBy(x => x.Value).First();
                var nearestVehicle = vehicles.FirstOrDefault(x => x.VehicleID == nearestVehiclePair.Key);
                if (nearestVehicle != null)
                {
                    var remainPeople = people - nearestVehicle.Capacity;
                    var eta = (int)nearestVehiclePair.Value;
                    if (eta < 50)
                    {
                        plans.Add(new EvacuationPlanModel
                        {
                            ZoneID = zoneId,
                            NumberOfPeople = remainPeople > 0 ? nearestVehicle.Capacity : people,
                            VehicleID = nearestVehiclePair.Key,
                            ETAMin = eta,
                        });
                        timeBetweenZoneVehicle.Remove(nearestVehiclePair.Key);
                        if (remainPeople > 0)
                        {
                            var recursivePlans = RecursiveAssign(timeBetweenZoneVehicle, remainPeople, vehicles, zoneId);
                            plans.AddRange(recursivePlans);
                        }
                    }
                }
            }
            return plans;
        }


    }
}
