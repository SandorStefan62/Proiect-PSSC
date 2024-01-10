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
        public TryAsync<Unit> TrySaveOrderHeader(PaidShoppingCart order)
        {
            throw new NotImplementedException();
        }

        public TryAsync<Unit> TrySaveOrderHeader(CalculatedShoppingCart order)
        {
            throw new NotImplementedException();
        }
        public Task<ShoppingCart> TryGetOrderHeaderByContact(Contact contact)
        {
            throw new NotImplementedException();
        }
    }
}
