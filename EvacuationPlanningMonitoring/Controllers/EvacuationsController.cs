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
        [HttpGet("status")]
        public IActionResult GetStatus()
        {
            return Ok(new List<EvacuationStatusDTO>());
        }

        [HttpPost("plan")]
        public IActionResult Plan()
        {
            return Ok(new List<EvacuationPlanDTO>());
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
