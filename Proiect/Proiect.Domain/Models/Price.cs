using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proiect.Domain.Models
{
    public static class Price
    {
        public interface IPrice {
            decimal TryGetPrice();
        }

        public record MonetaryUnits : IPrice
        {
            public MonetaryUnits(double number)
            {
                this.number = number;
            }
            public override string ToString()
            {
                return $"{this.number}";
            }
            public decimal TryGetPrice()
            {
                return (decimal)this.number;
            }
            public double number { get; init; }
        }
        public record Undefined : IPrice
        {
            public Undefined(string undefined)
            {
                this.undefined = undefined;
            }
            public override string ToString()
            {
                return $"{this.undefined}";
            }
            public decimal TryGetPrice()
            {
                return -1;
            }
            public string undefined { get; init; }
        }

    }
}
