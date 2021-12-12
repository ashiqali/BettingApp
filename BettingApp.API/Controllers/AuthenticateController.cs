using BettingApp.API.Logging;
using BettingApp.API.Models;
using BettingApp.DAL.IdentityAuth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BettingApp.API.Controllers.V1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v1/[controller]")]
    public class AuthenticateController : ControllerBase
    {
        private ILoggerManager _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;

        public AuthenticateController(UserManager<ApplicationUser> userManager, IConfiguration configuration, ILoggerManager logger)
        {
            _userManager = userManager;
            _configuration = configuration;
            _logger = logger;
        }
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            _logger.LogInfo("Start of register api call...");
            try
            {
                var userExists = await _userManager.FindByNameAsync(model.UserName);
                if (userExists != null)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User Already Exists!" });
                }

                ApplicationUser user = new ApplicationUser()
                {
                    Email = model.Email,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = model.UserName
                };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (!result.Succeeded)
                {
                    var errors = new List<string>();

                    foreach (var error in result.Errors)
                    {
                        errors.Add(error.Description);
                    }

                    return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = string.Join(",", errors) });
                }
                _logger.LogInfo("End of register api call...");
                return Ok(new Response { Status = "Success", Message = "User Created Successfully!" });

            }
            catch (Exception ex)
            {
                _logger.LogError("Exception: " + ex.ToString());
                throw;
            }

        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            _logger.LogInfo("Start of login api call...");
            try
            {
                var user = await _userManager.FindByNameAsync(model.Username);
                if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
                {
                    var userRoles = await _userManager.GetRolesAsync(user);

                    var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name,user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
                };

                    foreach (var userRole in userRoles)
                    {
                        authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                    }

                    var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:SecretKey"]));

                    var token = new JwtSecurityToken(
                       issuer: _configuration["JWT:ValidIssuer"],
                       audience: _configuration["JWT:ValidAudience"],
                       expires: DateTime.Now.AddHours(3),
                       claims: authClaims,
                       signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                       );

                    _logger.LogInfo("End of register api call...");
                    return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token), expiration = token.ValidTo });
                }
                _logger.LogInfo("Unauthorized");
                return Unauthorized();
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception: " + ex.ToString());
                throw;
            }

        }

        [HttpPost]
        [Route("change-password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordModel model)
        {
            _logger.LogInfo("Start of change-password api call...");
            try
            {
                var user = await _userManager.FindByNameAsync(model.Username);
                if (user == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, new Response { Status = "Error", Message = "User does not exists!" });
                }
                if (string.Compare(model.NewPassword, model.ConfirmNewPassword) != 0)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new Response { Status = "Error", Message = "The new password and confirm new password does not match!" });
                }
                var result = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
                if (!result.Succeeded)
                {
                    var errors = new List<string>();

                    foreach (var error in result.Errors)
                    {
                        errors.Add(error.Description);
                    }

                    return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = string.Join(",", errors) });
                }
                _logger.LogInfo("End of change-password api call...");
                return Ok(new Response { Status = "Success", Message = "Password Changed Successfully!" });

            }
            catch (Exception ex)
            {
                _logger.LogError("Exception: " + ex.ToString());
                throw;
            }

        }

        [HttpPost]
        [Route("reset-password-token")]
        public async Task<IActionResult> ResetPasswordToken([FromBody] ResetPasswordTokenModel model)
        {
            _logger.LogInfo("Start of reset-change-password api call...");
            try
            {
                var user = await _userManager.FindByNameAsync(model.Username);
                if (user == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, new Response { Status = "Error", Message = "User does not exists!" });
                }
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);

                _logger.LogInfo("End of reset-change-password api call...");
                return Ok(new { token = token });
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception: " + ex.ToString());
                throw;
            }
        }

        [HttpPost]
        [Route("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordModel model)
        {
            _logger.LogInfo("Start of reset-password api call...");
            try
            {
                var user = await _userManager.FindByNameAsync(model.Username);
                if (user == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, new Response { Status = "Error", Message = "User does not exists!" });
                }

                if (string.Compare(model.NewPassword, model.ConfirmNewPassword) != 0)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new Response { Status = "Error", Message = "The new password and confirm new password does not match!" });
                }

                if (string.IsNullOrEmpty(model.Token))
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new Response { Status = "Error", Message = "Invalid Token!" });
                }
                var result = await _userManager.ResetPasswordAsync(user, model.Token, model.NewPassword);
                if (!result.Succeeded)
                {
                    var errors = new List<string>();

                    foreach (var error in result.Errors)
                    {
                        errors.Add(error.Description);
                    }

                    return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = string.Join(",", errors) });
                }
                _logger.LogInfo("End of reset-password api call...");
                return Ok(new Response { Status = "Success", Message = "Password Reseted Successfully!" });
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception: " + ex.ToString());
                throw;
            }
        }
    }
}
