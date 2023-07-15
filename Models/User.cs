using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace FirstProject.Models
{
    public class User : IdentityUser
    {
        public string? Address { get; set; }
        [Required]
        public string FirstName { get; set; } = null!;
        [Required]
        public string LastName { get; set; } = null!;
        public float InitialPropertyCost => Properties?.Sum(p => p.InitialCost) ?? 0f;
        public float CurrentPropertyCost => Properties?.Sum(p => p.CurrentCost) ?? 0f;
        public List<Property> Properties { get; set; } = new List<Property>();
    }
}
