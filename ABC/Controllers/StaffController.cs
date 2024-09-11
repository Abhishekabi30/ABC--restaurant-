using Microsoft.AspNetCore.Mvc;

using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ABC.Database;

namespace ABC_Restaurant.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaffController : ControllerBase
    {
        private readonly dbContext _dbContext;

        public StaffController(dbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // POST: api/Staff/Login
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] StaffLoginDto loginDto)
        {
            var staff = await _dbContext.Staffs
                .FirstOrDefaultAsync(s => s.Email == loginDto.Email && s.Password == loginDto.Password);

            if (staff == null)
                return Unauthorized("Invalid email or password.");

            return Ok(new { staff.Id, staff.Name, staff.Email });
        }

        // DELETE: api/Staff/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStaff(string id)
        {
            var staff = await _dbContext.Staffs.FindAsync(id);
            if (staff == null)
                return NotFound();

            _dbContext.Staffs.Remove(staff);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }
    }

    public class StaffLoginDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
