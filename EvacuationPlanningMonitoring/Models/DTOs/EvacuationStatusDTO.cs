using Microsoft.Extensions.Options;

namespace EvacuationPlanningMonitoring.Models.DTOs
{
    public class EvacuationStatusDTO
    {
        public string ZoneID { get; set; } = "";
        public int TotalEvacuated { get; set; }
        public int EvacuatingPeople { get; set; }
        public int RemainPeople { get; set; }
        public bool IsEvacuatedComplete { get; set; } = false;
        public string Message { get; set; } = "";
        public string LastVehicleUsed { get; set; } = "";
    }
}
