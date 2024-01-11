using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Proiect.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShopingCartController : ControllerBase
    {
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

        // POST api/<ShopingCartController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
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
