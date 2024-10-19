using EvacuationPlanningMonitoring.Models.DbModels;

namespace EvacuationPlanningMonitoring.Repositorys.Interfaces
{
    public interface IEvacuationZoneRepository : IBaseRepository<EvacuationZoneModel>
    {
        Task<List<EvacuationZoneModel>> GetNotCompleteZone();
    }
}
