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
using static LanguageExt.Prelude;
using Microsoft.EntityFrameworkCore;

namespace Proiect.Data.Repository
{
    public class OrderHeaderRepository : IOrderHeaderRepository
    {
        private readonly OrderContext orderContext;
        public OrderHeaderRepository(OrderContext orderContext)
        {
            this.orderContext = orderContext;
        }
        public void SaveOrderHeader(ValidShoppingCart order)
        {
            var newOrderLine = new OrderHeaderDTO()
            {
                Total = 0,
                Adress = order.Contact.Address,
                FirstName = order.Contact.FirstName,
                LastName = order.Contact.LastName,
                TelephoneNumber = order.Contact.TelephoneNumber,
                CheckoutDate = DateTime.MaxValue,
            };
            orderContext.Add(newOrderLine);
            orderContext.SaveChanges();
        }
        

        public void SaveCalculatedOrderHeader(CalculatedShoppingCart order)
        {
            /* var searchCondition = (orderContext.OrderHeader.ToList()).ToLookup(orderHeader => orderHeader.Adress);
             var newOrderLine = new OrderHeaderDTO()
             {
                 OrderId = searchCondition[order.Contact.Address].SingleOrDefault().OrderId,
                 Total = (decimal) order.FinalPrice,
                 Adress = order.Contact.Address,
                 FirstName = order.Contact.FirstName,
                 LastName = order.Contact.LastName,
                 TelephoneNumber = order.Contact.TelephoneNumber,
                 CheckoutDate = DateTime.MaxValue,
             }; 
             orderContext.Entry(newOrderLine).State = EntityState.Modified;
             orderContext.SaveChanges();*/

            var existingOrder = orderContext.OrderHeader.First(a => a.Adress == order.Contact.Address);
            existingOrder.Total = (decimal)order.FinalPrice;
            orderContext.SaveChanges();
        }
        public void SavePaidOrderHeader(PaidShoppingCart order)
        {
            var existingOrder = orderContext.OrderHeader.First(a => a.Adress == order.Contact.Address);
            existingOrder.CheckoutDate = order.CheckoutDate;
            orderContext.SaveChanges();
        }
        public Task<ShoppingCart> TryGetOrderHeaderByContact(Contact contact)
        {
            throw new NotImplementedException();
        }
    }
}
