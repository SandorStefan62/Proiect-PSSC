using Proiect.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Proiect.Domain.Models.ShoppingCart;
using static Proiect.Domain.Operations.ShoppingCartOperations;

namespace Proiect.Domain.Workflows
{
    public class CalculateOrderWorkflow
    {

        public IShoppingCart Execute(IShoppingCart shoppingCart)
        {
           
            shoppingCart = CalculateShoppingCart(shoppingCart);

            switch (shoppingCart)
            {
                case CalculatedShoppingCart calculatedShoppingCart: return calculatedShoppingCart;
                case ValidShoppingCart:
                case EmptyShoppingCart:
                case UnvalidatedShoppingCart:
                case InvalidShoppingCart:
                case PaidShoppingCart:
                default: throw new ArgumentException("Unknown shopping cart type");
            }

        }

    }
}
