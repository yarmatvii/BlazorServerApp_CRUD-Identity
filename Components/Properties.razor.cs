using FirstProject.Models;
using FirstProject.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace FirstProject.Components
{
    public class PropertiesComponent : ComponentBase
    {
        [Inject]
        private ICRUDService<Property> PropertyService { get; set; } = null!;
        [Inject]
        private IJSRuntime JSRuntime { get; set; } = null!;
        protected List<Property> Properties { get; private set; } = new();

        protected async Task DeleteAsync(Property property)
        {
            bool confirmed = await JSRuntime.InvokeAsync<bool>("confirm", $"Are you sure you want to delete {property.Name}?");
            if (confirmed)
            {
                try
                {
                    await PropertyService.DeleteAsync(property.Id);
                }
                catch (Exception ex)
                {
                    await JSRuntime.InvokeVoidAsync("alert", ex.Message);
                    return;
                }
                Properties.Remove(property);
            }
        }
        protected override async Task OnInitializedAsync()
        {
            Properties = await PropertyService.GetAllAsync();
            await base.OnInitializedAsync();
        }
    }
}