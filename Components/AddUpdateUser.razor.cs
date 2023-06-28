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
        private string _title = "Add User";
        private string _message = string.Empty;
        private bool _isUpdating;
        private string _password = string.Empty;
        private User _user = new();
        [Inject]
        private CustomUserManager UserManager { get; set; } = null!;
        [Inject]
        private NavigationManager NavigationManager { get; set; } = null!;
        [Parameter]
        public string Id { get; set; } = null!;

        private async Task SaveAsync()
        {
            if (_password.Trim() != string.Empty)
            {
                await UserManager.AddPasswordAsync(_user, _password);
            }
            var result = await UserManager.CreateAsync(_user);
            if (result.Succeeded)
            {
                if (await IsUpdating())
                {
                    _message = "User updated successfully";
                    NavigationManager.NavigateTo("/users");
                }
                else
                    _message = "User added successfully";
                _user = new();
                _password = string.Empty;
            }
            else
            {
                _message = "Failed to add/update user";
            }
        }
        private async Task<bool> IsUpdating()
        {
            return await UserManager.Users.FirstOrDefaultAsync(x => x.Id == Id) != null;
        }
        protected override async Task OnInitializedAsync()
        {
            _isUpdating = await IsUpdating();
            if (await IsUpdating())
            {
                _title = "Update User";
                _user = await UserManager.Users.FirstOrDefaultAsync(x => x.Id == Id);
            }
            await base.OnInitializedAsync();
        }
    }
}