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
    public class SendOrderWorkflow
    {
        /* Validate Address -> Send command */

        public ICartEvent SenderExecute(SenderCartCommand command)
        {
            var Cart = OrderShoppingCart(command.InputCart);

            switch (Cart)
            {
                case ValidShoppingCart: return new CartFailedEvent("Failed to send order");
                case EmptyShoppingCart: return new CartFailedEvent("Failed to send order");
                case UnvalidatedShoppingCart: return new CartFailedEvent("Failed to send order");
                case InvalidShoppingCart: return new CartFailedEvent("Failed to send order");
                case PaidShoppingCart paidShoppingCart: return new SenderCartScucceededEvent(paidShoppingCart.ValidatedProducts, paidShoppingCart.Contact, paidShoppingCart.FinalPrice, paidShoppingCart.CheckoutDate);
                case CalculatedShoppingCart: return new CartFailedEvent("Failed to send order");
                default: throw new ArgumentException("Unknown shopping cart type");
            }

        }
    }
}
