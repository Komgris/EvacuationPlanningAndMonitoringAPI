using EvacuationPlanningMonitoring.Models.DbModels;
using EvacuationPlanningMonitoring.Repositorys.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EvacuationPlanningMonitoring.Repositorys
{
    public class EvacuationZoneRepository : BaseRepository<EvacuationZoneModel>, IEvacuationZoneRepository
    {
        public EvacuationZoneRepository(AppDbContext dbContext) : base(dbContext) { }

        public async Task Create(EvacuationZoneModel zone)
        {
            Add(zone);
            await SaveChangesAsync();
        }

        public async Task<List<EvacuationZoneModel>> GetNotCompleteZone()
        {
           var zones = await GetQueryable().Where(x=>x.RemainPeople > 0).ToListAsync();
            return zones;
        }

        public async Task<List<EvacuationZoneModel>> GetAll()
        {
            var zones = await GetQueryable().ToListAsync();
            return zones;
        }

        public async Task EvcuationDone(EvacuationPlanModel plan)
        {
            var zone = await GetQueryable().FirstOrDefaultAsync(x => x.ZoneID == plan.ZoneID);
            if(zone != null)
            {
                var remainPeople = zone.NumberOfPeople - plan.NumberOfPeople;
                zone.RemainPeople = remainPeople;
                Update(zone);
                await SaveChangesAsync();
            }
        }
    }
}
