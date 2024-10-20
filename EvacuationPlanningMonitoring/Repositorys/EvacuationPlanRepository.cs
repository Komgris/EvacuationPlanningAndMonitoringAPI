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
                await SaveChangesAsync();
                return plan;
            }
            else
            {
                return plan;
            }
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
            var oldPlans = await GetPlan();
            foreach (var plan in plans)
            {
                var duplicatePlan = oldPlans
                    .FirstOrDefault(x => x.VehicleID == plan.VehicleID);
                if (duplicatePlan == null)
                {
                    Add(plan);
                }
                else
                {
                    duplicatePlan.ZoneID= plan.ZoneID;
                    duplicatePlan.ETAMin = plan.ETAMin;
                    duplicatePlan.NumberOfPeople = plan.NumberOfPeople;
                    Update(duplicatePlan);
                }
            }
            await SaveChangesAsync();
        }
    }
}
