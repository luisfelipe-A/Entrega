
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WizardStoreAPI.Models
{
    public class User
    {
        public string UserId { get; set; } = null!;

        public string? Name { get; set; }
        
        [Required]
        public string Password { get; set; }

        [ForeignKey("Role")]
        public string Role { get; set; }

        public User(string userId, string? name, string password, string role)
        {
            UserId = userId;
            Name = name;
            Password = password;
            Role = role;
        }
    }
}