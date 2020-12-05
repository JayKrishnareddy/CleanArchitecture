using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.RepositoryPattern.UserLogin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CleanArchitecture.Controllers
{
    [ApiVersion("2.0")]
    [ApiExplorerSettings(GroupName = "v2")]
    public class RoleController : BaseController
    {
        #region Property
        private readonly IUserService _userService;
        #endregion

        #region Constructor
        public RoleController(IUserService userService)
        {
            _userService = userService;
        }
        #endregion

        #region GetRoles
        /// <summary>
        /// Get the User Roles 
        /// </summary>
        /// <returns></returns>
        [HttpGet(nameof(GetUserRoles))]
        public async Task<IActionResult> GetUserRoles()
        {
            try
            {
                var result = await _userService.GetUserRolesAsync();
                if (result is not null) return Ok(result); else return BadRequest("No Data Found");
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
