using eShopApi.Data;
using eShopApi.Interfaces;
using eShopApi.Models;
using Microsoft.EntityFrameworkCore;

namespace eShopApi.Repository
{
    public class CartRepo : ICart
    {
        private readonly eShopDbContext _context; 
        public CartRepo(eShopDbContext context)
        {
            _context = context;
        }

        // Deletes a cart from the database by ID. Returns a message indicating success or failure.
        public async Task<string> DeleteCart(int CartId)
        {
            string msg = ""; 
            Cart deleteCart = await _context.Carts.FindAsync(CartId);
            if (deleteCart != null) 
            {
                _context.Carts.Remove(deleteCart); 
                await _context.SaveChangesAsync(); 
                msg = "Deleted the Cart"; 
            }
            return msg; 
        }

        // Retrieves all carts from the database. Returns a list of Cart objects.
        public async Task<List<Cart>> GetAllCart()
        {
            List<Cart> carts = await _context.Carts.ToListAsync(); 
            return carts; 
        }

        // Retrieves a cart from the database by ID. Returns a Cart object.
        public async Task<Cart> GetCart(int CartId)
        {
            Cart cart = await _context.Carts.FindAsync(CartId); 
            return cart; 
        }

        // Saves a new cart to the database. Returns a message indicating success or failure.
        public async Task<string> SaveCart(Cart cart)
        {
            await _context.Carts.AddAsync(cart); 
            await _context.SaveChangesAsync(); 
            return "Saved the Cart"; 
        }

        // Updates a cart in the database. Returns a message indicating success or failure.
        public async Task<string> UpdateCart(Cart cart)
        {
            _context.Entry(cart).State = EntityState.Modified; 
            await _context.SaveChangesAsync(); 
            return "Updated the Cart"; 
        }

        // Retrieves all carts belonging to a specific user from the database. Returns a list of Cart objects.
        public async Task<IEnumerable<Cart>> GetCartByUserID(int UserId)
        {
            var cart = await _context.Carts.Where(a => a.UserId == UserId).ToListAsync(); 
            return cart; 
        }

        // Retrieves the ID of a cart belonging to a specific user from the database. Returns the ID as an integer.
        public async Task<int> GetCartId(int UserId)
        {
            Cart cart = await _context.Carts.FirstOrDefaultAsync(q => q.UserId == UserId); 
            int CartId = cart.CartId;
            return CartId; 
        }
    }
}

