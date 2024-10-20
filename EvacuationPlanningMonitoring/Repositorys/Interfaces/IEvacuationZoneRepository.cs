using EvacuationPlanningMonitoring.Models.DbModels;

namespace EvacuationPlanningMonitoring.Repositorys.Interfaces
{
    public interface IEvacuationZoneRepository : IBaseRepository<EvacuationZoneModel>
    {
        Task Create(List<EvacuationZoneModel> zone);
        Task<List<EvacuationZoneModel>> GetNotCompleteZone();
        Task<List<EvacuationZoneModel>> GetAll();
        Task EvcuationDone(EvacuationPlanModel plan);
        Task ClearZone();
    }
}
