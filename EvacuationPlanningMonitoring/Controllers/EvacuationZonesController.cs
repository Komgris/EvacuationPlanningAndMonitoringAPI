using EvacuationPlanningMonitoring.Models.DTOs;
using EvacuationPlanningMonitoring.Models.DTOs.Base;
using EvacuationPlanningMonitoring.Services.Interfaces;
using EvacuationPlanningMonitoring.Validators.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace EvacuationPlanningMonitoring.Controllers
{
    [Route("api/evacuation-zones")]
    [ApiController]
    public class EvacuationZonesController : ControllerBase
    {
        private readonly IEvacuationService _evacuationService;
        private readonly IEvacuationsValidator _evacuationsValidator;
        public EvacuationZonesController(
            IEvacuationService evacuationService,
            IEvacuationsValidator evacuationsValidator
            ) 
        { 
            _evacuationService= evacuationService;
            _evacuationsValidator= evacuationsValidator;
        }

        [HttpPost]
        public async Task<IActionResult> Create(List<EvacuationZoneDTO> zones)
        {
            var validateResult = _evacuationsValidator.IsValidZones(zones);
            if (validateResult.Count == 0)
            {
                await _evacuationService.Create(zones);
                return Ok(new BaseResponse<List<EvacuationStatusDTO>>()
                {
                    IsSuccess = true,
                    StatusCode = (int)HttpStatusCode.OK
                });
            }
            else
            {
                return BadRequest(
                    new BaseResponse() { 
                        IsSuccess = false, 
                        Message = String.Join(", ", validateResult), 
                        StatusCode = (int)HttpStatusCode.BadRequest 
                    });
            }
        }
    }
}
