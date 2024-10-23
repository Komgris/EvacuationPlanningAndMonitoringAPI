namespace EvacuationPlanningMonitoring.Models.DTOs
{
    public class VehicleDistanctCapcityDto
    {
        public string VehicleID { get; set; } = string.Empty;
        public double ETA { get; set;}
        public int Capacity { get; set; }
    }
}
