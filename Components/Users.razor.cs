using FirstProject.Extensions;
using FirstProject.Models;
using FirstProject.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.JSInterop;

namespace FirstProject.Components
{
    public class UsersComponent : ComponentBase
    {
        [Inject]
        private CustomUserManager UserManager { get; set; } = null!;
        [Inject]
        private IJSRuntime JSRuntime { get; set; } = null!;
        protected List<User> Users { get; private set; } = new();

        protected async Task DeleteAsync(User user)
        {
            bool confirmed = await JSRuntime.InvokeAsync<bool>("confirm", $"Are you sure you want to delete {user.UserName}?");
            if (confirmed)
            {
                try
                {
                    await UserManager.DeleteAsync(user);
                }
                catch (Exception ex)
                {
                    await JSRuntime.InvokeVoidAsync("alert", ex.Message);
                    return;
                }
                Users.Remove(user);
            }
        }
        protected override async Task OnInitializedAsync()
        {
            Users = await UserManager.Users.ToListAsync();
            await base.OnInitializedAsync();
        }
    }
}
