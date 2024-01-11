using Microsoft.AspNetCore.Mvc;
using Proiect.Domain.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Proiect.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShopingCartController : ControllerBase
    {
        public static Contact contactCurent;

        // POST api/<ShopingCartController>
        [HttpPost("addContatct")]
        public IActionResult AddContact([FromBody] Contact contact)
        {
            contactCurent = contact;
            return Ok(contactCurent);
        }

        // POST api/<ShopingCartController>
        [HttpPost("addProductsToShoppingCartForCurrentUser")]
        public IActionResult AddProductsToShoppingCartForCurrentUser([FromBody] List<UnvalidatedProduct> products)
        {

            return Ok(products);
        }

        // GET: api/<ShopingCartController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<ShopingCartController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // PUT api/<ShopingCartController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ShopingCartController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
