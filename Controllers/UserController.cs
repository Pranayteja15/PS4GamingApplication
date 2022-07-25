using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PS4GamingApplication.Models;

namespace PS4GamingApplication.Controllers
{

    [ApiController]
    public class UserController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _context;

        public UserController(IConfiguration configuration, ApplicationDbContext context)
        {
            _context = context;
            _configuration = configuration;
        }
        [HttpGet]
        [Route("ViewGamesList"), Authorize(Roles = "User")]
        public ActionResult GetGames()
        {
            var game= _context.Games;

            if (game != null)
            {
                var games = game.Select(m => new
                {
                    Id = m.GameId,
                    GameName = m.GameName,
                    Description = m.Description,
                    GamePrice=m.GamePrice
                    
                });
                return Ok(games);
            }
            else
            {
                return BadRequest("No Games");
            }
        }

        

       
    }
}
