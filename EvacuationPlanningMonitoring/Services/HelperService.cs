using EvacuationPlanningMonitoring.Services.Interfaces;
using System.ComponentModel.Design;

namespace EvacuationPlanningMonitoring.Services
{
    public class HelperService
    {
        private double Deg2Rad(double deg)
        {
            return deg * (Math.PI / 180);
        }

        public double GetDistanceFromLatLonInKm(double lat1, double lon1, double lat2, double lon2)
        {
            double EarthRadiusKm = 6371;
            double dLat = Deg2Rad(lat2 - lat1);
            double dLon = Deg2Rad(lon2 - lon1);

            double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                       Math.Cos(Deg2Rad(lat1)) * Math.Cos(Deg2Rad(lat2)) *
                       Math.Sin(dLon / 2) * Math.Sin(dLon / 2);

            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            double distance = EarthRadiusKm * c; // Distance in km

            return distance;
        }

        public double GetTimeFromVelocityAndDistancInMinute(double distance, int speed)
        {
            return (distance * 60) / speed;
        }
    }
}
