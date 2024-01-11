using Proiect.Domain.Models;
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
    public record SenderCartCommand
    {
        public SenderCartCommand(PaidShoppingCart paidShoppingCart)
        {
            InputCart = paidShoppingCart;
        }

        public PaidShoppingCart InputCart { get; }

    }
    public record InitialCartCommand
    {
        private EmptyShoppingCart? emptyShoppingCart;

        public InitialCartCommand(EmptyShoppingCart? emptyShoppingCart)
        {
            this.emptyShoppingCart = emptyShoppingCart;
        }

        public InitialCartCommand(EmptyShoppingCart emptyShoppingCart, List<UnvalidatedProduct> orderedProducts)
        {
            InputCart = emptyShoppingCart;
            OrderedProducts = orderedProducts;
        }

        public EmptyShoppingCart InputCart { get; }
        public List<UnvalidatedProduct> OrderedProducts { get; }
    }

    public record AditionalCartCommand
    {
        public AditionalCartCommand(UnvalidatedShoppingCart unvalidatedShoppingCart, List<UnvalidatedProduct> orderedProducts)
        {
            InputCart = unvalidatedShoppingCart;
            OrderedProducts = orderedProducts;
        }

        public UnvalidatedShoppingCart InputCart { get; }
        public List<UnvalidatedProduct> OrderedProducts { get; }
    }
}
