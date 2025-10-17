using System.ComponentModel.DataAnnotations;

namespace IdentityApi.Dto
{
    public class RegisterModelDto
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
