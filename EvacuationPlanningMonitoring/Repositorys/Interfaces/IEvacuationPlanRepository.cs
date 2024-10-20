using EvacuationPlanningMonitoring.Models.DbModels;

namespace EvacuationPlanningMonitoring.Repositorys.Interfaces
{
    public interface IEvacuationPlanRepository : IBaseRepository<EvacuationPlanModel>
    {
        Task SavePlan(List<EvacuationPlanModel> plans);
        Task<List<EvacuationPlanModel>> GetPlan();
        Task<List<EvacuationPlanModel>> GetPlanInProgress();
        Task<EvacuationPlanModel> ChangeStatusPlan(string zoneId, string vehicle, string status);
    }
}
