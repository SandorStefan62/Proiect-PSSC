using LanguageExt.ClassInstances.Pred;
using Proiect.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Proiect.Domain.Models.Events.CartEvent;
using static Proiect.Domain.Models.ShoppingCart;
using static Proiect.Domain.Operations.ShoppingCartOperations;

namespace Proiect.Domain.Workflows
{
    public class ValidationWorkflow
    {
        /* UnvalidatedCart/EmptyCart -> ValidatedCart -> Calculate total price */

        public ICartEvent Execute(CartCommand command)
        {    
            var Cart = ValidateShoppingCart(command.InputCart);
            Cart = CalculateShoppingCart(Cart);

            switch (Cart)
            {
                case ValidShoppingCart validShoppingCart: return new CartFailedEvent("Failed to calculate shopping cart.");
                case EmptyShoppingCart: return new CartFailedEvent("Failed to calculate shopping cart.");
                case UnvalidatedShoppingCart: return new CartFailedEvent("Failed to calculate shopping cart.");
                case InvalidShoppingCart: return new CartFailedEvent("Failed to calculate shopping cart.");
                case PaidShoppingCart: return new CartFailedEvent("Failed to calculate shopping cart.");
                case CalculatedShoppingCart calculatedShoppingCart: return new CartScucceededEvent(calculatedShoppingCart.ValidatedProducts, calculatedShoppingCart.FinalPrice);
                default: throw new ArgumentException("Unknown shopping cart type");
            }

        }


    }
}
