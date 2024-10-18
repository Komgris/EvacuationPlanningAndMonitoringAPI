using EvacuationPlanningMonitoring.Models.DbModels;
using EvacuationPlanningMonitoring.Repositorys.Interfaces;

namespace EvacuationPlanningMonitoring.Repositorys
{
    public class EvacuationZoneRepository : BaseRepository<EvacuationZoneModel>, IEvacuationZoneRepository
    {
        public EvacuationZoneRepository(AppDbContext dbContext) : base(dbContext) { }
    }
}
