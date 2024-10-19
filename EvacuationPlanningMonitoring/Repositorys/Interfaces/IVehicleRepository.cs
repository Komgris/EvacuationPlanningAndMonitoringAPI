using EvacuationPlanningMonitoring.Models.DbModels;

namespace EvacuationPlanningMonitoring.Repositorys.Interfaces
{
    public interface IVehicleRepository : IBaseRepository<VehicleModel>
    {
        Task<List<VehicleModel>> GetAvaiableVehicle();
    }
}
