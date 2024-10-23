using EvacuationPlanningMonitoring.Models.DbModels;
using EvacuationPlanningMonitoring.Models.DTOs;

namespace EvacuationPlanningMonitoring.Services.Interfaces
{
    public interface IPlanService
    {
        List<EvacuationPlanModel> GeneratePlan(List<VehicleModel> vehicles, List<EvacuationZoneModel> evacuationZones, List<EvacuationPlanModel> alreadyPlan);
        List<string> ValidateGeneratePlan(List<EvacuationPlanModel> plans, List<EvacuationZoneModel> evacuationZones, List<EvacuationPlanModel> alreadyPlans);    
    }
}
