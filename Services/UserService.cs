using FirstProject.Data;
using FirstProject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FirstProject.Services
{
    public class UserService : ICRUDService<User>
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;
        public UserService(ApplicationDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public bool AddUpdate(User user)
        {
            try
            {
                user.NormalizedEmail = _userManager.NormalizeEmail(user.Email);
                user.NormalizedUserName = _userManager.NormalizeName(user.UserName);

                //if (_userManager.FindByIdAsync(user.Id).Result == null)
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

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public User? Get(int id)
        {
            throw new NotImplementedException();
        }

        public List<User> GetAll()
        {
            return _context.Users
                .Include(u => u.Properties)
                .ToList();
        }
    }
}
