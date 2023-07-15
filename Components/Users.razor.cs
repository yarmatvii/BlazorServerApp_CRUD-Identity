using FirstProject.Extensions;
using FirstProject.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.JSInterop;
using System.Security.Claims;

namespace FirstProject.Components
{
    public class UsersComponent : ComponentBase
    {
        [Inject]
        private IJSRuntime _jsRuntime { get; set; } = null!;
        [Inject]
        private RoleManager<IdentityRole> _roleManager { get; set; } = null!;
        [Inject]
        private CustomUserManager _userManager { get; set; } = null!;
        [Inject]
        private AuthenticationStateProvider _authenticationStateProvider { get; set; } = null!;
        protected string? userId;
        protected Dictionary<string, string> _userRoles = new();
        protected List<User> Users { get; private set; } = new();
        protected List<IdentityRole> Roles { get; private set; } = new();

        protected async Task DeleteAsync(User user)
        {
            bool confirmed = await _jsRuntime.InvokeAsync<bool>("confirm", $"Are you sure you want to delete {user.UserName}?");
            if (confirmed)
            {
                try
                {
                    _ = await _userManager.DeleteAsync(user);
                }
                catch (Exception ex)
                {
                    await _jsRuntime.InvokeVoidAsync("alert", ex.Message);
                    return;
                }
                _ = Users.Remove(user);
            }
        }

        protected override async Task OnInitializedAsync()
        {
            AuthenticationState authenticationState = await _authenticationStateProvider.GetAuthenticationStateAsync();
            ClaimsPrincipal authUser = authenticationState.User;
            userId = authUser.FindFirstValue(ClaimTypes.NameIdentifier);
            User authorizedUser;

            if (authUser.IsInRole("Admin"))
            {
                // User is an Admin, retrieve the full list of users.
                Users = await _userManager.Users.ToListAsync();
            }
            else
            {
                // User is not an Admin, retrieve only their own data.
                authorizedUser = await _userManager.FindByIdAsync(userId);
                if (authorizedUser != null)
                {
                    Users = new List<User> { authorizedUser };
                }
            }

            foreach (User user in Users)
            {
                IList<string> roles = await _userManager.GetRolesAsync(user);
                _userRoles[user.Id] = roles.FirstOrDefault();
            }

            await base.OnInitializedAsync();
        }
    }
}
