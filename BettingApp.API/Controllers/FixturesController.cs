using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BettingApp.API.Models;
using BettingApp.BLL.Dto;
using BettingApp.BLL;
using BettingApp.API.Logging;
using Microsoft.AspNetCore.Authorization;

namespace BettingApp.API.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v1/[controller]")]
    [Authorize]
    public class FixturesController : ControllerBase
    {
        private readonly IFixtureService _fixtureService;
        private ILoggerManager _logger;

        public FixturesController(IFixtureService fixtureService, ILoggerManager logger)
        {
            _fixtureService = fixtureService;
            _logger = logger;
        }

        [HttpPost(nameof(CreateFixtureModel))]
        public async Task<IActionResult> CreateFixture(CreateFixtureModel model)
        {
            _logger.LogInfo("Start of CreateFixture api call...");
            try
            {
                var response = await _fixtureService.CreateFixture(model);

                _logger.LogInfo("End of CreateFixture api call...");
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception: " + ex.ToString());
                throw;
            }
        }



    }
}
