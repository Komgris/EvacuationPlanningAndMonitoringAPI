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
            var sortedZones = evacuationZones.OrderByDescending(x => x.UrgencyLevel).ThenBy(x=>x.ID).ToList();
            var sortedVehicle = vehicles.OrderByDescending(x => x.Capacity).ToList();
            foreach (var zone in sortedZones)
            {
                var timeBetweenZoneVehicle = new List<VehicleDistanctCapcityDto>();
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
                        //timeBetweenZoneVehicle.Add(vehicle.VehicleID, (estTime, vehicle.Capacity));
                        timeBetweenZoneVehicle.Add(new VehicleDistanctCapcityDto()
                        {
                            VehicleID = vehicle.VehicleID,
                            ETA = estTime,
                            Capacity = vehicle.Capacity,
                        });
                    }
                }
                timeBetweenZoneVehicle = timeBetweenZoneVehicle.OrderByDescending(x => x.Capacity).ThenBy(x => x.ETA).ToList();
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

        public List<string> ValidateGeneratePlan(List<EvacuationPlanModel> plans, List<EvacuationZoneModel> evacuationZones, List<EvacuationPlanModel> alreadyPlans)
        {
            var sumAlreadyPlan = alreadyPlans.Sum(x => x.NumberOfPeople);
            var errResult = new  List<string>();
            var sumPlans = plans.Sum(x => x.NumberOfPeople);
            var sumZones = evacuationZones.Sum(x => x.RemainPeople);
            var remainPeopleZone = sumZones - sumAlreadyPlan;
            var tooFarZones = plans.Where(x => x.ETAMin > 60).ToList();
            if (sumPlans != remainPeopleZone)
            {
                errResult.Add("Vehicles with insufficient capacity for larger zones, Add more Vehicles");
            }
            foreach(var tooFarZone in tooFarZones)
            {
                errResult.Add("No available vehicles within a reasonable distance For " + tooFarZone.ZoneID);
            }
            return errResult;
        }

        private List<EvacuationPlanModel> RecursiveAssign(List<VehicleDistanctCapcityDto> timeBetweenZoneVehicle, int people, List<VehicleModel> vehicles, string zoneId)
        {
            var plans = new List<EvacuationPlanModel>();
            if (timeBetweenZoneVehicle.Count > 0)
            {
                var selectedVehicle = new VehicleDistanctCapcityDto();
                var exactCapacityVehicle = timeBetweenZoneVehicle.FirstOrDefault(x => people  >= x.Capacity);
                if (exactCapacityVehicle != null)
                {
                    //IF EXACT CAPACITY DELIVERY FIRST
                    selectedVehicle = exactCapacityVehicle;
                }
                else
                {
                    //IF NOT, JUST NEAREST
                    var nearestVehiclePair = timeBetweenZoneVehicle.OrderBy(x=>x.Capacity).First();
                    selectedVehicle = nearestVehiclePair;
                }
                var nearestVehicle = vehicles.FirstOrDefault(x => x.VehicleID == selectedVehicle.VehicleID);
                if (nearestVehicle != null)
                {
                    var remainPeople = people - nearestVehicle.Capacity;
                    var eta = (int)selectedVehicle.ETA;
                    plans.Add(new EvacuationPlanModel
                    {
                        ZoneID = zoneId,
                        NumberOfPeople = remainPeople > 0 ? nearestVehicle.Capacity : people,
                        VehicleID = selectedVehicle.VehicleID,
                        ETAMin = eta,
                    });
                    timeBetweenZoneVehicle.Remove(selectedVehicle);
                    if (remainPeople > 0)
                    {
                        var recursivePlans = RecursiveAssign(timeBetweenZoneVehicle, remainPeople, vehicles, zoneId);
                        plans.AddRange(recursivePlans);
                    }
                }
            }
            return plans;
        }


    }
}
