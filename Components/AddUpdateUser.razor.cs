using FirstProject.Extensions;
using FirstProject.Models;
using FirstProject.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.JSInterop;
using System.Data;

namespace FirstProject.Components
{
    public class AddUpdateUserComponent : ComponentBase
    {
        [Inject]
        private CustomUserManager _userManager { get; set; } = null!;
        [Inject]
        private NavigationManager _navigationManager { get; set; } = null!;
        [Inject]
        private RoleManager<IdentityRole> _roleManager { get; set; } = null!;
        protected string Password = string.Empty;
        protected string SelectedRoleName { get; set; }
        protected bool IsUpdating;
        protected string Title = "Add User";
        protected string Message = string.Empty;
        protected User User { get; private set; } = new();
        protected List<IdentityRole> Roles { get; private set; } = new();
        [Parameter]
        public string Id { get; set; } = null!;

        protected async Task SaveAsync()
        {
            if (!await CheckIsUpdating() && Password.Trim() != string.Empty)
            {
                await _userManager.AddPasswordAsync(User, Password);
            }
            if (await CheckIsUpdating())
            {
                await _userManager.RemoveFromRolesAsync(User, await _userManager.GetRolesAsync(User));
            }

            var result = await _userManager.CreateAsync(User);
            await _userManager.AddToRoleAsync(User, SelectedRoleName);
            if (result.Succeeded)
            {
                if (await CheckIsUpdating())
                {
                    Message = "User updated successfully";
                    _navigationManager.NavigateTo("/users");
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
            return await _userManager.Users.FirstOrDefaultAsync(x => x.Id == Id) != null;
        }
        protected override async Task OnInitializedAsync()
        {
            IsUpdating = await CheckIsUpdating();
            if (await CheckIsUpdating())
            {
                Title = "Update User";
                User = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == Id);
            }

            Roles = await _roleManager.Roles.ToListAsync();
            await base.OnInitializedAsync();
        }
    }
}