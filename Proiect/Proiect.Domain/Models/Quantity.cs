using CSharp.Choices;

namespace Proiect.Domain.Models
{
    [AsChoice]
    public static class Quantity
    {
        public interface IQuantity
        {
            bool TryDecrease(int amount);
            int TryGetAmount();
        }
        public record Units : IQuantity
        {
            public Units(int number)
            {
                this.number = number;
            }
            public bool TryDecrease(int amount)
            {
                if(amount > this.number)
                {
                    return false;
                }
                this.number -= amount;
                return true;
            }
            public int TryGetAmount()
            {
                return this.number;
            }
            public override string ToString()
            {
                return $"{this.number}";
            }
            public int number { get; set; }
        }
        public record Undefined : IQuantity
        {
            public Undefined(string undefined)
            {
                this.undefined = undefined;
            }
            public bool TryDecrease(int amount)
            {
                return false;
            }
            public int TryGetAmount()
            {
                return -1;
            }
            public override string ToString()
            {
                return $"{this.undefined}";
            }
            public string undefined { get; init;}
        }
    }
}