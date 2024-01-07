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
            if(shopping is ValidShoppingCart validShopping)
            {
                foreach(ValidatedProduct product in validShopping.ValidatedProducts)
                {
                    Console.WriteLine(product.ToString() + " " + product.Quantity.GetType() + " " + product.Price.GetType() + "\n");
                }
            }
            //IShoppingCart final = CalculateShoppingCart(shopping);
        }
    }
}