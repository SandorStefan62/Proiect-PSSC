using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proiect.Domain.Models
{
    public record Order
    {
        public Contact Contact { get; init; }
        public List<Product> Products { get; init; } = new List<Product>();
    }
}
