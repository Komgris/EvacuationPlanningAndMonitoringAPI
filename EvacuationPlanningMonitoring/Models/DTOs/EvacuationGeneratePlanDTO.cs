using EvacuationPlanningMonitoring.Models.DbModels;

namespace EvacuationPlanningMonitoring.Models.DTOs
{
    public class EvacuationGeneratePlanDTO
    {
        public List<string> ErrorList { get; set; }
        public List<EvacuationPlanDTO> InCompletePlan { get; set; }
        public EvacuationGeneratePlanDTO()
        {
            ErrorList = new List<string>();
            InCompletePlan= new List<EvacuationPlanDTO>();
        }
    }
}
