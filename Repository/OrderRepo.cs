using eShopApi.Data;
using eShopApi.Interfaces;
using eShopApi.Models;
using Microsoft.EntityFrameworkCore;

namespace eShopApi.Repository
{
    public class OrderRepo : IOrder
    {
        private readonly eShopDbContext _context;

        public OrderRepo(eShopDbContext context)
        {
            _context = context;
        }

        // Delete an order from the database using its orderId
        public async Task<string> DeleteOrderDetails(int orderId)
        {
            string msg = "";
            Order deleteOrder = await _context.Orders.FindAsync(orderId);
            if (deleteOrder != null)
            {
                _context.Orders.Remove(deleteOrder);
                await _context.SaveChangesAsync();
                msg = "Deleted Order";
            }
            return msg;
        }

        // Retrieve all orders from the database
        public async Task<List<Order>> GetAllOrderDetails()
        {
            List<Order> order = await _context.Orders.ToListAsync();
            return order;
        }

        // Retrieve a specific order using its orderId
        public async Task<Order> GetOrderDetails(int orderId)
        {
            Order order = await _context.Orders.FindAsync(orderId);
            return order;
        }

        // Save a new order to the database
        public async Task<string> SaveOrderDetails(Order orderDetails)
        {
            await _context.Orders.AddAsync(orderDetails);
            await _context.SaveChangesAsync();
            return "Saved Order";
        }

        // Update an existing order in the database
        public async Task<string> UpdateOrderDetails(Order orderDetails)
        {
            _context.Entry(orderDetails).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return "Updated Order";
        }
    }
}
