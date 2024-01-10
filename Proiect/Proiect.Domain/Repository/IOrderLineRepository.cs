using LanguageExt;
using Proiect.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proiect.Domain.Repository
{
    public interface IOrderLineRepository
    {
        TryAsync<Unit> TryCreateOrderLine(ValidatedProduct product);
        Task<List<ValidatedProduct>> TryGetOrderLinesByOrderId(string orderId);
    }
}
