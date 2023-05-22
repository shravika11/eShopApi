using eShopApi.Data;
using eShopApi.Interfaces;
using eShopApi.Models;
using Microsoft.EntityFrameworkCore;

namespace eShopApi.Repository
{
    public class UserDetailRepo : IUserDetail
    {
        private readonly eShopDbContext _context;

        public UserDetailRepo(eShopDbContext context)
        {

            _context = context;
        }

        // Method to delete user details 
        public async Task<string> DeleteUserDetailAsync(int userId)
        {
            string msg = "";
            UserDetail deleteUser = await _context.UserDetails.FindAsync(userId);

            try
            {
                if (deleteUser != null)
                {
                    _context.UserDetails.Remove(deleteUser);
                    await _context.SaveChangesAsync();
                    msg = "Deleted User";
                }
            }
            catch (Exception)
            {
                throw;
            }
            return msg;
        }

        // Method to get all user details 
        public async Task<List<UserDetail>> GetAllUserDetailsAsync()
        {
            try
            {
                List<UserDetail> userDetail = await _context.UserDetails.ToListAsync();
                return userDetail;
            }
            catch (Exception)
            {
                throw;
            }
        }

        // Method to get a specific user detail 
        public async Task<UserDetail> GetUserDetailAsync(int userId)
        {
            try
            {
                UserDetail userDetail = await _context.UserDetails.FindAsync(userId);
                return userDetail;
            }
            catch (Exception)
            {
                throw;
            }
        }

        // Method to save user details 
        public async Task<string> SaveUserDetailAsync(UserDetail userDetail)
        {
            try
            {
                var existingUser = await _context.UserDetails.FirstOrDefaultAsync(u => u.EmailId == userDetail.EmailId);
                if (existingUser != null)
                {
                    return "Email address is already registered";
                }

                await _context.UserDetails.AddAsync(userDetail);
                await _context.SaveChangesAsync();
                return "Saved User";
            }
            catch (Exception ex)
            {
                return $"Error while saving user details. Error message: {ex.Message}";
            }
        }

        // Method to update user details
        public async Task<string> UpdateUserDetailAsync(UserDetail userDetail)
        {
            try
            {
                _context.Entry(userDetail).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
            return "Updated User";
        }

        // Method to get user detail by email 
        public async Task<UserDetail> GetUserByEmailAsync(string emailId)
        {
            UserDetail userDetail = null;
            try
            {
                userDetail = await _context.UserDetails.FirstOrDefaultAsync(q => q.EmailId == emailId);
            }
            catch (Exception)
            {
                throw;
            }
            return userDetail;
        }
    }
}
