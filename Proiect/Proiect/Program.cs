using domain.models;
using static domain.models.ShoppingCart;
using static domain.operations.ShoppingCartOperations;

namespace Proiect
{
    class Program
    {
        static void Main(string[] args)
        {
            /*List<UnvalidatedProduct> products = AvailableProducts.LoadProducts();
            List<Product> checkProducts;
            Contact contact = new Contact("asdas", "belfast", "1234567890", "asaada");
            IShoppingCart shoppingCart = new EmptyShoppingCart(contact);
            products.ForEach(product => { shoppingCart = AddProductToShoppingCart(shoppingCart, product); });
            checkProducts = GetShoppingCartItems(shoppingCart);
            checkProducts.ForEach(product => { Console.WriteLine(product.ToString()); });
            UnvalidatedProduct newProduct = new UnvalidatedProduct("asdasd", "11", "22");
            shoppingCart = AddProductToShoppingCart(shoppingCart, newProduct);
            checkProducts = GetShoppingCartItems(shoppingCart);
            checkProducts.ForEach(product => { Console.WriteLine(product.ToString()); });
            shoppingCart = RemoveProductFromShoppingCart(shoppingCart, "asdasd");
            checkProducts = GetShoppingCartItems(shoppingCart);
            checkProducts.ForEach(product => { Console.WriteLine(product.ToString()); });
            shoppingCart = ValidateShoppingCart((UnvalidatedShoppingCart)shoppingCart);
            shoppingCart = CalculateShoppingCart(shoppingCart);

            switch (shoppingCart)
            {
                case CalculatedShoppingCart calculatedCart:
                    Console.WriteLine("Final Validated and Calculated Shopping Cart:");
                    calculatedCart.ValidatedProducts.ForEach(p => { Console.WriteLine(p.ToString()); });
                    Console.WriteLine($"Total Price: {calculatedCart.FinalPrice}");
                    break;
                case InvalidShoppingCart invalidCart:
                    Console.WriteLine("Invalid Shopping Cart:");
                    invalidCart.InvalidProducts.ForEach(p => { Console.WriteLine(p.ToString()); });
                    Console.WriteLine($"Reason for invalidation: {invalidCart.Reason}");
                    break;
                default:
                    Console.WriteLine("Unknown or unchanged shopping cart state");
                    break;
            }*/

            AvailableProducts availableProducts = new AvailableProducts();
            availableProducts.CheckProducts();
            availableProducts.Products.ForEach(product => { Console.WriteLine(product.Quantity.GetType()); });
            UnvalidatedProduct product = availableProducts.OrderProduct("Product 1", 10);
            availableProducts.CheckProducts();
            Console.WriteLine(product.ToString());
        }
    }
}