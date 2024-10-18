namespace EvacuationPlanningMonitoring.Models.DTOs
{
    public class EvacuationStatusDTO : Evacuation
    {
        public int EvacuatedPeople { get; set; }
        public int RemainPeople { get; set; }
        public bool IsEvacuatedComplete { get; set; } = false;
    }
}
