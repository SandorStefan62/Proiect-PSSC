using Proiect.Domain.Models;
using LanguageExt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proiect.Domain.Repository
{
    public interface IProductRepository
    {
        Task<List<Product>> TryGetAllProducts();
        TryAsync<Unit> TryDecreaseQuantity(Product product, int quantity);

    }
}
