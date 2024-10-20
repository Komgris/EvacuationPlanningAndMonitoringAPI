using EvacuationPlanningMonitoring.Models.DbModels;
using EvacuationPlanningMonitoring.Models.DTOs;
using EvacuationPlanningMonitoring.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EvacuationPlanningMonitoring.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EvacuationsController : ControllerBase
    {
        private readonly IEvacuationService _evacuationService;
        public EvacuationsController(
            IEvacuationService evacuationService
            ) { 
            _evacuationService= evacuationService;
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
            await _evacuationService.UpdateStatus(status);
            return Ok();
        }

        [HttpDelete("clear")]
        public IActionResult Clear()
        {
            return Ok();
        }
    }
}
