using LanguageExt.ClassInstances.Pred;
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
    public class ValidationWorkflow
    {
        /* UnvalidatedCart/EmptyCart -> ValidatedCart -> Calculate total price */

        public IShoppingCart Execute(Contact contact, List<UnvalidatedProduct> orderedProducts)
        {
            IShoppingCart shoppingCart = new EmptyShoppingCart(contact);

            foreach (UnvalidatedProduct product in orderedProducts)
            {
                shoppingCart = AddProductToShoppingCart(shoppingCart, product);
            }

            shoppingCart = ValidateShoppingCart((UnvalidatedShoppingCart)shoppingCart);

            switch (shoppingCart)
            {
                case ValidShoppingCart validShoppingCart: return validShoppingCart;
                case EmptyShoppingCart:
                case UnvalidatedShoppingCart:
                case InvalidShoppingCart:
                case PaidShoppingCart:
                case CalculatedShoppingCart:
                default: throw new ArgumentException("Unknown shopping cart type");
            }

        }


    }
}
