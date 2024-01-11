using Microsoft.AspNetCore.Mvc;
using Proiect.Api.Models;
using Proiect.Data.Repository;
using Proiect.Domain.Models;
using Proiect.Domain.Repository;
using Proiect.Domain.Workflows;
using static Proiect.Domain.Models.ShoppingCart;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Proiect.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShopingCartController : ControllerBase
    {
        public static Contact contactCurent;
        public static IShoppingCart currentShoppingCart;
        private readonly ValidationWorkflow validationWorkflow;
        private readonly CalculateOrderWorkflow calculationWorkflow;
        private readonly FinishOrderWorkflow finishWorkflow;

        public ShopingCartController(ValidationWorkflow validationWorkflow,CalculateOrderWorkflow calculationWorkflow, FinishOrderWorkflow finishWorkflow)
        {
            this.validationWorkflow = validationWorkflow;
            this.calculationWorkflow = calculationWorkflow;
            this.finishWorkflow = finishWorkflow;
        }

        // POST api/<ShopingCartController>
        [HttpPost("addContatct")]
        public IActionResult AddContact([FromBody] ContactInput contact)
        {
            contactCurent = new Contact(contact.FirstName,contact.LastName,contact.TelephoneNumber,contact.Address);
            return Ok(contactCurent);
        }

        // POST api/<ShopingCartController>
        [HttpPost("addProductsToShoppingCartForCurrentUser")]
        public IActionResult AddProductsToShopingCartForCurrentUser([FromServices] IProductRepository productRepository, [FromServices] IOrderHeaderRepository orderHeaderRepository, [FromServices] IOrderLineRepository orderLineRepository, [FromBody] ProductInput[] products)
        {
            AvailableProducts availableProducts = new AvailableProducts(productRepository.GetAllProducts());
            List<UnvalidatedProduct> unvalidatedProducts = new List<UnvalidatedProduct>();
            foreach (var product in products)
            {
                UnvalidatedProduct unvalidatedProduct = availableProducts.OrderProduct(product.Code, product.Quantity);
                productRepository.DecreaseQuantity(product.Code, product.Quantity);
                unvalidatedProducts.Add(unvalidatedProduct);
            }
            currentShoppingCart = validationWorkflow.Execute(contactCurent, unvalidatedProducts);
            orderHeaderRepository.SaveOrderHeader((ValidShoppingCart)currentShoppingCart);
            orderLineRepository.SaveProductsFromShoppingCart((ValidShoppingCart)currentShoppingCart);
            return Ok(currentShoppingCart);
        }

        [HttpPut("calculate/{adress}")]
        public IActionResult CalculateShopingCart(string adress, [FromServices] IOrderHeaderRepository orderHeaderRepository)
        {
            currentShoppingCart = calculationWorkflow.Execute(currentShoppingCart);
            orderHeaderRepository.SaveCalculatedOrderHeader((CalculatedShoppingCart)currentShoppingCart);
            return Ok(currentShoppingCart);
        }

        [HttpPut("pay/{adress}")]
        public IActionResult PayShopingCart(string adress, [FromServices] IOrderHeaderRepository orderHeaderRepository)
        {
            currentShoppingCart = finishWorkflow.Execute(currentShoppingCart);
            orderHeaderRepository.SavePaidOrderHeader((PaidShoppingCart)currentShoppingCart);
            return Ok(currentShoppingCart);
        }
    }
}
