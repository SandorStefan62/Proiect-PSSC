using Microsoft.EntityFrameworkCore;
using Proiect.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proiect.Data
{
    public class OrderContext :DbContext
    {
        public OrderContext(DbContextOptions<OrderContext> options) : base(options)
        {
        }
        public DbSet<ProductDTO> Product { get; set; }
        public DbSet<OrderHeaderDTO> OrderHeader { get; set; }
        public DbSet<OrderLineDTO> OrderLine { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductDTO>().ToTable("Product").HasKey(s => s.ProductId);
            modelBuilder.Entity<OrderHeaderDTO>().ToTable("OrderHeader").HasKey(s => s.OrderId);
            modelBuilder.Entity<OrderLineDTO>().ToTable("OrderLine").HasKey(s => s.OrderLineId);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Integrated Security=true;Server=LAPTOP-DRAGOS\\SQLEXPRESS;Database=master;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=true");
        }
    }
}
