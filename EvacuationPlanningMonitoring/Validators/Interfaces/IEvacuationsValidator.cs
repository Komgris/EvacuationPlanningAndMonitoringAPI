using EvacuationPlanningMonitoring.Models.DTOs;
using Microsoft.AspNetCore.Http;

namespace EvacuationPlanningMonitoring.Validators.Interfaces
{
    public interface IEvacuationsValidator : IBaseValidator
    {
        Task<List<string>> IsValidZones(List<EvacuationZoneDTO> zoneDtos);
        Task<List<string>> IsValidUpdateStatus(UpdateEvcuationStatusDto status);
    }
}
