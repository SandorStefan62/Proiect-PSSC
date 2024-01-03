using Proiect.domain.models;

namespace Proiect
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Quantity.IQuantity a = new Quantity.Units(123);
            Quantity.IQuantity b = new Quantity.Undefined("asdasd");

            Console.WriteLine(a.ToString() + " " + a.GetType() + "\n" + b.ToString() + " " + b.GetType());
        }
    }
}