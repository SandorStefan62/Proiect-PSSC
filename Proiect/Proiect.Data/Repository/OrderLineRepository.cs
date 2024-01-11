using LanguageExt;
using Proiect.Data.Model;
using Proiect.Domain.Models;
using Proiect.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Proiect.Domain.Models.ShoppingCart;

namespace Proiect.Data.Repository
{
    public class OrderLineRepository : IOrderLineRepository
    {
        private readonly OrderContext orderContext;
        public OrderLineRepository(OrderContext orderContext)
        {
            this.orderContext = orderContext;
        }

        public void SaveProductsFromShoppingCart(ValidShoppingCart order)
        {
            var orderHeaders = (orderContext.OrderHeader.ToList()).ToLookup(orderHeader => orderHeader.Adress);
            var products = (orderContext.Product.ToList()).ToLookup(product => product.Code);
            IEnumerable<OrderLineDTO> newProducts = order.ValidatedProducts.Where(g => g.OrderId == 0)
                .Select(i => new OrderLineDTO()
                {
                    OrderId = orderHeaders[order.Contact.Address].SingleOrDefault().OrderId,
                    ProductId = products[i.Code].SingleOrDefault().ProductId,
                    Quantity = i.Quantity.TryGetAmount(),
                    OrderLineId = i.OrderId,
                    Price = Decimal.Multiply(products[i.Code].SingleOrDefault().Price,i.Quantity.TryGetAmount()),        
                }
                );
            orderContext.AddRange(newProducts);
            orderContext.SaveChanges();
        }

        public TryAsync<Unit> TryCreateOrderLine(ValidatedProduct product)
        {
            throw new NotImplementedException();
        }
    }
}
