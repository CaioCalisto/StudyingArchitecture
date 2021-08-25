using System.ComponentModel.DataAnnotations;

namespace Contoso.Registration.Services.Api.Commands
{
    public class AddVehicleCommand
    {
        [Required]
        [MinLength(3, ErrorMessage = "Minimum lenght is 3")]
        public string Name { get; set; }

        [Required]
        [MinLength(3, ErrorMessage = "Minimum lenght is 3")]
        public string Brand { get; set; }

        [Required]
        public string Category { get; set; }
    }
}