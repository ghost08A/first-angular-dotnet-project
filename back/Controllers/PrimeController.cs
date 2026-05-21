
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using back.DTOs;
using back.Services;
namespace back.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class PrimeController : ControllerBase
    {
        private readonly IPrimeService _primeService;
        public PrimeController(IPrimeService primeService)
        {
            _primeService = primeService;
        }

        [HttpPost("calculate")]
        public IActionResult Calculate([FromBody] PrimeRequest request)
        {
            try
            {
                var result = _primeService.CalculatePrimeNeighbors(request);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });

            }
        }
    }
}