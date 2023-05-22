using eShopApi.Models;

namespace eShopApi.Interfaces
{
    public interface IUserDetail
    {
        Task<string> SaveUserDetailAsync(UserDetail userDetail);
        Task<UserDetail> GetUserDetailAsync(int userId);
        Task<List<UserDetail>> GetAllUserDetailsAsync();
        Task<string> UpdateUserDetailAsync(UserDetail userDetail);
        Task<string> DeleteUserDetailAsync(int userId);
        Task<UserDetail> GetUserByEmailAsync(string emailId);
    }
}
