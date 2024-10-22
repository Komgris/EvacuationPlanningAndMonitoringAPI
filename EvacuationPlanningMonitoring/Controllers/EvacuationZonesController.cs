using EvacuationPlanningMonitoring.Models.DTOs;
using EvacuationPlanningMonitoring.Services.Interfaces;
using EvacuationPlanningMonitoring.Validators.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
            var errorResult = _evacuationsValidator.IsValidZones(zones);
            if (errorResult.Count == 0)
            {
                await _evacuationService.Create(zones);
                return Ok();
            }
            else
            {
                return BadRequest(new { error = new List<string>(){ });
            }
        }
    }
}
