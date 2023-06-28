using FirstProject.Extensions;
using FirstProject.Models;
using FirstProject.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.JSInterop;

namespace FirstProject.Components
{
    public partial class AddUpdateProperty
    {
        private string _title = "Add Property";
        private string _message = string.Empty;
        private List<User> _users = new();
        Property _property = new();
        [Inject]
        private ICRUDService<Property> PropertyService { get; set; } = null!;
        [Inject]
        private NavigationManager NavigationManager { get; set; } = null!;
        [Inject]
        private CustomUserManager UserManager { get; set; } = null!;
        [Parameter]
        public int Id { get; set; }

        private async Task SaveAsync()
        {
            if (await PropertyService.AddUpdateAsync(_property))
            {
                if (Id > 0)
                {
                    _message = "Property updated successfully";
                    NavigationManager.NavigateTo("/");
                }
                else
                    _message = "Property added successfully";
                _property = new();
            }
            else
            {
                _message = "Failed to add/update property";
            }
        }
        protected override async Task OnInitializedAsync()
        {
            if (Id > 0)
            {
                _title = "Update Property";
                _property = await PropertyService.GetAsync(Id);
            }
            _users = await UserManager.Users.ToListAsync();
            await base.OnInitializedAsync();
        }
    }
}