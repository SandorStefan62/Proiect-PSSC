using domain.exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static domain.models.Price;
using static domain.models.Quantity;

namespace domain.models
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
            try
            {
                if (quantity is int intValue)
                {
                    if (intValue > 0)
                    {
                        return new Units(intValue);
                    }
                    else
                    {
                        throw new InvalidProductException("Quantity must not be negative");
                    }
                }
                else if (quantity is string stringValue)
                {
                    if (int.TryParse(stringValue, out int parsedNumber))
                    {
                        if (parsedNumber > 0)
                        {
                            return new Units(parsedNumber);
                        }
                        else
                        {
                            throw new InvalidProductException("Quantity must not be negative");
                        }
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
            catch (InvalidProductException exception)
            {
                Console.WriteLine($"Error converting quantity: {exception.Message}");
                Console.WriteLine($"Problematic quantity: {quantity}");
                Console.WriteLine($"Quantity type: {quantity.GetType()}");
                if(quantity is string stringValue)
                {
                    Console.WriteLine($"Parsed string value: {stringValue}");
                }
                throw;
            }
            
        }

        private static IPrice ConvertToPrice(object price)
        {
            try
            {
                if (price is MonetaryUnits monetaryUnits)
                {
                    if (monetaryUnits.number > 0)
                    {
                        return monetaryUnits;
                    }
                    else
                    {
                        throw new InvalidProductException("Price must not be negative");
                    }
                }
                else if (price is double doubleValue)
                {
                    if (doubleValue > 0)
                    {
                        return new MonetaryUnits(doubleValue);
                    }
                    else
                    {
                        throw new InvalidProductException("Price must not be negative");
                    }
                }
                else if (price is int intValue)
                {
                    if (intValue > 0)
                    {
                        return new MonetaryUnits(intValue);
                    }
                    else
                    {
                        throw new InvalidProductException("Price must not be negative");
                    }
                }
                else if (price is string stringValue)
                {
                    if (double.TryParse(stringValue, out double parsedNumber))
                    {
                        if (parsedNumber > 0)
                        {
                            return new MonetaryUnits(parsedNumber);
                        }
                        else
                        {
                            throw new InvalidProductException("Price must not be negative");
                        }
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
            catch (InvalidProductException ex)
            {
                Console.WriteLine($"Error converting price: {ex.Message}");
                Console.WriteLine($"Problematic price: {price}");
                Console.WriteLine($"Price type: {price.GetType()}");
                if (price is string stringValue)
                {
                    Console.WriteLine($"Parsed string value: {stringValue}");
                }
                throw;
            }
        }
        public static bool TryParse(string codeString, object quantityString, object priceString, out Product? result)
        {
            try
            {
                result = new Product(codeString, quantityString, priceString);
                return true;
            }
            catch (InvalidProductException)
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
