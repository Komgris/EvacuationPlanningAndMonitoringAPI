namespace EvacuationPlanningMonitoring.Models.DTOs
{
    public class EvacuationPlanDTO
    {
        public string ZoneID { get; set; } = "";
        public string VehicleID { get; set; } = "";
        public string ETA { get; set; } = "";
        public int NumberOfPeople { get; set; }
        public string Message { get; set; } = "";
    }
}
