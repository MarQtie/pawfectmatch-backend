using Microsoft.AspNetCore.Mvc;
using PawfectMatch.Backend.Data;

namespace PawfectMatch.Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TestController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("db-check")]
        public IActionResult CheckDb()
        {
            try
            {
                var canConnect = _context.Database.CanConnect();
                return Ok(new { success = canConnect });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, error = ex.Message });
            }
        }
    }
}