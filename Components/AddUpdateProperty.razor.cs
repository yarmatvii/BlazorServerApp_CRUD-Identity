using FirstProject.Models;
using FirstProject.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using Microsoft.JSInterop;

namespace FirstProject.Components
{
    public partial class AddUpdateProperty
    {
        //private UserManager<User> _userManager;
        //private ICRUDService<Property> _propertyService;
        //private NavigationManager _navigationManager;
        //public AddUpdateProperty(UserManager<User> userManager, ICRUDService<Property> propertyService, NavigationManager navigationManager)
        //{
        //    _userManager = userManager;
        //    _propertyService = propertyService;
        //    _navigationManager = navigationManager;
        //}
        [Parameter]
        public int Id { get; set; }
        private string message = string.Empty;
        Property property = new();
        private string Title = "Add Property";

        private void Save()
        {
            if (PropertyService.AddUpdate(property))
            {
                if (Id > 0)
                {
                    message = "Property updated successfully";
                    NavigationManager.NavigateTo("/");
                }
                else
                    message = "Property added successfully";
                property = new();
            }
            else
            {
                message = "Failed to add/update property";
            }
        }
        protected override void OnInitialized()
        {
            if (Id > 0)
            {
                Title = "Update Property";
                property = PropertyService.Get(Id);
            }
            base.OnInitialized();
        }
    }
}