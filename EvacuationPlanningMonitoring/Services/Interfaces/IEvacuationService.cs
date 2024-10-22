using EvacuationPlanningMonitoring.Models.DbModels;
using EvacuationPlanningMonitoring.Models.DTOs;

namespace EvacuationPlanningMonitoring.Services.Interfaces
{
    public interface IEvacuationService
    {
        Task GeneratePlan();
        Task Create(List<EvacuationZoneDTO> zoneDto);
        Task<List<EvacuationPlanDTO>> GetPlan();
        Task<List<EvacuationStatusDTO>> GetStatus();
        Task<List<EvacuationStatusDTO>> GetStatusCache();
        Task UpdateStatus(UpdateEvcuationStatusDto status);
        Task Clear();
    }
}
