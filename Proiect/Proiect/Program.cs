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
            ProductRepository productRepository = new ProductRepository(orderContext);

            //init
            Contact contact = new Contact("asd", "asd", "1111111111", "asd");
            AvailableProducts availableProductsDB = new AvailableProducts(productRepository.TryGetAllProducts().Result);
            AvailableProducts availableProducts = new AvailableProducts();
            List<Product> shoppingCartProducts = new List<Product>();

            //checks the available stock
            availableProductsDB.CheckProducts();
            //availableProductsDB.CheckProducts();
            availableProductsDB.Products.ForEach(product => { Console.WriteLine(product.Quantity+" "+product.Price); });
            Console.WriteLine("\n\n");
            
            UnvalidatedProduct product2 = availableProductsDB.OrderProduct("Telefon", 1);
            UnvalidatedProduct product1 = availableProductsDB.OrderProduct("Pix", 10);

            //checks if quantity has been modified successfully
            availableProductsDB.CheckProducts();
            availableProductsDB.Products.ForEach(product => { Console.WriteLine(product.Quantity); });
            Console.WriteLine("\n\n"); 

            List<UnvalidatedProduct> orderedProducts = new List<UnvalidatedProduct>
            {
                product1,
                product2
            };

            //WORKFLOW: ADD PRODUCTS AND VALIDATE THEM
            ValidationWorkflow valudationWorkflow = new ValidationWorkflow();
            var validationResult = valudationWorkflow.Execute(contact, orderedProducts);

            switch(validationResult)
            {
                case ValidShoppingCart success:
                    Console.WriteLine("Items were added and validated!");
                    success.ValidatedProducts.ForEach(product => { Console.WriteLine(product.Quantity + " " + product.Price); });
                    break;
                case InvalidShoppingCart failed:
                    Console.WriteLine(failed.Reason);
                    break;
                default: Console.WriteLine("Error at Order calculation!"); break;
            }

            //WORKFLOW: CALCULATE TOTAL PRICE
            CalculateOrderWorkflow calculateWorkflow = new CalculateOrderWorkflow();
            var calculateResult = calculateWorkflow.Execute(validationResult);

            switch (calculateResult)
            {
                case CalculatedShoppingCart success:
                    success.ValidatedProducts.ForEach(product => { Console.WriteLine(product.Quantity + " " + product.Price); });
                    Console.WriteLine("Final Price Is: " + success.FinalPrice);
                    break;
                case InvalidShoppingCart failed:
                    Console.WriteLine(failed.Reason);
                    break;
            }

            //WORKFLOW: ORDER SHOPPING CART
            FinishOrderWorkflow finishOrderWorkflow = new FinishOrderWorkflow();
            var finalResult = finishOrderWorkflow.Execute(validationResult);

            switch (finalResult)
            {
                case PaidShoppingCart success:
                    success.ValidatedProducts.ForEach(product => { Console.WriteLine(product.Quantity + " " + product.Price); });
                    Console.WriteLine("Final Price Is: " + success.FinalPrice);
                    Console.WriteLine("CheckoutDate: " + success.CheckoutDate);
                    break;
                case InvalidShoppingCart failed:
                    Console.WriteLine(failed.Reason);
                    break;
            }
        }
    }
}