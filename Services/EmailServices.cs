using Azure.Core;
using IdentityApi.Data;
using IdentityApi.Dto;
using IdentityApi.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Text;

namespace IdentityApi.Services
{
    public class EmailServices 
    {
        // Implement email sending functionality here
        private readonly UserManager<UsersModel> _userManager;
        private readonly SignInManager<UsersModel> _signInManager;
        private readonly IConfiguration _configuration;
        public EmailServices(UserManager<UsersModel> userManager, SignInManager<UsersModel> signInManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }
        public async Task<string> ForgotPasswordAsync(ForgotPasswordDto model)
        {
            // Step 1: Find user by email
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
                return "User not found"; 
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);  
            return token;
        }

        public async Task<string> ResetPasswordRequest(ResetPasswordDto resetPasswordDto)
        {
            var user = await _userManager.FindByEmailAsync(resetPasswordDto.Email);
            if (user == null)
                return ("Not Found");  
            var password = await _userManager.ResetPasswordAsync(user, resetPasswordDto.Token, resetPasswordDto.NewPassword);
            Console.WriteLine(password);
            if (!password.Succeeded)
            {
                var errors = string.Join(", ", password.Errors.Select(e => e.Description));
                return $"Password reset failed: {errors}";
            }
             
            return "Password reset successful";

        }
        public async Task<IEnumerable<UsersModel>> GetAllUsersAsync()
        {
            return await _userManager.Users.ToListAsync();
        }
        public async Task<UsersModel> GetByIdUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if(user==null)
                return null;


            
            return user;
        }
    }
}
