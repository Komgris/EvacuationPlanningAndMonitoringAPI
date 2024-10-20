

using EvacuationPlanningMonitoring.Models;
using EvacuationPlanningMonitoring.Models.DbModels;
using EvacuationPlanningMonitoring.Models.DTOs;
using EvacuationPlanningMonitoring.Services.Interfaces;
using Moq;
using System.Numerics;
using Xunit;

namespace EvacuationPlanningMonitoring.Services.Tests
{
    public class PlanServiceTests
    {
        [Fact]
        public void EvacuationPlan()
        {
            var zones = GenerateZone(3);
            var vehicles = GenerateVehicles(4);
            var planned = GenerateOldPlan(1);
            var planService = new PlanService();
            var plans = planService.GeneratePlan(vehicles, zones, planned);
            var sumCapacity = vehicles.Sum(x => x.Capacity);
            var planPeople = plans.Sum(x => x.NumberOfPeople);
            Assert.True(planPeople <= sumCapacity);
            Assert.True(plans.Count > 0);
        }

        private List<EvacuationPlanModel> GenerateOldPlan(int amount)
        {
            var plans = new List<EvacuationPlanModel>();
            for (int i = 0; i < amount; i++)
            {
                plans.Add(new EvacuationPlanModel
                {
                    ZoneID = "Z" + i,
                    VehicleID = "V" + i,
                    NumberOfPeople = 5,
                    Status = EvacuationPlanStatus.InProgress
                });
            }
            return plans;
        }

        private List<VehicleModel> GenerateVehicles(int amount)
        {
            var vehicles = new List<VehicleModel>();
            var rnd = new Random();
            var locationCoordinate = new List<LocationCoordinatesDTO>()
            {
                new LocationCoordinatesDTO() { Latitude = 13.7516046, Longitude = 100.6751537},
                new LocationCoordinatesDTO() { Latitude = 13.5769205, Longitude = 100.6522948},
                new LocationCoordinatesDTO() { Latitude = 13.5831999, Longitude = 100.6339376},
                new LocationCoordinatesDTO() { Latitude = 13.6429856, Longitude = 100.5946448},
            };
            for (int i = 0; i < amount; i++)
            {
                vehicles.Add(new VehicleModel()
                {
                    ID = i,
                    Latitude = locationCoordinate[i].Latitude,
                    Longitude = locationCoordinate[i].Longitude,
                    Capacity = rnd.Next(10, 30),
                    Type = "bus",
                    Speed = 60,
                    VehicleID = "V" + i
                });
            }
            return vehicles;
        }

        private List<EvacuationZoneModel> GenerateZone(int amount)
        {
            var zones = new List<EvacuationZoneModel>();
            var rnd = new Random();
            var locationCoordinate = new List<LocationCoordinatesDTO>()
            {
                new LocationCoordinatesDTO() { Latitude = 13.7496399, Longitude = 100.5818955},
                new LocationCoordinatesDTO() { Latitude = 13.7491743, Longitude = 100.5818062},
                new LocationCoordinatesDTO() { Latitude = 13.7491156, Longitude = 100.5823113},
                new LocationCoordinatesDTO() { Latitude = 13.7497594, Longitude = 100.5755525},
                new LocationCoordinatesDTO() { Latitude = 13.7473681, Longitude = 100.5677069},
                new LocationCoordinatesDTO() { Latitude = 13.7499582, Longitude = 100.5625157},
            };
            for (int i = 0; i < amount; i++)
            {
                zones.Add(new EvacuationZoneModel()
                {
                    ID = i,
                    Latitude = locationCoordinate[i].Latitude,
                    Longitude = locationCoordinate[i].Longitude,
                    NumberOfPeople = rnd.Next(10, 100),
                    UrgencyLevel = rnd.Next(1, 5),
                    ZoneID = "Z" + i,
                });
            }
            return zones;
        }
    }
}