using domain.models;
using static domain.models.ShoppingCart;
using static domain.operations.ShoppingCartOperations;

namespace Proiect
{
    class Program
    {
        static void Main(string[] args)
        {
            List<UnvalidatedProduct> inventory = Storage.LoadProducts();
            //inventory.ForEach(p => { Console.WriteLine(p.ToString() + " " + p.Quantity.GetType() + " " + p.Price.GetType() + "\n"); });
            Contact contact = new Contact("asdas", "belfast", "1234567890", "asaada");
            UnvalidatedShoppingCart shoppingCart = new UnvalidatedShoppingCart(inventory, contact);
            IShoppingCart shopping = ValidateShoppingCart(shoppingCart);
            Console.WriteLine("\n" + shopping.GetType());
            IShoppingCart final = CalculateShoppingCart(shopping);
            if(final is CalculatedShoppingCart calculatedShoppingCart)
            {
                Console.WriteLine(calculatedShoppingCart.FinalPrice);
            }
            Console.WriteLine(final.GetType());
        }
    }
}