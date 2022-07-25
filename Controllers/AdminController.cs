using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PS4GamingApplication.Models;


namespace PS4GamingApplication.Controllers
{
    [ApiController]
    public class AdminController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _context;

        public AdminController(IConfiguration configuration, ApplicationDbContext context)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpGet]
        [Route("UsersList"), Authorize(Roles = "Admin")]
        public ActionResult GetUsers()
        {
            var user = _context.Users.Where(x => x.Role == "User");

            if (user != null)
            {
                var users = user.Select(u => new
                {
                    Id = u.UserId,
                    UserName = u.UserName,
                    Email = u.Email,
                });
                return Ok(users);
            }
            else
            {
                return BadRequest("No Users");
            }
        }
        [HttpGet]
        [Route("GamesList"), Authorize(Roles = "Admin")]
        public ActionResult GetGames()
        {
            var game = _context.Games;  

            if (game != null)
            {
                var Games = game.Select(m => new
                {
                    Id = m.GameId,
                    gameName = m.GameName,
                    Description = m.Description,
                    gamePrice = m.GamePrice,

                });
                return Ok(Games);
            }
            else
            {
                return BadRequest("No game");
            }
        }
        [HttpPost]
        [Route("addgame"), Authorize(Roles = "Admin")]
        public string Addgame([FromBody] Game game)
        {
            try
            {
                var checkgame = _context.Games.SingleOrDefault(x => x.GameName == game.GameName);
                if (checkgame == null)
                {
                    _context.Games.Add(game);
                    _context.SaveChanges();
                    return "game Added Successfully";
                }
                else
                {
                    return "game already exists !";
                }
            }
            catch (Exception ex)
            {
                return "Exception! + " + ex;
            }
        }

        [HttpPut]
        [Route("updateGame/{id}"), Authorize(Roles = "Admin")]
        public string UpdateGame([FromBody] Game game, int? id)
        {
            try
            {
                var update = _context.Games.Where(x => x.GameId == id).SingleOrDefault();
                update.GameName = game.GameName;
                update.Description = game.Description;
                update.GamePrice = game.GamePrice;


                _context.SaveChanges();
                return "Game : " + game.GameName + " is Updated";
            }
            catch (Exception ex)
            {
                return "Error Occured " + ex;
            }

        }

        [HttpDelete]
        [Route("deleteGame/{id}"), Authorize(Roles = "Admin")]
        public string DeleteGame(int? id)
        {
            try
            {
                var game = _context.Games.Where(e => e.GameId == id).SingleOrDefault();
                _context.Games.Remove(game);
                _context.SaveChanges();

                return "Course with Id=" + id + " is deleted successfully";
            }
            catch (Exception ex)
            {
                return "Exception occurred: " + ex;
            }
        }


    }
}


