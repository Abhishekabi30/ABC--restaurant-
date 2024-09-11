using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ABC.Model;
using ABC.Database;
using System.Linq;
using System.Threading.Tasks;


namespace ABC_Resturant.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReservationController : ControllerBase
    {
        private readonly dbContext _dbContext;

        public ReservationController(dbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // POST: api/TableBooking
        [HttpPost]
        public async Task<IActionResult> CreateRservation([FromBody] Reservation reservation)
        {
            if (reservation == null)
            {
                return BadRequest("TableBooking object cannot be null.");
            }

            // Validate input data if necessary
            if (string.IsNullOrEmpty(reservation.Type) || string.IsNullOrEmpty(reservation.NumberOfTable))
            {
                return BadRequest("Type and NumberOfTable are required.");
            }

            _dbContext.Reservations.Add(reservation);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTableBookingById), new { id = reservation.Id }, reservation);
        }

        // GET: api/TableBooking
        [HttpGet]
        public async Task<IActionResult> GetAllTableBookings()
        {
            var tableBookings = await _dbContext.Reservations.Include(t => t.Customer).ToListAsync();
            return Ok(tableBookings);
        }

        // GET: api/TableBooking/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTableBookingById(int id)
        {
            var tableBooking = await _dbContext.Reservations.Include(t => t.Customer).FirstOrDefaultAsync(t => t.Id == id);

            if (tableBooking == null)
            {
                return NotFound("TableBooking not found.");
            }

            return Ok(tableBooking);
        }

        // DELETE: api/TableBooking/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTableBooking(int id)
        {
            var tableBooking = await _dbContext.Reservations.FindAsync(id);

            if (tableBooking == null)
            {
                return NotFound("TableBooking not found.");
            }

            _dbContext.Reservations.Remove(tableBooking);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }
    }
}
