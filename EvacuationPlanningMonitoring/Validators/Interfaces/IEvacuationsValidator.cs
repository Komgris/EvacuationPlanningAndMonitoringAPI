using EvacuationPlanningMonitoring.Models.DTOs;
using Microsoft.AspNetCore.Http;

namespace EvacuationPlanningMonitoring.Validators.Interfaces
{
    public interface IEvacuationsValidator : IBaseValidator
    {
        List<string> IsValidZones(List<EvacuationZoneDTO> zones);
        Task<List<string>> IsValidUpdateStatus(UpdateEvcuationStatusDto status);
    }
}
