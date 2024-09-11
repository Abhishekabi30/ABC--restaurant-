using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using ABC.Database;
using ABC.Model;

namespace ABC_Resturant.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ResturantController : ControllerBase
    {
        private readonly dbContext _dbContext;

        public ResturantController(dbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // POST: api/Resturant
        [HttpPost]
        [Route("AdminAdd")]
        public IActionResult CreateResturant([FromBody] Resturant resturant)
        {
            if (resturant == null)
            {
                return BadRequest("Resturant cannot be null.");
            }

            _dbContext.Resturants.Add(resturant);
            _dbContext.SaveChanges();

            return CreatedAtAction(nameof(GetAllResturants), new { id = resturant.Id }, resturant);
        }

        // GET: api/Resturant
        [HttpGet]
        [Route("CustomerView")]
        public IActionResult GetAllResturants()
        {
            var resturants = _dbContext.Resturants.ToList();
            return Ok(resturants);
        }

        // PUT: api/Resturant/{id}
        [HttpPut("AdminUpdate/{id}")]
        public IActionResult UpdateResturant(int id, [FromBody] Resturant resturant)
        {
            if (id != resturant.Id)
            {
                return BadRequest("Resturant ID mismatch.");
            }

            var existingResturant = _dbContext.Resturants.Find(id);
            if (existingResturant == null)
            {
                return NotFound("Resturant not found.");
            }

            existingResturant.Name = resturant.Name;
            existingResturant.Location = resturant.Location;
            existingResturant.phone = resturant.phone;
            existingResturant.Overview = resturant.Overview;
            existingResturant.Email = resturant.Email;

            _dbContext.Resturants.Update(existingResturant);
            _dbContext.SaveChanges();

            return NoContent();
        }

        // DELETE: api/Resturant/{id}
        [HttpDelete("AdminDelete/{id}")]
        public IActionResult DeleteResturant(int id)
        {
            var resturant = _dbContext.Resturants.Find(id);
            if (resturant == null)
            {
                return NotFound("Resturant not found.");
            }

            _dbContext.Resturants.Remove(resturant);
            _dbContext.SaveChanges();

            return Ok("Resturant deleted successfully.");
        }

        // GET: api/Resturant/SearchByLocation/{location}
        [HttpGet("SearchByLocation/{location}")]
        public IActionResult SearchByLocation(string location)
        {
            var resturants = _dbContext.Resturants
                                      .Where(r => r.Location == location)
                                      .ToList();

            if (resturants == null || !resturants.Any())
            {
                return NotFound($"No resturants found in location '{location}'.");
            }

            return Ok(resturants);
        }
    }
}
