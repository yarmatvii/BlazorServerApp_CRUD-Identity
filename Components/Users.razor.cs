using FirstProject.Extensions;
using FirstProject.Models;
using FirstProject.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.JSInterop;

namespace FirstProject.Components
{
    public partial class Users
    {
        private List<User> _users = new();
        [Inject]
        private CustomUserManager UserManager { get; set; } = null!;
        [Inject]
        private IJSRuntime JSRuntime { get; set; } = null!;

        private async Task DeleteAsync(User user)
        {
            bool confirmed = await JSRuntime.InvokeAsync<bool>("confirm", $"Are you sure you want to delete {user.UserName}?");
            if (confirmed)
            {
                await UserManager.DeleteAsync(user);
                _users.Remove(user);
            }
        }
        protected override async Task OnInitializedAsync()
        {
            _users = await UserManager.Users.ToListAsync();
            await base.OnInitializedAsync();
        }
    }
}
