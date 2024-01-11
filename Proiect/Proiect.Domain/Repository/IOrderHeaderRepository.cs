//using LanguageExt;
using LanguageExt;
using Proiect.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Proiect.Domain.Models.ShoppingCart;

namespace Proiect.Domain.Repository
{
    public interface IOrderHeaderRepository
    {
        void SaveOrderHeader(ValidShoppingCart order);
        void SaveCalculatedOrderHeader(CalculatedShoppingCart order);
        void SavePaidOrderHeader(PaidShoppingCart order);
        Task<ShoppingCart> TryGetOrderHeaderByContact(Contact contact);
    }
}
