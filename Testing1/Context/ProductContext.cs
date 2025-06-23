using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Testing1.Models;

namespace Testing1.Context
{
    public class ProductContext:DbContext
    {

        public ProductContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Product> Products { get; set; }
    }
}
