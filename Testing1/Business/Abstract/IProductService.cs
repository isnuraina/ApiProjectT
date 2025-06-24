using Testing1.Models;

namespace Testing1.Business.Abstract
{
    public interface IProductService
    {
        Task<List<Product>> GetAllAsync();
        Task<Product?> GetByIdAsync(int id);
        Task<Product> CreateAsync(Product product);
        Task<bool> DeleteAsync(int id);
        Task<Product?> UpdateAsync(int id, Product updatedProduct);
    }
}
