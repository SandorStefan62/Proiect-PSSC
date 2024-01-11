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
            public record SenderCartScucceededEvent : ICartEvent
            {
                public List<ValidatedProduct> ValidatedProducts { get; }
                public Contact Contact { get; }

                double FinalPrice { get; }

                DateTime CheckoutDate { get; }

                internal SenderCartScucceededEvent(List<ValidatedProduct> validatedProducts, Contact contact, double finalPrice, DateTime checkoutDate)
                {
                    ValidatedProducts = validatedProducts;
                    Contact = contact;
                    FinalPrice = finalPrice;
                    CheckoutDate = checkoutDate;
                }
            }

            public record InitialCartScucceededEvent : ICartEvent
            {
                public List<UnvalidatedProduct> UnvalidatedProducts { get; }
                public Contact Contact { get; }

                internal InitialCartScucceededEvent(List<UnvalidatedProduct> unvalidatedProducts, Contact contact)
                {
                    UnvalidatedProducts = unvalidatedProducts;
                    Contact = contact;
                }
            }

            public record AditionalCartScucceededEvent : ICartEvent
            {
                public List<UnvalidatedProduct> UnvalidatedProducts { get; }

                internal AditionalCartScucceededEvent(List<UnvalidatedProduct> unvalidatedProducts)
                {
                    UnvalidatedProducts = unvalidatedProducts;
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
