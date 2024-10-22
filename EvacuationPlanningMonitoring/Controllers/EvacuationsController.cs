using EvacuationPlanningMonitoring.Models.DbModels;
using EvacuationPlanningMonitoring.Models.DTOs;
using EvacuationPlanningMonitoring.Services.Interfaces;
using EvacuationPlanningMonitoring.Validators.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EvacuationPlanningMonitoring.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EvacuationsController : ControllerBase
    {
        private readonly IEvacuationService _evacuationService;
        private readonly IEvacuationsValidator _evacuationsValidator;
        public EvacuationsController(
            IEvacuationService evacuationService,
            IEvacuationsValidator evacuationsValidator
            ) { 
            _evacuationService= evacuationService;
            _evacuationsValidator= evacuationsValidator;
        }

        [HttpGet("status")]
        public async Task<IActionResult> GetStatus()
        {
            var status = await _evacuationService.GetStatus();
            return Ok(status);
        }

        [HttpPost("plan")]
        public async Task<IActionResult> Plan()
        {
            var plans = await _evacuationService.GetPlan();
            return Ok(plans);
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody]UpdateEvcuationStatusDto status)
        {
            var validateResult = await _evacuationsValidator.IsValidUpdateStatus(status);
            if (string.IsNullOrEmpty(validateResult))
            {
                await _evacuationService.UpdateStatus(status);
                return Ok();
            }
            else
            {
                return BadRequest(new { error = validateResult });
            }
        }

        [HttpDelete("clear")]
        public async Task<IActionResult> Clear()
        {
            await _evacuationService.Clear();
            return Ok();
        }
    }
}
