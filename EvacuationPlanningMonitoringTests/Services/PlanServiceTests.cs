

using EvacuationPlanningMonitoring.Models.DbModels;
using EvacuationPlanningMonitoring.Models.DTOs;
using EvacuationPlanningMonitoring.Services.Interfaces;
using Moq;
using Xunit;

namespace EvacuationPlanningMonitoring.Services.Tests
{
    public class PlanServiceTests
    {
        [Fact]
        public void EvacuationPlan()
        {
            var zones = GenerateZone(5);
            var vehicles = GenerateVehicles(3);
            var planService = new PlanService();
            var plans = planService.GeneratePlan(vehicles, zones);
            var sumCapacity = vehicles.Sum(x => x.Capacity);
            var planPeople = plans.Sum(x => x.NumberOfPeople);
            Assert.True(planPeople <= sumCapacity);
            Assert.True(plans.Count > 0);
        }

        private List<VehicleModel> GenerateVehicles(int amount)
        {
            var vehicles = new List<VehicleModel>();
            var rnd = new Random();
            var locationCoordinate = new List<LocationCoordinatesDTO>()
            {
                new LocationCoordinatesDTO() { Latitude = 13.7501555, Longitude = 100.5631574},
                new LocationCoordinatesDTO() { Latitude = 13.7497441, Longitude = 100.5649323},
                new LocationCoordinatesDTO() { Latitude = 13.7521694, Longitude = 100.5646969},
                new LocationCoordinatesDTO() { Latitude = 13.7371752, Longitude = 100.5617332},
            };
            for (int i = 1; i <= amount; i++)
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
            for (int i = 1; i <= amount; i++)
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