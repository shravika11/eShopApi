using eShopApi.Models;

namespace eShopApi.Interfaces
{
    public interface IOrder
    {
        Task<string> DeleteOrderDetails(int orderId);
        Task<List<Order>> GetAllOrderDetails();
        Task<Order> GetOrderDetails(int orderId);
        Task<string> SaveOrderDetails(Order orderDetails);
        Task<string> UpdateOrderDetails(Order orderDetails);
    }
}
