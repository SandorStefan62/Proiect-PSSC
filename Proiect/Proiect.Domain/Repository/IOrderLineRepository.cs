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
    public interface IOrderLineRepository
    {
        TryAsync<Unit> TryCreateOrderLine(ValidatedProduct product);
        void SaveProductsFromShoppingCart(ValidShoppingCart order); //Incearca sa implementezi asta cu exemplul de la prof,de la gradeRepository
    }
}
