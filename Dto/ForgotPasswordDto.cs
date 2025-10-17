using System.ComponentModel.DataAnnotations;

namespace IdentityApi.Dto
{
    public class ForgotPasswordDto
    {
        [Required]
        public string Email { get; set; }
    }
}
