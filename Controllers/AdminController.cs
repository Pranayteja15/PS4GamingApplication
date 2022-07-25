using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PS4GamingApplication.Models;

namespace PS4GamingApplication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public AdminController(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        [HttpGet("ListOfGameDetails")]
       
        public async Task<IActionResult> GetAsync()
        {
            var games = await _applicationDbContext.games.ToListAsync();
            return Ok(games);
        }

        [HttpPost("AddGame")]
        
        public async Task<IActionResult> PostAsync(Game games)
        {
            _applicationDbContext.games.Add(games);
            await _applicationDbContext.SaveChangesAsync();
            return Created($"/get-games-by-id?id={games.Id}", games);
        }
        [HttpPut("UpdateGame")]
        public async Task<IActionResult> PutAsync(Game gameToUpdate)
        {
            _applicationDbContext.games.Update(gameToUpdate);
            await _applicationDbContext.SaveChangesAsync();
            return NoContent();
        }
        [Route("{id}")]
        [HttpDelete("DeleteGame")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var gameToDelete = await _applicationDbContext.games.FindAsync(id);
            if (gameToDelete == null)
            {
                return NotFound();
            }
            _applicationDbContext.games.Remove(gameToDelete);
            await _applicationDbContext.SaveChangesAsync();
            return NoContent();
        }
    }


}
