﻿using EvacuationPlanningMonitoring.Models.DTOs;
using EvacuationPlanningMonitoring.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EvacuationPlanningMonitoring.Controllers
{
    [Route("api/evacuation-zones")]
    [ApiController]
    public class EvacuationZonesController : ControllerBase
    {
        private readonly IEvacuationService _evacuationService;
        public EvacuationZonesController(
            IEvacuationService evacuationService
            ) 
        { 
            _evacuationService= evacuationService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(List<EvacuationZoneDTO> zones)
        {
            await _evacuationService.Create(zones);
            return Ok();
        }
    }
}
