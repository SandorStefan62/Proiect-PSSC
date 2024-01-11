using LanguageExt.ClassInstances;
using Proiect.Domain.Models;
using System.Collections.Generic;
using static Proiect.Domain.Models.Events.CartEvent;
using static Proiect.Domain.Models.ShoppingCart;
using static Proiect.Domain.Operations.ShoppingCartOperations;

namespace Proiect.Domain.Workflows
{
    public class GetCartItemsWorkflow
    {
        /* Get add items to Cart */

        public ICartEvent InitialExecute(InitialCartCommand command, List<UnvalidatedProduct> orderedProducts)
        {

            var Cart = (IShoppingCart)GetShoppingCartItems(command.InputCart);

            foreach (var product in orderedProducts)
            {
                Cart = AddProductToShoppingCart(Cart, product);
                Cart = RemoveProductFromShoppingCart(Cart, "Telefon");
            }

            switch (Cart)
            {
                case ValidShoppingCart: return new CartFailedEvent("Failed to add items to cart.");
                case EmptyShoppingCart emptyShoppingCart: return new InitialCartScucceededEvent(emptyShoppingCart.UnvalidatedProducts, emptyShoppingCart.Contact);
                case UnvalidatedShoppingCart unvalidatedShoppingCart: return new AditionalCartScucceededEvent(unvalidatedShoppingCart.UnvalidatedProducts);
                case InvalidShoppingCart: return new CartFailedEvent("Failed to add items to cart.");
                case PaidShoppingCart: return new CartFailedEvent("Failed to add items to cart.");
                case CalculatedShoppingCart: return new CartFailedEvent("Failed to add items to cart.");
                default: throw new ArgumentException("Unknown shopping cart type");
            }
        }
    }
}
