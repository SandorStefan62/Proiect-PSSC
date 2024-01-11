using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Proiect.Domain.Models.Quantity;

namespace Proiect.Domain.Models
{
    public class AvailableProducts
    {
        public AvailableProducts()
        {
            this.Products = LoadProducts();
        }
        public AvailableProducts(List<Product> products)
        {
            this.Products = products;
        }
        public List<Product> Products { get; set; }
        public void CheckProducts()
        {
            foreach (var product in Products)
            {
                Console.WriteLine(product.ToString());
            }
        }
        public Product GetProduct(string code)
        {
            Product product = this.Products.FirstOrDefault(product => product.Code == code);
            if(product == null)
            {
                Console.WriteLine("Product not found.");
            }
            return product;
        }
        public UnvalidatedProduct OrderProduct(string code, object quantity)
        {
            Product product = GetProduct(code);
            if(product == null)
            {
                Console.WriteLine("Product not found.");
                return null;
            }
            if(!int.TryParse(quantity.ToString(), out int orderQuantity))
            {
                throw new ArgumentException("Invalid input.");
            }
            if(orderQuantity <= 0)
            {
                throw new ArgumentException("Ordered quantity must be positive.");
            }
            if (product.Quantity.TryDecrease(orderQuantity))
            {

                Console.WriteLine($"Ordered {orderQuantity} units of {product.Code}. Remaining quantity: {product.Quantity}");
                return new UnvalidatedProduct(product.Code, quantity, product.Price);
            }
            else
            {
                throw new Exception("Insuficient quantiy");
            }
        }
        private static List<Product> LoadProducts()
        {
            return new List<Product>
            {
                new("Product 1", "12", 100.0),
                new("Product 2", 22, 102),
                new("Product 3", "35", 75),
                new("Product 4", "111", 150),
                new("Product 5", 40, 99.99),
                new("Product 6", "55", 120),
                new("Product 7", 18, 80),
                new("Product 8", "23", 200),
                new("Product 9", "28", 89.50),
                new("Product 10", "55", 175)
            };
        }
    }
}
