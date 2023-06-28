using FirstProject.Models;
using FirstProject.Services;
using Microsoft.JSInterop;

namespace FirstProject.Components
{
    public partial class Properties
    {
        //private readonly ICRUDService<Property> _propertyService;
        //private readonly IJSRuntime _jsRuntime;
        //public Properties(ICRUDService<Property> propertyService, IJSRuntime jsRuntime)
        //{
        //    _propertyService = propertyService;
        //    _jsRuntime = jsRuntime;
        //}
        private List<Property> properties = new();
        private async Task Delete(Property property)
        {
            bool confirmed = await JSRuntime.InvokeAsync<bool>("confirm", $"Are you sure you want to delete {property.Name}?");
            if (confirmed)
            {
                PropertyService.Delete(property.Id);
                properties.Remove(property);
            }
        }
        protected override void OnInitialized()
        {
            properties = PropertyService.GetAll();
            base.OnInitialized();
        }
    }
}