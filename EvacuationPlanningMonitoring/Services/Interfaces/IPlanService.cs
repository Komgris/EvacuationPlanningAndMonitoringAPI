using EvacuationPlanningMonitoring.Models.DbModels;
using EvacuationPlanningMonitoring.Models.DTOs;

namespace EvacuationPlanningMonitoring.Services.Interfaces
{
    public interface IPlanService
    {
        List<EvacuationPlanDTO> GeneratePlan(List<VehicleModel> vehicles, List<EvacuationZoneModel> evacuationZones);
    }
}
