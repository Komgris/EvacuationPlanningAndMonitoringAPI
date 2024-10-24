namespace EvacuationPlanningMonitoring.Models.DTOs
{
    public class VehicleDTO
    {
        public string VehicleID { get; set; } = string.Empty;
        public int Capacity { get; set;}
        public string Type { get; set; } = string.Empty;
        public LocationCoordinatesDTO LocationCoordinates { get; set; }
        public int Speed { get; set; }
        public string? Status { get; set; }
    }
}
