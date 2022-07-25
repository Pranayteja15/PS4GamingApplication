using System.ComponentModel.DataAnnotations;

namespace PS4GamingApplication.Models
{
    public class Game
    {
        [Key]
       public int GameId { get; set; }
       public string GameName { get; set; }
       public int GamePrice { get; set; }
       public string Description { get; set; }
    }
}
