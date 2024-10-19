using EvacuationPlanningMonitoring.Models;
using EvacuationPlanningMonitoring.Models.DbModels;
using EvacuationPlanningMonitoring.Repositorys.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EvacuationPlanningMonitoring.Repositorys
{
    public class VehicleRepositorycs : BaseRepository<VehicleModel>, IVehicleRepository
    {
        public VehicleRepositorycs(AppDbContext dbContext) : base(dbContext) { }

        public Task<List<VehicleModel>> GetAvaiableVehicle()
        {
            var vehicles = GetQueryable().Where(x => x.Status == VehicleStatus.Available).ToListAsync();
            return vehicles;
        }
    }
}
