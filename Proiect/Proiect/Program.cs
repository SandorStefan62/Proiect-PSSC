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

            Console.WriteLine("\n");
            Product productA = new Product("codeA", a);
            Product productB = new Product("codeB", b);
            Console.WriteLine(productA.ToString() + " " + productA.Quantity.GetType() + "\n" + productB.ToString() + " " + productB.Quantity.GetType() + "\n" );

            //test exceptions
            Console.WriteLine("\n");
            Contact contactA = new Contact("Marius", "Adam", "1234567890", "asdaads");
            //Contact contactB = new Contact("1", "Adam", "1234567890", "asdaads");
            //Contact contactC = new Contact("Marius", "2", "1234567890", "asdaads");
            //Contact contactD = new Contact("Marius", "Adam", "12345678901", "asdaads");
            //Contact contactE = new Contact("Marius", "Adam", "123456789a", "asdaads");
        }
    }
}