using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proiect.Data.Repository;
using Proiect.Data;
using Proiect.Domain.Models;
using Proiect.Domain.Repository;
using LanguageExt;
using Proiect.Api.Models;

namespace Proiect.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {

        [HttpGet("getAllProducts")]
        public IEnumerable<ProductBody> Get([FromServices] IProductRepository productRepository)
        {
            List<Product> products = productRepository.GetAllProducts();
            List<ProductBody> prettyProducts = new List<ProductBody>();
            foreach (Product product in products) {
                prettyProducts.Add(new ProductBody
                {
                    Price = product.Price.TryGetPrice(),
                    Stock = product.Quantity.TryGetAmount(),
                    Code = product.Code,
                });
            }
            return prettyProducts;
        }
    }
}
