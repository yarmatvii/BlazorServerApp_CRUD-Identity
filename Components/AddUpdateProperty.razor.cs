using FirstProject.Extensions;
using FirstProject.Models;
using FirstProject.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using Microsoft.JSInterop;

namespace FirstProject.Components
{
    public class AddUpdatePropertyComponent : ComponentBase
    {
        [Inject]
        private ICRUDService<Property> _propertyService { get; set; } = null!;
        [Inject]
        private NavigationManager _navigationManager { get; set; } = null!;
        [Inject]
        private CustomUserManager _userManager { get; set; } = null!;
        [Inject]
        private IJSRuntime _jsRuntime { get; set; } = null!;
        protected string Title = "Add Property";
        protected string Message = string.Empty;
        protected List<User> Users { get; private set; } = new();
        protected Property Property { get; private set; } = new();
        [Parameter]
        public int Id { get; set; }

        protected async Task SaveAsync()
        {
            try
            {
                _ = await _propertyService.AddUpdateAsync(Property);
                if (Id > 0)
                {
                    Message = "Property updated successfully";
                    _navigationManager.NavigateTo("/");
                }
                else
                {
                    Message = "Property added successfully";
                }

                Property = new();
            }
            catch
            {
                Message = "Failed to add/update property";
            }
        }
        protected override async Task OnInitializedAsync()
        {
            try
            {
                if (Id > 0)
                {
                    Title = "Update Property";
                    Property = await _propertyService.GetAsync(Id);
                }
            }
            catch
            {
                _ = await _jsRuntime.InvokeAsync<object>("alert", "Property does not exist.");
                _navigationManager.NavigateTo("/property/add");
            }

            Users = await _userManager.Users.ToListAsync();
            await base.OnInitializedAsync();
        }
    }
}