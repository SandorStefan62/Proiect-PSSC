/*    using Proiect.domain.models;
    using static Proiect.domain.models.ShoppingCart;

    namespace Proiect.domain.operations
    {
        public static class ShoppingCartOperations
        {
            public static IShoppingCart ValidateShoppingCart(UnvalidatedShoppingCart unvalidatedShoppingCart)
            {
                List<ValidatedProduct> validatedProducts = new List<ValidatedProduct>();
                Contact contact = null;
                bool isValid = false;
                string invalidReason = string.Empty;

                foreach(UnvalidatedProduct unvalidatedProduct in unvalidatedShoppingCart.UnvalidatedProducts )
                {
                    if(!Product.TryParse(unvalidatedProduct.Code, unvalidatedProduct.Quantity, unvalidatedProduct.Price, out var parsedProduct))
                    {
                        invalidReason = $"Invalid product format: [{unvalidatedProduct.Code}, {unvalidatedProduct.Quantity}, {unvalidatedProduct.Price}]";
                    }
                }
            }
        }
    }*/