using CSharp.Choices;

namespace domain.models
{
    [AsChoice]
    public static class Quantity
    {
        public interface IQuantity { };
        public record Units : IQuantity
        {
            public Units(int number)
            {
                this.number = number;
            }
            public override string ToString()
            {
                return $"{this.number}";
            }
            public int number {  get; init; }
        }
        public record Undefined : IQuantity
        {
            public Undefined(string undefined)
            {
                this.undefined = undefined;
            }
            public override string ToString()
            {
                return $"{this.undefined}";
            }
            public string undefined { get; init;}
        }
    }
}