using LanguageExt;
using Proiect.Domain.Models;
using Proiect.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proiect.Data.Repository
{
    public class OrderLineRepository : IOrderLineRepository
    {
        private readonly OrderContext orderContext;
        public OrderLineRepository(OrderContext orderContext)
        {
            this.orderContext = orderContext;
        }

        public TryAsync<Unit> TryCreateOrderLine(ValidatedProduct product)
        {
            throw new NotImplementedException();
        }

        public Task<List<ValidatedProduct>> TryGetOrderLinesByOrderId(string orderId)
        {
            throw new NotImplementedException();
        }
    }
}
