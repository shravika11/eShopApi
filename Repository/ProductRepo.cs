using eShopApi.Data;
using eShopApi.Interfaces;
using eShopApi.Models;
using Microsoft.EntityFrameworkCore;

namespace eShopApi.Repository
{
    public class ProductRepo : IProduct
    {
        private readonly eShopDbContext _context;

        public ProductRepo(eShopDbContext context)
        {
            _context = context;
        }

        // Method to delete a product 
        public async Task<string> DeleteProductAsync(int ProductId)
        {
            string msg = "";
            Product delete = await _context.Products.FindAsync(ProductId);
            try
            {
                if (delete != null)
                {
                    _context.Products.Remove(delete);
                    await _context.SaveChangesAsync();
                    msg = "Product deleted successfully";
                }
                else
                {
                    msg = "Product not found";
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while deleting product.", ex);
            }
            return msg;
        }

        // Method to get all products 
        public async Task<List<Product>> GetAllProductsAsync()
        {
            List<Product> products = await _context.Products.ToListAsync();
            return products;
        }

        // Method to get a specific product 
        public async Task<Product> GetProductAsync(int ProductId)
        {
            try
            {
                Product product = await _context.Products.FindAsync(ProductId);

                if (product == null)
                {
                    throw new KeyNotFoundException("Product not found");
                }

                return product;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while getting product.", ex);
            }
        }

        // Method to save a product 
        public async Task<string> SaveProductAsync(Product product)
        {
            try
            {
                _context.Products.Add(product);
                await _context.SaveChangesAsync();
                return "Product saved successfully";
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while saving product.", ex);
            }
        }

        // Method to update a product 
        public async Task<string> UpdateProductAsync(Product product)
        {
            try
            {
                _context.Entry(product).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return "Product updated successfully";
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while updating product.", ex);
            }
        }

        // Method to get products by category 
        public async Task<List<Product>> GetProductsByCategoryAsync(string category)
        {
            try
            {
                List<Product> products = await _context.Products
                    .Where(p => p.Category == category)
                    .ToListAsync();

                if (products == null || products.Count == 0)
                {
                    throw new KeyNotFoundException("No products found in this category");
                }

                return products;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while getting products by category.", ex);
            }
        }
    }
}
