using EvacuationPlanningMonitoring.Models.DbModels;
using EvacuationPlanningMonitoring.Models.DTOs;

namespace EvacuationPlanningMonitoring.Services.Interfaces
{
    public interface IEvacuationService
    {
        Task GeneratePlan();
        Task<List<EvacuationPlanDTO>> GetPlan();
    }
}
