using GoldenRaspberryAPI.Service;
using Microsoft.AspNetCore.Mvc;

namespace GoldenRaspberryAPI.Controllers
{
    [ApiController]
    [Route("api/movies")]
    public class MovieController : ControllerBase
    {
        private readonly AwardIntervalService _awardIntervalService;

        public MovieController(AwardIntervalService awardIntervalService)
        {
            _awardIntervalService = awardIntervalService;
        }

        [HttpGet("award-intervals")]
        public IActionResult GetAwardIntervals()
        {
            var result = _awardIntervalService.GetAwardIntervals();
            return Ok(result);
        }
    }
}
