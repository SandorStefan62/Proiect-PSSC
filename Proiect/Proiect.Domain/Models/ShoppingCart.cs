﻿using CSharp.Choices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proiect.Domain.Models
{
    [AsChoice]
    public partial class ShoppingCart
    {
        public interface IShoppingCart { }
        public record EmptyShoppingCart : IShoppingCart // empty -> validat ; validat -> calculat ; calculat -> paid
        {
            public EmptyShoppingCart(Contact contact)
            {
                this.UnvalidatedProducts = new List<UnvalidatedProduct>();
                this.Contact = contact;
            }
            public List<UnvalidatedProduct>UnvalidatedProducts { get; init; }
            public Contact Contact { get; init; }
        }
        public record UnvalidatedShoppingCart : IShoppingCart
        {
            internal UnvalidatedShoppingCart(List<UnvalidatedProduct> unvalidatedProducts, Contact contact)
            {
                this.UnvalidatedProducts = unvalidatedProducts;
                this.Contact = contact;
            }
            public List<UnvalidatedProduct> UnvalidatedProducts { get; init; }
            public Contact Contact { get; init; }
        }
        public record InvalidShoppingCart : IShoppingCart
        {
            internal InvalidShoppingCart(List<UnvalidatedProduct> unvalidatedProducts, Contact contact, string reason)
            {
                this.InvalidProducts = unvalidatedProducts;
                this.Contact = contact;
                this.Reason = reason;
            }
            public List<UnvalidatedProduct> InvalidProducts { get; init; }
            public Contact Contact { get; init; }
            public string Reason { get; init; }
        }
        public record ValidShoppingCart : IShoppingCart
        {
            internal ValidShoppingCart(List<ValidatedProduct> validatedProducts, Contact contact)
            {
                this.ValidatedProducts = validatedProducts;
                this.Contact= contact;
            }
            public List<ValidatedProduct> ValidatedProducts { get; init; }
            public Contact Contact { get; init; }
        }
        public record CalculatedShoppingCart : IShoppingCart
        {
            internal CalculatedShoppingCart(List<ValidatedProduct> validatedProducts, Contact contact, double finalPrice)
            {
                this.ValidatedProducts = validatedProducts;
                this.Contact = contact;
                this.FinalPrice = finalPrice;
            }
            public List<ValidatedProduct> ValidatedProducts { get; init; }
            public Contact Contact { get; init; }
            public double FinalPrice { get; init; }
        }
        public record PaidShoppingCart : IShoppingCart
        {
            internal PaidShoppingCart(List<ValidatedProduct> validatedProducts, Contact contact, double finalPrice, DateTime checkoutDate)
            {
                this.ValidatedProducts= validatedProducts;
                this.Contact = contact;
                this.FinalPrice = finalPrice;
                this.CheckoutDate = checkoutDate;
            }
            public List<ValidatedProduct> ValidatedProducts { get; init; }
            public Contact Contact { get; init; }
            public double FinalPrice { get; init; }
            public DateTime CheckoutDate {  get; init; }
        }

    }
}
