using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Proiect.domain.models.Quantity;

namespace Proiect.domain.models
{
    public record class Product
    {
        public string code { get; init; }
        public IQuantity quantity { get; init; }
        public Product(string code, IQuantity quantity)
        {
            this.code = code;
            this.quantity = quantity;
        }
        public override string ToString()
        {
            return $"Code: {this.code} Quantity: {this.quantity}";
        }
    }
}
