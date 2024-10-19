using EvacuationPlanningMonitoring.Models.DTOs;

namespace EvacuationPlanningMonitoring.Services.Interfaces
{
    public interface IEvacuationService
    {
        Task<List<EvacuationPlanDTO>> GeneratePlan();
    }
}
