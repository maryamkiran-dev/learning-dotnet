using Microsoft.AspNetCore.Identity;
namespace IdentityApi.Models
{ 
    public class UsersModel : IdentityUser
    {
        public string? Name { get; set; }
        public string Role { get; set; } = "";
    }

}
