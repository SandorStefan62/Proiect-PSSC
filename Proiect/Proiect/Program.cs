using Proiect.Domain.Models;
using Proiect.Data.Repository;
using Microsoft.EntityFrameworkCore;
using static Proiect.Domain.Models.ShoppingCart;
using static Proiect.Domain.Operations.ShoppingCartOperations;
using Proiect.Domain.Workflows;
using Microsoft.Extensions.Logging;
using Proiect.Data;
using Proiect.Domain.Repository;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using static Proiect.Domain.Models.Events.CartEvent;


namespace Proiect
{
    class Program
    {
        private static string ConnectionString1 = "Integrated Security=true;Server=LAPTOP-DRAGOS\\SQLEXPRESS;Database=master;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=true";
        private static string ConnectionString = "Integrated Security=true;Server=localhost\\SQLEXPRESS;Database=master;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=true";
        static void Main(string[] args)
        {
            //DataBase Initialization
            var dbContextBuilder = new DbContextOptionsBuilder<OrderContext>()
            .UseSqlServer(ConnectionString);
            OrderContext orderContext = new(dbContextBuilder.Options);
            //OrderHeaderRepository orderHeaderRepositroy = new OrderHeaderRepository(orderContext);
            //OrderLineRepository orderLineRepository = new OrderLineRepository(orderContext);
            ProductRepository productRepository = new ProductRepository(orderContext);

            //init
            Contact contact = new Contact("asd", "asd", "1111111111", "asd");
            IShoppingCart shoppingCart = new EmptyShoppingCart(contact);
            AvailableProducts availableProductsDB = new AvailableProducts(productRepository.TryGetAllProducts().Result);
            AvailableProducts availableProducts = new AvailableProducts();
            List<Product> shoppingCartProducts = new List<Product>();

            //checks the available stock
            availableProductsDB.CheckProducts();
            //availableProductsDB.CheckProducts();
            availableProductsDB.Products.ForEach(product => { Console.WriteLine(product.Quantity.GetType()+" "+product.Price.GetType()); });
            Console.WriteLine("\n\n");

            //two products to be ordered
            //UnvalidatedProduct product1 = availableProductsDB.OrderProduct("Product 1", 10);
            //UnvalidatedProduct product2 = availableProductsDB.OrderProduct("Product 2", 5);
            
            UnvalidatedProduct product2 = availableProductsDB.OrderProduct("Telefon", 1);
            UnvalidatedProduct product1 = availableProductsDB.OrderProduct("Pix", 10);

            //checks if quantity has been modified successfully
            availableProductsDB.CheckProducts();
            availableProductsDB.Products.ForEach(product => { Console.WriteLine(product.Quantity.GetType()); });
            Console.WriteLine("\n\n");
            
            //adds ordered products to shopping cart
            shoppingCart = AddProductToShoppingCart(shoppingCart, product1);
            shoppingCart = AddProductToShoppingCart(shoppingCart, product2);//workflow

            ////checks items in shopping cart
            //shoppingCartProducts = GetShoppingCartItems(shoppingCart);
            //shoppingCartProducts.ForEach(product => { Console.WriteLine(product.ToString()); });
            //Console.WriteLine("\n\n");

            ////removes one item from the shopping cart
            ////shoppingCart = RemoveProductFromShoppingCart(shoppingCart, "Product 1");
            //shoppingCart = RemoveProductFromShoppingCart(shoppingCart, "Telefon");

            ////checks if item has been removed successfully
            //shoppingCartProducts = GetShoppingCartItems(shoppingCart);
            //shoppingCartProducts.ForEach(product => { Console.WriteLine(product.ToString()); });
            //Console.WriteLine("\n\n");

            ////checks type of shopping cart
            //Console.WriteLine(shoppingCart.GetType());
            //Console.WriteLine("\n\n");

            
            ValidationWorkflow workflow = new ValidationWorkflow();
            var result = workflow.Execute(new CartCommand(shoppingCart as UnvalidatedShoppingCart));

            ////validates shopping cart
            //shoppingCart = ValidateShoppingCart((UnvalidatedShoppingCart)shoppingCart);
            //Console.WriteLine(shoppingCart.GetType());
            //Console.WriteLine("\n\n");

            ////calculates shopping cart
            //shoppingCart = CalculateShoppingCart(shoppingCart);
            //Console.WriteLine(shoppingCart.GetType());
            //Console.WriteLine("\n\n");

            //orders shopping cart
            switch(result)
            {
                case CartScucceededEvent success:
                    Console.WriteLine("Final Price Is: " + success.FinalPrice);
                    success.ValidatedProducts.ForEach(product => { Console.WriteLine(product.Quantity + " " + product.Price); });
                    break;
                case CartFailedEvent failed:
                    Console.WriteLine(failed.Reason);
                    break;
                default: Console.WriteLine("Error at Order calculation!"); break;
            }

            //workflow 3

            //Afisare si adresa adaugata
            shoppingCart = OrderShoppingCart(shoppingCart);
            Console.WriteLine(shoppingCart.GetType());
        }
    }
}