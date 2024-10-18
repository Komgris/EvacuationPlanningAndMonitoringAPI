using EvacuationPlanningMonitoring.Models.DbModels;
using EvacuationPlanningMonitoring.Repositorys.Interfaces;

namespace EvacuationPlanningMonitoring.Repositorys
{
    public class VehicleRepositorycs : BaseRepository<VehicleModel>, IVehicleRepository
    {
        public VehicleRepositorycs(AppDbContext dbContext) : base(dbContext) { }
    }
}
