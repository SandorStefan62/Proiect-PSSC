using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Proiect.domain.models.Quantity;

namespace Proiect.domain.models
{
    public record ValidatedProduct(string Code, IQuantity Quantity, double Price);
}
