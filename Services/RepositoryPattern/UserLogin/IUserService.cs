using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using static Services.ViewModels.CommonModel;

namespace Services.RepositoryPattern.UserLogin
{
    public interface IUserService
    {
        Task<List<RolesModel>> GetUserRolesAsync();
        Task<string> CreateUserAsync(UserModel userModel);
        Task<bool> UserLoginAsync(LoginModel loginModel);
    } 
}
