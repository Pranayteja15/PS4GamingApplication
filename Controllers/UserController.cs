using Microsoft.AspNetCore.Mvc;
using PS4GamingApplication.Models;

namespace PS4GamingApplication.Controllers
{
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public UserController(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        [HttpPut("UpdateUser")]
        public async Task<IActionResult> PutAsync(User userToUpdate)
        {
            _applicationDbContext.users.Update(userToUpdate);
            await _applicationDbContext.SaveChangesAsync();
            return NoContent();
        }
    }
}
