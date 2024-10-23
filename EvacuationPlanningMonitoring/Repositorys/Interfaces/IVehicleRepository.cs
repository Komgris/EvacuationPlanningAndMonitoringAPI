using EvacuationPlanningMonitoring.Models.DbModels;
using EvacuationPlanningMonitoring.Models.DTOs;

namespace EvacuationPlanningMonitoring.Repositorys.Interfaces
{
    public interface IVehicleRepository : IBaseRepository<VehicleModel>
    {
        Task<List<VehicleModel>> GetAvaiableVehicle();
        Task<List<VehicleModel>> GetAll();
        Task Create(List<VehicleModel> vehicle);
        Task ChangeVehicleStatus(string vehicle, string status);
        Task ClearVehicle();
    }
}
