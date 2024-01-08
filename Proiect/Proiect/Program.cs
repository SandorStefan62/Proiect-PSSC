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

            //init
            Contact contact = new Contact("asd", "asd", "1111111111", "asd");
            IShoppingCart shoppingCart = new EmptyShoppingCart(contact);
            AvailableProducts availableProducts = new AvailableProducts();
            List<Product> shoppingCartProducts = new List<Product>();

            //checks the available stock
            availableProducts.CheckProducts();
            availableProducts.Products.ForEach(product => { Console.WriteLine(product.Quantity.GetType()); });
            Console.WriteLine("\n\n");
            
            //two products to be ordered
            UnvalidatedProduct product1 = availableProducts.OrderProduct("Product 1", 10);
            UnvalidatedProduct product2 = availableProducts.OrderProduct("Product 2", 5);

            //checks if quantity has been modified successfully
            availableProducts.CheckProducts();
            availableProducts.Products.ForEach(product => { Console.WriteLine(product.Quantity.GetType()); });
            Console.WriteLine("\n\n");
            
            //adds ordered products to shopping cart
            shoppingCart = AddProductToShoppingCart(shoppingCart, product1);
            shoppingCart = AddProductToShoppingCart(shoppingCart, product2);

            //checks items in shopping cart
            shoppingCartProducts = GetShoppingCartItems(shoppingCart);
            shoppingCartProducts.ForEach(product => { Console.WriteLine(product.ToString()); });
            Console.WriteLine("\n\n");

            //removes one item from the shopping cart
            shoppingCart = RemoveProductFromShoppingCart(shoppingCart, "Product 1");

            //checks if item has been removed successfully
            shoppingCartProducts = GetShoppingCartItems(shoppingCart);
            shoppingCartProducts.ForEach(product => { Console.WriteLine(product.ToString()); });
            Console.WriteLine("\n\n");

            //checks type of shopping cart
            Console.WriteLine(shoppingCart.GetType());
            Console.WriteLine("\n\n");

            //validates shopping cart
            shoppingCart = ValidateShoppingCart((UnvalidatedShoppingCart)shoppingCart);
            Console.WriteLine(shoppingCart.GetType());
            Console.WriteLine("\n\n");

            //calculates shopping cart
            shoppingCart = CalculateShoppingCart(shoppingCart);
            Console.WriteLine(shoppingCart.GetType());
            Console.WriteLine("\n\n");

            //orders shopping cart
            shoppingCart = OrderShoppingCart(shoppingCart);
            Console.WriteLine(shoppingCart.GetType());
        }
    }
}