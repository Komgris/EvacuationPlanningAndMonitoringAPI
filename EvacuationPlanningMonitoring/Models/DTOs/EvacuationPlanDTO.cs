namespace EvacuationPlanningMonitoring.Models.DTOs
{
    public class EvacuationPlanDTO : Evacuation
    {
        public string ETA { get; set; } = "";
        public int NumberOfPeople { get; set; }
    }
}
