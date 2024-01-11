using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Proiect.Domain.Models.ShoppingCart;
using static Proiect.Domain.Operations.ShoppingCartOperations;

namespace Proiect.Domain.Workflows
{
    public class FinishOrderWorkflow
    {
        public IShoppingCart Execute(IShoppingCart shoppingCart)
        {

            shoppingCart = OrderShoppingCart(shoppingCart);

            switch (shoppingCart)
            {
                case PaidShoppingCart paidShoppingCart: return paidShoppingCart;
                case CalculatedShoppingCart:
                case ValidShoppingCart:
                case EmptyShoppingCart:
                case UnvalidatedShoppingCart:
                case InvalidShoppingCart:   
                default: throw new ArgumentException("Unknown shopping cart type");
            }

        }
    }
}
