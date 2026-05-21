using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using back.DTOs;
using back.Services;

namespace back.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class AtmController : ControllerBase
    {
        private readonly IAtmService _atmService;

        public AtmController(IAtmService atmService)
        {
            _atmService = atmService;
        }

        [HttpPost("withdraw")]
        public IActionResult Withdraw([FromBody] AtmRequest request)
        {
            try
            {
                var result = _atmService.CalculateWithdrawal(request);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}