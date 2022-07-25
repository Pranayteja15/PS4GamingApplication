using System.ComponentModel.DataAnnotations;

namespace PS4GamingApplication.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }  
        public string Username { get; set; } = string.Empty;
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }

        public int Wallet { get; set; }=0;

    }
}
