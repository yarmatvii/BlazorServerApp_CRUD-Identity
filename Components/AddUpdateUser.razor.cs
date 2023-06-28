using FirstProject.Extensions;
using FirstProject.Models;
using FirstProject.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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
        private async void Save()
        {
            if (password.Trim() != string.Empty)
                await UserManager.AddPasswordAsync(user, password);
            var result = await UserManager.CreateAsync(user);
            if (result.Succeeded)
            {
                if (IfUpdating())
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
        protected override async Task OnInitializedAsync()
        {
            if (UserManager.Users.FirstOrDefault(x => x.Id == Id) != null)
            {
                Title = "Update User";
                user = await UserManager.Users.FirstOrDefaultAsync(x => x.Id == Id);
            }
            await base.OnInitializedAsync();
        }
        public bool IfUpdating()
        {
            if (UserManager.Users.FirstOrDefault(x => x.Id == Id) != null)
                return true;
            else
                return false;
        }
    }
}