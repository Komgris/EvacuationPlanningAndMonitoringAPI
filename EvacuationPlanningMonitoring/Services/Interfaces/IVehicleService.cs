﻿using EvacuationPlanningMonitoring.Models.DbModels;
using EvacuationPlanningMonitoring.Models.DTOs;

namespace EvacuationPlanningMonitoring.Services.Interfaces
{
    public interface IVehicleService
    {
        Task Create(List<VehicleDTO> vehicleDTO);
        Task<List<VehicleDTO>> Get();
    }
}
