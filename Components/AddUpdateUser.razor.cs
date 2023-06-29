using FirstProject.Extensions;
using FirstProject.Models;
using FirstProject.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.JSInterop;

namespace FirstProject.Components
{
    public class AddUpdateUserComponent : ComponentBase
    {
        [Inject]
        private CustomUserManager UserManager { get; set; } = null!;
        [Inject]
        private NavigationManager NavigationManager { get; set; } = null!;
        protected string Password = string.Empty;
        protected bool IsUpdating;
        protected string Title = "Add User";
        protected string Message = string.Empty;
        protected User User { get; private set; } = new();
        [Parameter]
        public string Id { get; set; } = null!;

        protected async Task SaveAsync()
        {
            if (!await CheckIsUpdating() && Password.Trim() != string.Empty)
            {
                await UserManager.AddPasswordAsync(User, Password);
            }
            var result = await UserManager.CreateAsync(User);
            if (result.Succeeded)
            {
                if (await CheckIsUpdating())
                {
                    Message = "User updated successfully";
                    NavigationManager.NavigateTo("/users");
                }
                else
                {
                    Message = "User added successfully";
                }
                User = new();
                Password = string.Empty;
            }
            else
            {
                Message = "Failed to add/update user";
            }
        }
        private async Task<bool> CheckIsUpdating()
        {
            return await UserManager.Users.FirstOrDefaultAsync(x => x.Id == Id) != null;
        }
        protected override async Task OnInitializedAsync()
        {
            IsUpdating = await CheckIsUpdating();
            if (await CheckIsUpdating())
            {
                Title = "Update User";
                User = await UserManager.Users.FirstOrDefaultAsync(x => x.Id == Id);
            }
            await base.OnInitializedAsync();
        }
    }
}