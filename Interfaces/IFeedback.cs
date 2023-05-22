using eShopApi.Models;

namespace eShopApi.Interfaces
{
    public interface IFeedback
    {
        Task<bool> SaveFeedDetailsAsync(Feedback feedback);
        Task<Feedback> GetFeedDetailsAsync(int Id);
        Task<List<Feedback>> GetAllFeedDetailsAsync();
        Task<bool> DeleteFeedDetailsAsync(int Id);
    }
}
