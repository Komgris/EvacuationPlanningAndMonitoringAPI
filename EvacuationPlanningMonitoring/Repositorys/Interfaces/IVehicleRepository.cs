using EvacuationPlanningMonitoring.Models.DbModels;
using EvacuationPlanningMonitoring.Models.DTOs;

namespace EvacuationPlanningMonitoring.Repositorys.Interfaces
{
    public interface IVehicleRepository : IBaseRepository<VehicleModel>
    {
        Task<List<VehicleModel>> GetAvaiableVehicle();
        Task Create(VehicleModel vehicle);
    }
}
