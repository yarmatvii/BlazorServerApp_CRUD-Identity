using FirstProject.Models;
using FirstProject.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace FirstProject.Components
{
    public partial class Properties
    {
        private List<Property> _properties = new();
        [Inject]
        private ICRUDService<Property> PropertyService { get; set; } = null!;
        [Inject]
        private IJSRuntime JSRuntime { get; set; } = null!;

        private async Task DeleteAsync(Property property)
        {
            bool confirmed = await JSRuntime.InvokeAsync<bool>("confirm", $"Are you sure you want to delete {property.Name}?");
            if (confirmed)
            {
                await PropertyService.DeleteAsync(property.Id);
                _properties.Remove(property);
            }
        }
        protected override async Task OnInitializedAsync()
        {
            _properties = await PropertyService.GetAllAsync();
            await base.OnInitializedAsync();
        }
    }
}