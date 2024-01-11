using Proiect.Domain.Models;
using LanguageExt;
using Microsoft.EntityFrameworkCore;
using Proiect.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proiect.Data.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly OrderContext orderContext;
        public ProductRepository(OrderContext orderContext)
        {
            this.orderContext = orderContext;
        }
        public TryAsync<Unit> TryDecreaseQuantity(Product product, int quantity)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Product>> TryGetAllProducts()
        {
            var products = await this.orderContext.Product.AsNoTracking().ToListAsync();
            return products.Select(product => new Product(product.Code,product.Stock,(product.Price)))
                           .ToList();
        }
        public List<Product> GetAllProducts()
        {
            var products = this.orderContext.Product.ToList();
            return products.Select(product => new Product(product.Code, product.Stock, (product.Price)))
                           .ToList();
        }
    }
}
