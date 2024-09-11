using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using System.Linq;
using ABC.Database;
using ABC.Model;

namespace ABC_Resturant.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MenuController : ControllerBase
    {
        private readonly dbContext _dbContext;

        public MenuController(dbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // POST: api/Menu
        [HttpPost]
        [Route("AdminAdd")]
        public IActionResult CreateMenu([FromBody] Menu menu)
        {
            if (menu == null)
            {
                return BadRequest("Menu object cannot be null.");
            }

            _dbContext.Menus.Add(menu);
            _dbContext.SaveChanges();

            return CreatedAtAction(nameof(GetAllMenus), new { id = menu.Id }, menu);
        }

        // GET: api/Menu
        [HttpGet]
        [Route("CustomerView")]
        public IActionResult GetAllMenus()
        {
            var menus = _dbContext.Menus.Include(m => m.Resturant).ToList();
            return Ok(menus);
        }

        // DELETE: api/Menu/{id}
        [HttpDelete("AdminDelete/{id}")]
        public IActionResult DeleteMenu(int id)
        {
            var menu = _dbContext.Menus.Find(id);
            if (menu == null)
            {
                return NotFound("Menu not found.");
            }

            _dbContext.Menus.Remove(menu);
            _dbContext.SaveChanges();

            return NoContent();
        }

        // PUT: api/Menu/{id}
        [HttpPut("AdminUpdate/{id}")]
        public IActionResult UpdateMenu(int id, [FromBody] Menu updatedMenu)
        {
            if (id != updatedMenu.Id)
            {
                return BadRequest("Menu ID mismatch.");
            }

            var menu = _dbContext.Menus.Find(id);
            if (menu == null)
            {
                return NotFound("Menu not found.");
            }

            menu.Name = updatedMenu.Name;
            menu.Description = updatedMenu.Description;
            menu.Price = updatedMenu.Price;
            menu.Category = updatedMenu.Category;
            menu.Status = updatedMenu.Status;
            menu.ResturantId = updatedMenu.ResturantId;

            _dbContext.Menus.Update(menu);
            _dbContext.SaveChanges();

            return NoContent();
        }

        // GET: api/Menu/Category/{category}
        [HttpGet("Category/{category}")]
        public IActionResult SearchByCategory(string category)
        {
            var menus = _dbContext.Menus.Include(m => m.Resturant)
                                      .Where(m => m.Category == category)
                                      .ToList();

            if (menus == null || !menus.Any())
            {
                return NotFound("No menus found for the given category.");
            }

            return Ok(menus);
        }
    }
}
