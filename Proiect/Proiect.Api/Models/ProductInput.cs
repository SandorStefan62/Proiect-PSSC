using System.ComponentModel.DataAnnotations;

namespace Proiect.Api.Models
{
    public class ProductInput
    {
        [Required]
        public string Code { get; set; }

        [Required]
        [Range(1, 10)]
        public int Quantity { get; set; }
    }
}
