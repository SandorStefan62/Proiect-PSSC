using CSharp.Choices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proiect.Domain.Models
{
    public class Events
    {
        [AsChoice]
        public static partial class CartEvent
        {
            public interface ICartEvent { }

            public record CartScucceededEvent : ICartEvent
            {
                public List<ValidatedProduct> ValidatedProducts { get; }
                public double FinalPrice { get; }

                internal CartScucceededEvent(List<ValidatedProduct> validatedProducts, double finalPrice)
                {
                    ValidatedProducts = validatedProducts;
                    FinalPrice = finalPrice;
                }
            }

            public record CartFailedEvent : ICartEvent
            {
                public string Reason { get; }

                internal CartFailedEvent(string reason)
                {
                    Reason = reason;
                }
            }
        }
    }
}
