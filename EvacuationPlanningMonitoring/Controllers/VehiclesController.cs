using EvacuationPlanningMonitoring.Models.DbModels;
using EvacuationPlanningMonitoring.Models.DTOs;
using EvacuationPlanningMonitoring.Models.DTOs.Base;
using EvacuationPlanningMonitoring.Repositorys.Interfaces;
using EvacuationPlanningMonitoring.Services.Interfaces;
using EvacuationPlanningMonitoring.Validators.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace EvacuationPlanningMonitoring.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehiclesController : ControllerBase
    {
        private readonly IVehicleService _vehicleService;
        private readonly IVehiclesValidator _vehiclesValidator;
        public VehiclesController(IVehicleService vehicleService, IVehiclesValidator vehiclesValidator) 
        {
            _vehicleService = vehicleService;
            _vehiclesValidator = vehiclesValidator;
        }

        [HttpPost]
        public async Task<IActionResult> Create(List<VehicleDTO> vehicleDTO)
        {
            var validateResult = await _vehiclesValidator.IsValidVehicles(vehicleDTO);
            if (validateResult.Count == 0)
            {
                await _vehicleService.Create(vehicleDTO);
                return Ok(new BaseResponse<List<EvacuationStatusDTO>>()
                {
                    IsSuccess = true,
                    StatusCode = (int)HttpStatusCode.OK
                });
            }
            else
            {
                return BadRequest(
                    new BaseResponse()
                    {
                        IsSuccess = false,
                        Message = String.Join(", ", validateResult),
                        StatusCode = (int)HttpStatusCode.BadRequest
                    });

            }
        }
    }
}
