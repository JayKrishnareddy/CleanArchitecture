using DataAccessLayer.ApplicationDbContext;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Models;
using System.Linq;
using static Services.ViewModels.CommonModel;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;

namespace Services.RepositoryPattern.UserLogin
{
    public class UserService : IUserService
    {
        #region Property
        private readonly CFCDbContext _cFCDbContext;
        private readonly IMapper _mapper;
       
        #endregion

        #region Constructor
        public UserService(CFCDbContext cFCDbContext, IMapper mapper)
        {
            _cFCDbContext = cFCDbContext;
            _mapper = mapper;
            
        }
        #endregion

        #region Get User Roles
        /// <summary>
        /// Get User Roles from Db
        /// </summary>
        /// <returns></returns>
        public async Task<List<RolesModel>> GetUserRolesAsync()
        {
            try
            {
                var userRoles = await _cFCDbContext.userRoles.Where(c => c.IsActive.Equals(true)).ToListAsync();
                return _mapper.Map<List<RolesModel>>(userRoles);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Create User
        /// <summary>
        /// Create user to Db
        /// </summary>
        /// <param name="userModel"></param>
        /// <returns></returns>
        public async Task<string> CreateUserAsync(UserModel userModel)
        {
            try
            {
                var userExists = await _cFCDbContext.users.Where(c => c.Email.Equals(userModel.Email)).AnyAsync();
                if (userExists is false)
                {
                    User res = new User
                    {
                        FirstName = userModel.FirstName,
                        LastName = userModel.LastName,
                        PhoneNumber = userModel.PhoneNumber,
                        Password = userModel.Password,
                        Email = userModel.Email,
                        RoleId = await _cFCDbContext.userRoles.Where(c => c.IsActive.Equals(true) && c.RoleName.Equals(userModel.RoleName)).Select(x => x.RoleId).FirstOrDefaultAsync(),
                        IsActive = true,
                        CreatedDate = DateTime.UtcNow,
                        ModifiedDate = DateTime.UtcNow
                    };
                    await _cFCDbContext.users.AddAsync(res);
                    await _cFCDbContext.SaveChangesAsync();
                    return "User Created Success";
                }
                else return "Email already exists";

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region User Login
        public async Task<bool> UserLoginAsync(LoginModel loginModel)
        {
            try
            {
                var response = await _cFCDbContext.users.Where(c => c.Email.Equals(loginModel.UserName) && c.Password.Equals(loginModel.Password) && c.IsActive.Equals(true)).AnyAsync();
                if (response is true) return true; else return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

    }
}
