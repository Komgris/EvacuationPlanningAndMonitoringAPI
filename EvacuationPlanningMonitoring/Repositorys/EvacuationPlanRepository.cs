using EvacuationPlanningMonitoring.Models;
using EvacuationPlanningMonitoring.Models.DbModels;
using EvacuationPlanningMonitoring.Repositorys.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EvacuationPlanningMonitoring.Repositorys
{
    public class EvacuationPlanRepository : BaseRepository<EvacuationPlanModel>, IEvacuationPlanRepository
    {
        public EvacuationPlanRepository(AppDbContext dbContext) : base(dbContext) { }

        public async Task<EvacuationPlanModel> ChangeStatusPlan(string zoneId, string vehicle, string status)
        {
            var plan = await GetQueryable()
                .Where(x => x.Status != EvacuationPlanStatus.Done &&
                x.ZoneID == zoneId &&
                x.VehicleID == vehicle)
                .FirstOrDefaultAsync();
            if (plan != null)
            {
                plan.Status = status;
                Update(plan);
            }
            await SaveChangesAsync();
            return plan;
        }

        public async Task ClearPlan()
        {
            var plans = await GetQueryable().ToListAsync();
            foreach (var plan in plans)
            {
                Delete(plan);
            }
            await SaveChangesAsync();
        }

        public async Task<List<EvacuationPlanModel>> GetPlan()
        {
           var plans = await GetQueryable().Where(x => x.Status != EvacuationPlanStatus.Done).ToListAsync();
           return plans;
        }

        public async Task<List<EvacuationPlanModel>> GetPlanInProgress()
        {
            var plans = await GetQueryable().Where(x => x.Status == EvacuationPlanStatus.InProgress).ToListAsync();
            return plans;
        }

        public async Task SavePlan(List<EvacuationPlanModel> plans)
        {
            var oldPlans = await GetQueryable().Where(x => x.Status == EvacuationPlanStatus.Ready).ToListAsync();
            foreach(var oldPlan in oldPlans)
            {
                Delete(oldPlan);
            }
            foreach (var plan in plans)
            {
                Add(plan);
            }
            await SaveChangesAsync();
        }
    }
}
