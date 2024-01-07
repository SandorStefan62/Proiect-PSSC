using domain.models;
using static domain.models.ShoppingCart;
using CSharp.Choices;

namespace domain.operations
{
    public static class ShoppingCartOperations
    {
        public static IShoppingCart ValidateShoppingCart(UnvalidatedShoppingCart unvalidatedShoppingCart)
        {
            List<ValidatedProduct> validatedProducts = new List<ValidatedProduct>();
            Contact contact = null;
            bool isValid = true;
            string invalidReason = string.Empty;

            foreach (UnvalidatedProduct unvalidatedProduct in unvalidatedShoppingCart.UnvalidatedProducts)
            {
                if (!Product.TryParse(unvalidatedProduct.Code, unvalidatedProduct.Quantity, unvalidatedProduct.Price, out Product? parsedProduct))
                {
                    invalidReason = $"Invalid product format: [{unvalidatedProduct.Code}, {unvalidatedProduct.Quantity}, {unvalidatedProduct.Price}]";
                    Console.WriteLine(invalidReason);
                    isValid = false;
                    break;
                }
                ValidatedProduct validatedProduct = new ValidatedProduct(parsedProduct.Code, parsedProduct.Quantity, parsedProduct.Price);
                validatedProducts.Add(validatedProduct);
            }
            if(!Contact.TryParse(unvalidatedShoppingCart.Contact.FirstName, unvalidatedShoppingCart.Contact.LastName, unvalidatedShoppingCart.Contact.TelephoneNumber, unvalidatedShoppingCart.Contact.Address, out Contact? result))
            {
                invalidReason = $"Invalid contact format: [{unvalidatedShoppingCart.Contact.FirstName}, {unvalidatedShoppingCart.Contact.LastName}, {unvalidatedShoppingCart.Contact.TelephoneNumber}, {unvalidatedShoppingCart.Contact.Address}]";
                Console.WriteLine(invalidReason);
                isValid = false;
            }
            contact = result;
            if (isValid)
            {
                return new ValidShoppingCart(validatedProducts, contact);
            }
            else
            {
                return new InvalidShoppingCart(unvalidatedShoppingCart.UnvalidatedProducts, unvalidatedShoppingCart.Contact, invalidReason);
            }
        }
        /*public static IShoppingCart CalculateShoppingCart(IShoppingCart shoppingCart)
        {
            var result = shoppingCart.Match(
                emptyShoppingCart => emptyShoppingCart,
                unvalidatedShoppingCart => unvalidatedShoppingCart,
                invalidShoppingCart => invalidShoppingCart,
                validShoppingCart =>
                {
                    double finalPrice = 0.0;
                    foreach(ValidatedProduct product in validShoppingCart.ValidatedProducts)
                    {
                        double productPrice = ((Price.MonetaryUnits)product.Price).number;
                        int productQuantity = ((Quantity.Units)product.Quantity).number;
                        finalPrice += productPrice * productQuantity;
                    }
                    return new CalculatedShoppingCart(validShoppingCart.Products, validShoppingCart.Contact, finalPrice);
                },
                paidShoppingCart => paidShoppingCart,
                calculatedShoppingCart => calculatedShoppingCart
            );
            return result;
        }*/
        public static IShoppingCart CalculateShoppingCart(IShoppingCart shoppingCart)
        {
            if(shoppingCart is ValidShoppingCart validShoppingCart)
            {
                double finalPrice = 0.0;
                foreach(ValidatedProduct product in validShoppingCart.ValidatedProducts)
                {
                    double productPrice = ((Price.MonetaryUnits)product.Price).number;
                    int productQuantity = ((Quantity.Units)product.Quantity).number;
                    finalPrice += productPrice * productQuantity;
                }
                return new CalculatedShoppingCart(validShoppingCart.ValidatedProducts, validShoppingCart.Contact, finalPrice);
            }
            else if (shoppingCart is EmptyShoppingCart)
            {
                return shoppingCart;
            }
            else if (shoppingCart is UnvalidatedShoppingCart)
            {
                return shoppingCart;
            }
            else if (shoppingCart is InvalidShoppingCart invalidShoppingCart)
            {
                return invalidShoppingCart;
            }
            else if (shoppingCart is PaidShoppingCart)
            {
                return shoppingCart;
            }
            else if (shoppingCart is CalculatedShoppingCart)
            {
                return shoppingCart;
            }
            else
            {
                throw new ArgumentException("Unknown shopping cart type");
            }
        }
    }
}