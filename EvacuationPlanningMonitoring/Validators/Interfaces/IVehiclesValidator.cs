using EvacuationPlanningMonitoring.Models.DTOs;

namespace EvacuationPlanningMonitoring.Validators.Interfaces
{
    public interface IVehiclesValidator : IBaseValidator
    {
        List<string> IsValidVehicles(List<VehicleDTO> zones);
    }
}
