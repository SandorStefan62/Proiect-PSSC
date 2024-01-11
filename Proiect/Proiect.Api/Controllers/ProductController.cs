using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proiect.Data.Repository;
using Proiect.Data;
using Proiect.Domain.Models;
using Proiect.Domain.Repository;

namespace Proiect.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {

        [HttpGet]
        public IEnumerable<Product> Get([FromServices] IProductRepository productRepository)
        {
            var products = productRepository.GetAllProducts();
            return products;
        }
    }
}
