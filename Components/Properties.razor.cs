using FirstProject.Models;
using FirstProject.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace FirstProject.Components
{
    public class PropertiesComponent : ComponentBase
    {
        [Inject]
        private ICRUDService<Property> _propertyService { get; set; } = null!;
        [Inject]
        private IJSRuntime _jsRuntime { get; set; } = null!;
        protected List<Property> Properties { get; private set; } = new();

        protected async Task DeleteAsync(Property property)
        {
            bool confirmed = await _jsRuntime.InvokeAsync<bool>("confirm", $"Are you sure you want to delete {property.Name}?");
            if (confirmed)
            {
                try
                {
                    await _propertyService.DeleteAsync(property.Id);
                }
                catch (Exception ex)
                {
                    await _jsRuntime.InvokeVoidAsync("alert", ex.Message);
                    return;
                }
                Properties.Remove(property);
            }
        }
        protected override async Task OnInitializedAsync()
        {
            Properties = await _propertyService.GetAllAsync();
            await base.OnInitializedAsync();
        }
    }
}