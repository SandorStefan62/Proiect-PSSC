using Proiect.domain.models;

namespace Proiect
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Product> inventory = Storage.LoadProducts();
            inventory.ForEach(p => { Console.WriteLine(p.ToString() + " " + p.Quantity.GetType() + " " + p.Price.GetType() + "\n"); });
        }
    }
}