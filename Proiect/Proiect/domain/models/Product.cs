using Proiect.domain.exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Proiect.domain.models.Quantity;

namespace Proiect.domain.models
{
    public record class Product
    {
        public string Code { get; init; }
        public IQuantity Quantity { get; init; }
        private double _price;
        public double Price
        {
            get => _price;
            init
            {
                if(value <= 0)
                {
                    throw new InvalidProductException("Price must not be negative");
                }
                _price = value;
            }
        }
        public Product(string code, object quantity, double price)
        {
            this.Code = code;
            this.Quantity = ConvertToQuantity(quantity);
            this.Price = price;
        }
        private static IQuantity ConvertToQuantity(object quantity)
        {
            if(quantity is int number)
            {
                return new Units(number);
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
        public static bool TryParse(string codeString, object quantityString, double priceString, out Product? result)
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
