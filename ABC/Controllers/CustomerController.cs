using Microsoft.AspNetCore.Mvc;

using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using ABC.Database;
using ABC.Model;

namespace ABC_Resturant.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly dbContext _dbContext;

        public CustomerController(dbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // POST: api/Customer/Register
        [HttpPost("Register")]
        public IActionResult Register([FromBody] Customer customer)
        {
            if (_dbContext.Customers.Any(c => c.Email == customer.Email))
            {
                return BadRequest("Customer with this email already exists.");
            }

            // Hashing the password before saving
            customer.Password = HashPassword(customer.Password);

            _dbContext.Customers.Add(customer);
            _dbContext.SaveChanges();

            return CreatedAtAction(nameof(Register), new { id = customer.Id }, customer);
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var customer = _dbContext.Customers.Find(id);
            if (customer == null)
            {
                return NotFound($"Customer with Id = {id} not found.");
            }

            _dbContext.Customers.Remove(customer);
            _dbContext.SaveChanges();

            return Ok($"Customer with Id = {id} deleted.");
        }


        // POST: api/Customer/Login
        [HttpPost("Login")]
        public IActionResult Login([FromBody] LoginModel model)
        {
            var customer = _dbContext.Customers
                                   .SingleOrDefault(c => c.Email == model.Email);

            if (customer == null || !VerifyPassword(model.Password, customer.Password))
            {
                return Unauthorized("Invalid email or password.");
            }

            // Generate a token or session (for simplicity, returning a success message)
            return Ok("Login successful.");
        }

        // Password hashing
        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(password);
                var hash = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }

        // Password verification
        private bool VerifyPassword(string enteredPassword, string storedHash)
        {
            var hashOfEnteredPassword = HashPassword(enteredPassword);
            return hashOfEnteredPassword == storedHash;
        }
    }

    // Model for Login
    public class LoginModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
