using Microsoft.EntityFrameworkCore;
using Testing1.Business.Abstract;
using Testing1.Context;
using Testing1.Models;

namespace Testing1.Business.Concrete
{
    public class ProductService : IProductService
    {
        private readonly ProductContext _context;

        public ProductService(ProductContext context)
        {
            _context = context;
        }

        public async Task<List<Product>> GetAllAsync()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Product?> GetByIdAsync(int id)
        {
            return await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Product> CreateAsync(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existProduct = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
            if (existProduct == null) return false;

            _context.Products.Remove(existProduct);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Product?> UpdateAsync(int id, Product updatedProduct)
        {
            var existProduct = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
            if (existProduct == null) return null;

            existProduct.Name = updatedProduct.Name;
            existProduct.Description = updatedProduct.Description;
            existProduct.Price = updatedProduct.Price;

            await _context.SaveChangesAsync();
            return existProduct;
        }
       
    }
}
