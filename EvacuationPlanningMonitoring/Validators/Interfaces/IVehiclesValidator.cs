using EvacuationPlanningMonitoring.Models.DTOs;

namespace EvacuationPlanningMonitoring.Validators.Interfaces
{
    public interface IVehiclesValidator : IBaseValidator
    {
        Task<List<string>> IsValidVehicles(List<VehicleDTO> vehicleDtos);
    }
}
