using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ABC.Database;
using ABC.Model;
using System.Linq;

namespace ABC_Restaurant.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OfferController : ControllerBase
    {
        private readonly dbContext _dbContext;

        public OfferController(dbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: api/Offer
        [HttpGet]
        public IActionResult GetAllOffers()
        {
            var offers = _dbContext.Offers.ToList();
            return Ok(offers);
        }

        // GET: api/Offer/{id}
        [HttpGet("{id}")]
        public IActionResult GetOfferById(int id)
        {
            var offer = _dbContext.Offers.Find(id);
            if (offer == null)
            {
                return NotFound($"Offer with Id = {id} not found.");
            }
            return Ok(offer);
        }

        // POST: api/Offer
        [HttpPost]
        public IActionResult CreateOffer([FromBody] Offer offer)
        {
            if (offer == null)
            {
                return BadRequest("Offer data is null.");
            }

            _dbContext.Offers.Add(offer);
            _dbContext.SaveChanges();

            return CreatedAtAction(nameof(GetOfferById), new { id = offer.Id }, offer);
        }

        // PUT: api/Offer/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateOffer(int id, [FromBody] Offer offer)
        {
            if (id != offer.Id)
            {
                return BadRequest("Offer ID mismatch.");
            }

            var offerToUpdate = _dbContext.Offers.Find(id);
            if (offerToUpdate == null)
            {
                return NotFound($"Offer with Id = {id} not found.");
            }

            // Update the fields
            offerToUpdate.Name = offer.Name;
            offerToUpdate.Description = offer.Description;
            offerToUpdate.Price = offer.Price;
            offerToUpdate.Url = offer.Url;

            _dbContext.Entry(offerToUpdate).State = EntityState.Modified;
            _dbContext.SaveChanges();

            return Ok("Offer updated successfully.");
        }

        // DELETE: api/Offer/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteOffer(int id)
        {
            var offer = _dbContext.Offers.Find(id);
            if (offer == null)
            {
                return NotFound($"Offer with Id = {id} not found.");
            }

            _dbContext.Offers.Remove(offer);
            _dbContext.SaveChanges();

            return Ok($"Offer with Id = {id} deleted successfully.");
        }
    }
}
