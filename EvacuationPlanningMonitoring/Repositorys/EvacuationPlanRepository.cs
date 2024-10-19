using EvacuationPlanningMonitoring.Models;
using EvacuationPlanningMonitoring.Models.DbModels;
using EvacuationPlanningMonitoring.Repositorys.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EvacuationPlanningMonitoring.Repositorys
{
    public class EvacuationPlanRepository : BaseRepository<EvacuationPlanModel>, IEvacuationPlanRepository
    {
        public EvacuationPlanRepository(AppDbContext dbContext) : base(dbContext) { }

        public async Task<List<EvacuationPlanModel>> GetPlan()
        {
           var plans = await GetQueryable().Where(x => x.Status != EvacuationPlanStatus.Done).ToListAsync();
           return plans;
        }

        public async Task SavePlan(List<EvacuationPlanModel> plans)
        {
            var oldPlans = await GetPlan();
            foreach (var plan in plans)
            {
                var duplicatePlan = oldPlans
                    .FirstOrDefault(x => x.VehicleID == plan.VehicleID &&
                    x.ZoneID == plan.ZoneID &&
                    x.NumberOfPeople == plan.NumberOfPeople);
                if (duplicatePlan == null)
                {
                    Add(plan);
                }
                else
                {
                    duplicatePlan.ETAMin = plan.ETAMin;
                    duplicatePlan.NumberOfPeople = plan.NumberOfPeople;
                    Update(duplicatePlan);
                }
            }
            await SaveChangesAsync();
        }
    }
}
