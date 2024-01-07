using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace domain.models
{
    public record UnvalidatedProduct(string Code, object Quantity, object Price);
}
