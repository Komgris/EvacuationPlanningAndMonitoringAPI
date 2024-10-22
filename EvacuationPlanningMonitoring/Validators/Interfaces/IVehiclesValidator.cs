using EvacuationPlanningMonitoring.Models.DTOs;

namespace EvacuationPlanningMonitoring.Validators.Interfaces
{
    public interface IVehiclesValidator
    {
        List<string> IsValidVehicles(List<EvacuationZoneDTO> zones);
    }
}
