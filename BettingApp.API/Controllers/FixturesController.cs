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

namespace BettingApp.API.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Route("api/[controller]")]
    public class FixturesController : ControllerBase
    {
        private readonly IFixtureService _fixtureService;

        public FixturesController(IFixtureService fixtureService)
        {
            _fixtureService = fixtureService;
        }

        [HttpPost(nameof(CreateFixtureModel))]
        public async Task<IActionResult> CreateFixture(CreateFixtureModel model)
        {

            var response = await _fixtureService.CreateFixture(model);

            return Ok(response);
        }



    }
}
