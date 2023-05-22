using eShopApi.Models;

namespace eShopApi.Interfaces
{
    public interface ICart
    {
        Task<string> DeleteCart(int cartId);
        Task<List<Cart>> GetAllCart();
        Task<Cart> GetCart(int cartId);
        Task<string> SaveCart(Cart cart);
        Task<string> UpdateCart(Cart cart);
        Task<IEnumerable<Cart>> GetCartByUserID(int UserId);
        Task<int> GetCartId(int UserId);
    }
}
