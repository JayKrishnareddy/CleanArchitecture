using Services.RepositoryPattern.UserLogin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static Services.ViewModels.CommonModel;

namespace CleanArchitecture.Controllers
{
    [ApiVersion("1.0")]
    [ApiExplorerSettings(GroupName = "v1")]
    public class UserController : BaseController
    {
        #region Property
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;
        #endregion

        #region Constructor
        public UserController(IUserService userService, IConfiguration configuration)
        {
            _userService = userService;
            _configuration = configuration;
        }
        #endregion

        #region Create User
        /// <summary>
        /// To Create a User
        /// </summary>
        /// <param name="userModel"></param>
        /// <returns></returns>
        [HttpPost(nameof(CreateUser))]
        public async Task<IActionResult> CreateUser([FromBody]UserModel userModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _userService.CreateUserAsync(userModel);
                    return Ok(result);
                }
                else
                {
                    return BadRequest("Please fill all the required parameters");
                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex);
                throw;
            }
        }
        #endregion

        #region User Login
        /// <summary>
        /// Login Authentication
        /// </summary>
        /// <param name="loginModel"></param>
        /// <returns></returns>
        [HttpPost(nameof(Login)), AllowAnonymous]
        public async Task<IActionResult> Login([FromBody]LoginModel loginModel)
        {
            try
            {
                var response = await _userService.UserLoginAsync(loginModel);
                if (response is true)
                {
                    var userRoles = await _userService.GetUserRolesAsync();
                    var authClaims = new List<Claim>
                             {
                    new Claim(ClaimTypes.Name, loginModel.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                             };

                    foreach (var userRole in userRoles)
                    {
                        authClaims.Add(new Claim(ClaimTypes.Role, userRole.RoleName));
                    }

                    var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

                    var token = new JwtSecurityToken(
                        issuer: _configuration["Jwt:Issuer"],
                        audience: _configuration["Jwt:Issuer"],
                        expires: DateTime.Now.AddHours(3),
                        claims: authClaims,
                        signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                        );
                    return Ok(new
                    {
                        token = new JwtSecurityTokenHandler().WriteToken(token),
                        expiration = token.ValidTo
                    });
                }
                return Unauthorized();

            }

            catch (Exception ex)
            {
                return BadRequest(ex);
                throw;
            }
        }
        #endregion
    }
}
