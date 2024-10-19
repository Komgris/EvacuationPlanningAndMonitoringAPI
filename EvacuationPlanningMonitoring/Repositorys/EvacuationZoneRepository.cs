using EvacuationPlanningMonitoring.Models.DbModels;
using EvacuationPlanningMonitoring.Repositorys.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EvacuationPlanningMonitoring.Repositorys
{
    public class EvacuationZoneRepository : BaseRepository<EvacuationZoneModel>, IEvacuationZoneRepository
    {
        public EvacuationZoneRepository(AppDbContext dbContext) : base(dbContext) { }

        public Task<List<EvacuationZoneModel>> GetNotCompleteZone()
        {
           var zones = GetQueryable().Where(x=>x.RemainPeople > 0).ToListAsync();
            return zones;
        }
    }
}
