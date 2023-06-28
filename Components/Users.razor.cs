using FirstProject.Models;
using FirstProject.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.JSInterop;

namespace FirstProject.Components
{
    public partial class Users
    {
        //private UserManager<User> _userManager;
        //private ICRUDService<User> _userService;
        //private IJSRuntime _jsRuntime;
        //public Users(UserManager<User> userManager, ICRUDService<User> userService, IJSRuntime jsRuntime)
        //{
        //    _userManager = userManager;
        //    _userService = userService;
        //    _jsRuntime = jsRuntime;
        //}
        private List<User> users = new();
        private async Task Delete(User user)
        {
            bool confirmed = await JSRuntime.InvokeAsync<bool>("confirm", $"Are you sure you want to delete {user.UserName}?");
            if (confirmed)
            {
                await UserManager.DeleteAsync(user);
                users.Remove(user);
            }
        }
        protected override void OnInitialized()
        {
            users = UserManager.Users.ToList();
            base.OnInitialized();
        }
    }
}
