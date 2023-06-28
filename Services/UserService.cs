using FirstProject.Data;
using FirstProject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FirstProject.Services
{
    public class UserService
    {
        private readonly UserManager<User> _userManager;
        public UserService(UserManager<User> userManager)
        {
            _userManager = userManager;
        }
        public bool AddUpdate(User user)
        {
            try
            {
                user.NormalizedEmail = _userManager.NormalizeEmail(user.Email);
                user.NormalizedUserName = _userManager.NormalizeName(user.UserName);

                if (_userManager.Users.FirstOrDefault(x => x.Id == user.Id) == null)
                {
                    _userManager.CreateAsync(user).Wait();
                }
                else
                {
                    _userManager.UpdateAsync(user).Wait();
                }
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
