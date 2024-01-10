using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Proiect.Domain.Models.Price;
using static Proiect.Domain.Models.Quantity;

namespace Proiect.Domain.Models
{
    public record ValidatedProduct(string Code, IQuantity Quantity, IPrice Price)
    {
        public int OrderId { get; set; }
    }
}
