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
        public IActionResult GetStatus()
        {
            return Ok(new List<EvacuationStatusDTO>());
        }

        [HttpPost("plan")]
        public async Task<IActionResult> Plan()
        {
            var plan = await _evacuationService.GetPlan();
            return Ok(plan);
        }

        [HttpPut("update")]
        public IActionResult Update()
        {
            return Ok(new EvacuationStatusDTO());
        }

        [HttpDelete("clear")]
        public IActionResult Clear()
        {
            return Ok();
        }
    }
}
