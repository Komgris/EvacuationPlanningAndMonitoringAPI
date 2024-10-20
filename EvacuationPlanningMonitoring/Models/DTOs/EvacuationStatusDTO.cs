namespace EvacuationPlanningMonitoring.Models.DTOs
{
    public class EvacuationStatusDTO
    {
        public string ZoneID { get; set; } = "";
        public int EvacuatedPeople { get; set; }
        public int RemainPeople { get; set; }
        public bool IsEvacuatedComplete { get; set; } = false;
        public string Message { get; set; } = "";
    }
}
