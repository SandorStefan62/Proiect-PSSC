using Proiect.Domain.Models;
using Proiect.Data.Repository;
using Microsoft.EntityFrameworkCore;
using static Proiect.Domain.Models.ShoppingCart;
using static Proiect.Domain.Operations.ShoppingCartOperations;
using Microsoft.Extensions.Logging;
using Proiect.Data;
using Proiect.Domain.Repository;


namespace Proiect
{
    class Program
    {
        private static string ConnectionString = "Integrated Security=true;Server=LAPTOP-DRAGOS\\SQLEXPRESS;Database=master;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=true";
        static void Main(string[] args)
        {

            //DataBase Initialization
            var dbContextBuilder = new DbContextOptionsBuilder<OrderContext>();
            OrderContext orderContext = new(dbContextBuilder.Options);
            OrderHeaderRepository orderHeaderRepositroy = new OrderHeaderRepository(orderContext);
            OrderLineRepository orderLineRepository = new OrderLineRepository(orderContext);
            ProductRepository productRepository = new ProductRepository(orderContext);

            //init
            Contact contact = new Contact("FirstName", "LastName", "1111111111", "Adresa");
            IShoppingCart shoppingCart = new EmptyShoppingCart(contact);
            AvailableProducts availableProducts = new AvailableProducts(productRepository.GetAllProducts());
            AvailableProducts availableProductsOld = new AvailableProducts();
            List<Product> shoppingCartProducts = new List<Product>();

            //checks the available stock
            availableProducts.CheckProducts();
            availableProductsOld.CheckProducts();
            availableProductsOld.Products.ForEach(product => { Console.WriteLine(product.Quantity.GetType()); });
            Console.WriteLine("\n\n");

            //two products to be ordered
            //UnvalidatedProduct product1 = availableProducts.OrderProduct("Product 1", 10);
            //UnvalidatedProduct product2 = availableProducts.OrderProduct("Product 2", 5);
            UnvalidatedProduct product1 = availableProducts.OrderProduct("Pix", 10);
            productRepository.DecreaseQuantity("Pix", 10);
            UnvalidatedProduct product2 = availableProducts.OrderProduct("Telefon", 1);
            productRepository.DecreaseQuantity("Telefon", 1);
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
            //shoppingCart = RemoveProductFromShoppingCart(shoppingCart, "Product 1");
            //shoppingCart = RemoveProductFromShoppingCart(shoppingCart, "Telefon");

            //checks if item has been removed successfully
            shoppingCartProducts = GetShoppingCartItems(shoppingCart);
            shoppingCartProducts.ForEach(product => { Console.WriteLine(product.ToString()); });
            Console.WriteLine("\n\n");

            //checks type of shopping cart
            Console.WriteLine(shoppingCart.GetType());
            Console.WriteLine("\n\n");

            //validates shopping cart
            shoppingCart = ValidateShoppingCart((UnvalidatedShoppingCart)shoppingCart);
            ValidShoppingCart validCart = (ValidShoppingCart)shoppingCart;
            //orderHeaderRepositroy.SaveOrderHeader(validCart);
            //orderLineRepository.SaveProductsFromShoppingCart(validCart);
            Console.WriteLine(shoppingCart.GetType());
            Console.WriteLine("\n\n");

            //calculates shopping cart
            shoppingCart = CalculateShoppingCart(shoppingCart);
            CalculatedShoppingCart calculatedCart = (CalculatedShoppingCart)shoppingCart;
            //orderHeaderRepositroy.SaveCalculatedOrderHeader(calculatedCart);
            Console.WriteLine(shoppingCart.GetType());
            Console.WriteLine("\n\n");

            //orders shopping cart
            shoppingCart = OrderShoppingCart(shoppingCart);
            PaidShoppingCart paidCart = (PaidShoppingCart)shoppingCart;
            //orderHeaderRepositroy.SavePaidOrderHeader(paidCart);
            Console.WriteLine(shoppingCart.GetType());
        }
    }
}