using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proiect.domain.models
{
    internal class Storage
    {
        public static List<Product> LoadProducts()
        {
            return new List<Product>
            {
                new("Product 1", "12", 100),
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
