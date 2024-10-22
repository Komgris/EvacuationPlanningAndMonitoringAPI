using EvacuationPlanningMonitoring.Models.DbModels;
using EvacuationPlanningMonitoring.Models.DTOs;
using EvacuationPlanningMonitoring.Models.DTOs.Base;
using EvacuationPlanningMonitoring.Services.Interfaces;
using EvacuationPlanningMonitoring.Validators.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Numerics;

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
            )
        {
            _evacuationService = evacuationService;
            _evacuationsValidator = evacuationsValidator;
        }

        [HttpGet("status")]
        public async Task<IActionResult> GetStatus()
        {
            var status = await _evacuationService.GetStatusCache();
            return Ok(new
                BaseResponse<List<EvacuationStatusDTO>>()
            {
                IsSuccess = true,
                Data = status,
                StatusCode = (int)HttpStatusCode.OK
            });
        }

        [HttpPost("plan")]
        public async Task<IActionResult> Plan()
        {
            var plans = await _evacuationService.GetPlan();
            return Ok(new
                BaseResponse<List<EvacuationPlanDTO>>()
            {
                IsSuccess = true,
                Data = plans,
                StatusCode = (int)HttpStatusCode.OK
            });
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] UpdateEvcuationStatusDto status)
        {
            var validateResult = await _evacuationsValidator.IsValidUpdateStatus(status);
            if (validateResult.Count == 0)
            {
                await _evacuationService.UpdateStatus(status);
                return Ok(
                    new BaseResponse()
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

        [HttpDelete("clear")]
        public async Task<IActionResult> Clear()
        {
            await _evacuationService.Clear();
            return Ok(
                    new BaseResponse()
                    {
                        IsSuccess = true,
                        StatusCode = (int)HttpStatusCode.OK
                    });
        }
    }
}
