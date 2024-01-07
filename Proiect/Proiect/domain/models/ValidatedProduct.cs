using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static domain.models.Quantity;
using static domain.models.Price;

namespace domain.models
{
    public record ValidatedProduct(string Code, IQuantity Quantity, IPrice Price);
}
