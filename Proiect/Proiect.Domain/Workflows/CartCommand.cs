using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Proiect.Domain.Models.ShoppingCart;

namespace Proiect.Domain.Workflows
{

    public record CartCommand
    {
        public CartCommand(UnvalidatedShoppingCart unvalidatedShoppingCart)
        {
            InputCart = unvalidatedShoppingCart;
        }

        public UnvalidatedShoppingCart InputCart { get; }
    }

}
