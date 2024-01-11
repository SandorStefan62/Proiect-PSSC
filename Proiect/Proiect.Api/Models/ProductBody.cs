using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace Proiect.Api.Models
{
    public class ProductBody
    {
        public string Code { get; set; }
        public int Stock { get; set; }
        public decimal Price { get; set; }
    }
}
