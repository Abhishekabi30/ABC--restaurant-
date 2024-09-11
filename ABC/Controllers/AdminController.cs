using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ABC.Model;

using ABC.Database;

namespace ABC_Restaurant.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly dbContext _dbContext;

        public AdminController(dbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        [Route("GetAdminByEmail/{email}")]
        public async Task<IActionResult> GetAdminByEmail(string email)
        {
            // Find the admin with the provided email
            var admin = await _dbContext.Admins.Where(a => a.Email == email).ToListAsync();

            // If admin is not found, return 404 Not Found
            if (admin == null)
            {
                return NotFound($"Admin with email '{email}' not found");
            }

            // Return the admin details
            return Ok(admin);
        }

        // POST: api/Admin/Login
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] AdminLoginDto loginDto)
        {
            var admin = await _dbContext.Admins
                .FirstOrDefaultAsync(a => a.Email == loginDto.Email && a.Password == loginDto.Password);

            if (admin == null)
                return Unauthorized("Invalid email or password.");

            return Ok(new { admin.Id, admin.Name, admin.Email });
        }



    }

    public class AdminLoginDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
