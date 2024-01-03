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
        public string Code { get; init; }
        public IQuantity Quantity { get; init; }
        public Product(string code, IQuantity quantity)
        {
            this.Code = code;
            this.Quantity = quantity;
        }
        public override string ToString()
        {
            return $"[Code: {this.Code} Quantity: {this.Quantity}]";
        }
    }
}
