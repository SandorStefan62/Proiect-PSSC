using Proiect.domain.exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Proiect.domain.models.Price;
using static Proiect.domain.models.Quantity;

namespace Proiect.domain.models
{
    public record class Product
    {
        public string Code { get; init; }
        public IQuantity Quantity { get; init; }
        public IPrice Price { get; init; }
        public Product(string code, object quantity, object price)
        {
            this.Code = code;
            this.Quantity = ConvertToQuantity(quantity);
            this.Price = ConvertToPrice(price);
        }
        private static IQuantity ConvertToQuantity(object quantity)
        {
            if(quantity is int intValue)
            {
                return new Units(intValue);
            }
            else if(quantity is string stringValue)
            {
                if(int.TryParse(stringValue, out int parsedNumber))
                {
                    return new Units(parsedNumber);
                }
                else
                {
                    throw new InvalidProductException("Invalid quantity");
                }
            }
            else
            {
                throw new InvalidProductException("Invalid quantity");
            }
        }

        private static IPrice ConvertToPrice(object price)
        {
            if (price is double doubleValue)
            {
                return new MonetaryUnits(doubleValue);
            }
            else if(price is int intValue)
            {
                return new MonetaryUnits((double)intValue);
            }
            else if (price is string stringValue)
            {
                if (double.TryParse(stringValue, out double parsedNumber))
                {
                    return new MonetaryUnits(parsedNumber);
                }
                else
                {
                    throw new InvalidProductException("Invalid price");
                }
            }
            else
            {
                throw new InvalidProductException("Invalid price");
            }
        }
        public static bool TryParse(string codeString, object quantityString, object priceString, out Product? result)
        {
            try
            {
                result = new Product(codeString, quantityString, priceString);
                return true;
            }
            catch(InvalidProductException)
            {
                result = null;
                return false;
            }
        }
        public override string ToString()
        {
            return $"[Code: {this.Code} Quantity: {this.Quantity} Price: {this.Price}]";
        }
    }
}
