using Proiect.Domain.Models;
using static Proiect.Domain.Models.ShoppingCart;

namespace Proiect.Domain.Operations
{
    public static class ShoppingCartOperations
    {
        public static IShoppingCart ValidateShoppingCart(UnvalidatedShoppingCart unvalidatedShoppingCart)
        {
            List<ValidatedProduct> validatedProducts = new List<ValidatedProduct>();
            Contact? contact = null;
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
        public static List<Product> GetShoppingCartItems(IShoppingCart shoppingCart)
        {
            switch (shoppingCart)
            {
                case ValidShoppingCart validShoppingCart:
                    return validShoppingCart.ValidatedProducts
                        .Select(validProduct => new Product(validProduct.Code, validProduct.Quantity, validProduct.Price))
                        .ToList();
                case EmptyShoppingCart:
                    return new List<Product>();
                case UnvalidatedShoppingCart unvalidatedShoppingCart:
                    return unvalidatedShoppingCart.UnvalidatedProducts
                        .Select(unvalidatedProduct => new Product(unvalidatedProduct.Code, unvalidatedProduct.Quantity, unvalidatedProduct.Price))
                        .ToList();
                case InvalidShoppingCart invalidShoppingCart:
                    return invalidShoppingCart.InvalidProducts
                        .Select(invalidProduct => new Product(invalidProduct.Code, invalidProduct.Quantity, invalidProduct.Price))
                        .ToList();
                case PaidShoppingCart paidShoppingCart:
                    return paidShoppingCart.ValidatedProducts
                        .Select(validProduct => new Product(validProduct.Code, validProduct.Quantity, validProduct.Price))
                        .ToList();
                case CalculatedShoppingCart calculatedShoppingCart:
                    return calculatedShoppingCart.ValidatedProducts
                        .Select(validProduct => new Product(validProduct.Code, validProduct.Quantity, validProduct.Price))
                        .ToList();
                default:
                    throw new ArgumentException("Unknown shopping cart type");
            }
        }
        public static IShoppingCart AddProductToShoppingCart(IShoppingCart shoppingCart, UnvalidatedProduct product)
        {
            switch (shoppingCart)
            {
                case ValidShoppingCart:
                    return shoppingCart;
                case EmptyShoppingCart emptyShoppingCart:
                    List<UnvalidatedProduct> emptyProducts = new List<UnvalidatedProduct>(emptyShoppingCart.UnvalidatedProducts);
                    emptyProducts.Add(product);
                    return new UnvalidatedShoppingCart(emptyProducts, emptyShoppingCart.Contact);
                case UnvalidatedShoppingCart unvalidatedShoppingCart:
                    unvalidatedShoppingCart.UnvalidatedProducts.Add(product);
                    return unvalidatedShoppingCart;
                case InvalidShoppingCart:
                case PaidShoppingCart:
                case CalculatedShoppingCart:
                    return shoppingCart;
                default:
                    throw new ArgumentException("Unknown shopping cart type");
            }
        }
        public static IShoppingCart RemoveProductFromShoppingCart(IShoppingCart shoppingCart, string productCode)
        {
            switch (shoppingCart)
            {
                case ValidShoppingCart:
                case EmptyShoppingCart:
                    return shoppingCart;
                case UnvalidatedShoppingCart unvalidatedShoppingCart:
                    List<UnvalidatedProduct> unvalidatedProducts = unvalidatedShoppingCart.UnvalidatedProducts
                        .Where(unvalidatedProduct => unvalidatedProduct.Code != productCode)
                        .ToList();
                    return new UnvalidatedShoppingCart(unvalidatedProducts, unvalidatedShoppingCart.Contact);
                case InvalidShoppingCart:
                case PaidShoppingCart:
                case CalculatedShoppingCart:
                    return shoppingCart;
                default:
                    throw new ArgumentException("Unknown shopping cart type");
            }
        }
        public static IShoppingCart CalculateShoppingCart(IShoppingCart shoppingCart)
        {
            switch (shoppingCart)
            {
                case ValidShoppingCart validShoppingCart:
                    double finalPrice = 0.0;
                    foreach (ValidatedProduct product in validShoppingCart.ValidatedProducts)
                    {
                        double productPrice = ((Price.MonetaryUnits)product.Price).number;
                        int productQuantity = ((Quantity.Units)product.Quantity).number;
                        finalPrice += productPrice * productQuantity;
                    }
                    return new CalculatedShoppingCart(validShoppingCart.ValidatedProducts, validShoppingCart.Contact, finalPrice);
                case EmptyShoppingCart:
                case UnvalidatedShoppingCart:
                case InvalidShoppingCart:
                case PaidShoppingCart:
                case CalculatedShoppingCart:
                    return shoppingCart;
                default:
                    throw new ArgumentException("Unknown shopping cart type");
            }
        }
        public static IShoppingCart OrderShoppingCart(IShoppingCart shoppingCart)
        {
            switch (shoppingCart)
            {
                case ValidShoppingCart validShoppingCart:
                    IShoppingCart calculatedShoppingCartVar = CalculateShoppingCart(validShoppingCart);
                    if(calculatedShoppingCartVar is CalculatedShoppingCart calculated)
                    {
                        DateTime checkoutDateValidCase = DateTime.Now;
                        return new PaidShoppingCart(calculated.ValidatedProducts, calculated.Contact, calculated.FinalPrice, checkoutDateValidCase);
                    }
                    else
                    {
                        throw new InvalidOperationException("Failed to calculate shopping cart.");
                    }
                case EmptyShoppingCart:
                case UnvalidatedShoppingCart:
                case InvalidShoppingCart:
                case PaidShoppingCart:
                    return shoppingCart;
                case CalculatedShoppingCart calculatedShoppingCart:
                    DateTime checkoutDateCalculatedCase = DateTime.Now;
                    return new PaidShoppingCart(calculatedShoppingCart.ValidatedProducts, calculatedShoppingCart.Contact, calculatedShoppingCart.FinalPrice, checkoutDateCalculatedCase);
                default: throw new ArgumentException("Unknown shopping cart type");
            }
        }
    }
}