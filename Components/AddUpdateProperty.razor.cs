using FirstProject.Extensions;
using FirstProject.Models;
using FirstProject.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.JSInterop;

namespace FirstProject.Components
{
    public class AddUpdatePropertyComponent : ComponentBase
    {
        [Inject]
        private ICRUDService<Property> PropertyService { get; set; } = null!;
        [Inject]
        private NavigationManager NavigationManager { get; set; } = null!;
        [Inject]
        private CustomUserManager UserManager { get; set; } = null!;
        [Inject]
        private IJSRuntime JSRuntime { get; set; } = null!;
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
                await PropertyService.AddUpdateAsync(Property);
                if (Id > 0)
                {
                    Message = "Property updated successfully";
                    NavigationManager.NavigateTo("/");
                }
                else
                    Message = "Property added successfully";
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
                    Property = await PropertyService.GetAsync(Id);
                }
            }
            catch
            {
                await JSRuntime.InvokeAsync<object>("alert", "Property does not exist.");
                NavigationManager.NavigateTo("/property/add");
            }
            Users = await UserManager.Users.ToListAsync();
            await base.OnInitializedAsync();
        }
    }
}