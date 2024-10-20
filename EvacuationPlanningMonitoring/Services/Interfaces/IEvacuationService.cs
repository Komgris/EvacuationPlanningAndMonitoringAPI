﻿using EvacuationPlanningMonitoring.Models.DbModels;
using EvacuationPlanningMonitoring.Models.DTOs;

namespace EvacuationPlanningMonitoring.Services.Interfaces
{
    public interface IEvacuationService
    {
        Task GeneratePlan();
        Task Create(EvacuationZoneDTO zoneDto);
        Task<List<EvacuationPlanDTO>> GetPlan();
        Task<List<EvacuationStatusDTO>> GetStatus();
        Task UpdateStatus(UpdateEvcuationStatusDto status);
    }
}
