using domain.exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace domain.models
{
    public record Contact
    {
        public string FirstName { get; init; }
        public string LastName { get; init; }
        public string TelephoneNumber { get; init; }
        public string Address { get; init; }

        public Contact(string firstName, string lastName, string telephoneNumber, string address)
        {
            if (!firstName.Any(char.IsDigit))
            {
                this.FirstName = firstName;
            }
            else
            {
                throw new InvalidContactException("FirstName cannot contain numbers");
            }

            if (!lastName.Any(char.IsDigit))
            {
                this.LastName = lastName;
            }
            else
            {
                throw new InvalidContactException("LastName cannot contain numbers");
            }

            if (telephoneNumber.Length <= 10)
            {
                if (!telephoneNumber.Any(char.IsLetter))
                {
                    this.TelephoneNumber= telephoneNumber;
                }
                else
                {
                    throw new InvalidContactException("TelephoneNumber cannot contain letters");
                }
            }
            else
            {
                throw new InvalidContactException("TelephoneNumber cannot have more than 10 numbers");
            }

            this.Address = address;
        }
        public static bool TryParse(string firstName, string lastName, string telephoneNumber, string address, out Contact? result)
        {
            try
            {
                result = new Contact(firstName, lastName, telephoneNumber, address);
                return true;
            }
            catch(InvalidContactException)
            {
                result = null;
                return false;
            }
        }
        public override string ToString()
        {
            return $"[FirstName: {this.FirstName}, LastName: {this.LastName}, TelephoneNumber: {this.TelephoneNumber}, Address: {this.Address}]";
        }
    }
}
