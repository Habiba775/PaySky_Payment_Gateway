using Domain.Entities.Custom_Entities;
using Duende.IdentityServer.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Application.Interfaces;
using Application.Users.Commands;
using Application.Users.Commands.CreateUser;
using Application.Users.Commands.LoginUser;



    [Route("api/[controller]")]
    [ApiController]
    public class IdentityController : ControllerBase
    {
        private readonly UserManager<APPUser> _userManager;
       private readonly TokenService _tokenService;

        public IdentityController(UserManager<APPUser> userManager, TokenService tokenService)
        {
            _userManager = userManager;
            _tokenService = tokenService;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterNewUser(CreateUserCommand user)
        {
            if (ModelState.IsValid)
            {
                var appUser = new APPUser
                {
                    UserName = user.Username,
                    Email = user.Email,
                     PhoneNumber = user.PhoneNumber
                };

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var validRoles = new[] { "Admin", "User" };
                if (string.IsNullOrEmpty(user.Role) || !validRoles.Contains(user.Role, StringComparer.OrdinalIgnoreCase))
                {
                    return BadRequest("Role must be either 'Admin' or 'User'.");
                }

                IdentityResult result = await _userManager.CreateAsync(appUser, user.Password);

                if (result.Succeeded)
                {

                    await _userManager.AddToRoleAsync(appUser, user.Role);


                    return Ok("Registration successful");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }

                return BadRequest(ModelState);
            }

            return BadRequest(ModelState);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> LoginUser(LoginUserCommand login)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userManager.FindByEmailAsync(login.Email);

            if (user == null)
                return Unauthorized("Invalid email or password");

            var passwordValid = await _userManager.CheckPasswordAsync(user, login.Password);
            if (!passwordValid)
                return Unauthorized("Invalid email or password");

            var roles = await _userManager.GetRolesAsync(user);
            var role = roles.FirstOrDefault() ?? "User";

            var token = await _tokenService.GenerateToken(user, roles);


            return Ok(new { token });
        }
    }
    