using FirstProject.Models;
using FirstProject.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using Microsoft.JSInterop;

namespace FirstProject.Components
{
    public partial class AddUpdateUser
    {
        //private UserManager<User> _userManager;
        //private ICRUDService<User> _userService;
        //private NavigationManager _navigationManager;
        //public AddUpdateUser(UserManager<User> userManager, ICRUDService<User> userService, NavigationManager navigationManager)
        //{
        //    _userManager = userManager;
        //    _userService = userService;
        //    _navigationManager = navigationManager;
        //}
        [Parameter]
        public string Id { get; set; }
        private string message = string.Empty;
        User user = new();
        private string Title = "Add User";
        private string password = string.Empty;
        private void Save()
        {
            UserManager.AddPasswordAsync(user, password).Wait();
            if (AddUpdate(user))
            {
                if (UserManager.Users.FirstOrDefault(x => x.Id == Id) != null)
                {
                    message = "User updated successfully";
                    NavigationManager.NavigateTo("/users");
                }
                else
                    message = "User added successfully";
                user = new();
                password = string.Empty;
            }
            else
            {
                message = "Failed to add/update user";
            }
        }
        protected override void OnInitialized()
        {
            if (UserManager.Users.FirstOrDefault(x => x.Id == Id) != null)
            {
                Title = "Update User";
                //user = u;
                user = UserManager.Users.FirstOrDefault(x => x.Id == Id);
            }
            base.OnInitializedAsync();
        }

        public bool AddUpdate(User user)
        {
            try
            {
                user.NormalizedEmail = UserManager.NormalizeEmail(user.Email);
                user.NormalizedUserName = UserManager.NormalizeName(user.UserName);

                if (UserManager.Users.FirstOrDefault(x => x.Id == user.Id) == null)
                {
                    UserManager.CreateAsync(user).Wait();
                }
                else
                {
                    UserManager.UpdateAsync(user).Wait();
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