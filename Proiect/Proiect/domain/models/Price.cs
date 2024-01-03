using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proiect.domain.models
{
    public static class Price
    {
        public interface IPrice { }

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
            public string undefined { get; init; }
        }

    }
}
