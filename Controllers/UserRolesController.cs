using IdentityApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController] 
public class UserRolesController : ControllerBase
{
    private readonly UserManager<UsersModel> _userManager;

    public UserRolesController(UserManager<UsersModel> userManager)
    {
        _userManager = userManager;
    }

    [HttpGet("{email}")]
    public async Task<IActionResult> GetUserRoles(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user == null)
            return NotFound(new { message = "User not found" });

        var roles = await _userManager.GetRolesAsync(user);
        return Ok(new
        {
            user.Email,
            Roles = roles
        });
    }

    [HttpPost("assign")]
    public async Task<IActionResult> AssignRole(string email, string roleName)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user == null)
            return NotFound(new { message = "User not found" });

        var roleExists = await _userManager.IsInRoleAsync(user, roleName);
        if (roleExists)
            return BadRequest(new { message = "User already has this role" });

        await _userManager.AddToRoleAsync(user, roleName);
        //here role is also stored in Role column of UsersTable IA 
        user.Role = roleName;
        await _userManager.UpdateAsync(user);
        return Ok(new { message = $"Role '{roleName}' assigned to {email}" });
    }
    [Authorize(Roles = "Admin")]
    [HttpGet("admin-only")]
    public IActionResult AdminEndpoint()
    {
        var userEmail = User.Identity?.Name ?? "Unknown user";
        return Ok($"Hello {userEmail}, you have access because you are an Admin.");
    }
    [Authorize(Roles = "PublicUser")]
    [HttpGet("user-only")]
    public IActionResult UserEndpoint()
    {
        var userTest = User;
        var userEmail = User.Identity?.Name ?? "Unknown user";
        return Ok($" Hello {userEmail}, you have access because you are an User.");
    }
}
