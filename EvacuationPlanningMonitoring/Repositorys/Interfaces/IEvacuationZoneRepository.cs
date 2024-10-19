using EvacuationPlanningMonitoring.Models.DbModels;

namespace EvacuationPlanningMonitoring.Repositorys.Interfaces
{
    public interface IEvacuationZoneRepository : IBaseRepository<EvacuationZoneModel>
    {
        Task Create(EvacuationZoneModel zone);
        Task<List<EvacuationZoneModel>> GetNotCompleteZone();
    }
}
