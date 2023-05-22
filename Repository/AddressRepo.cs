using eShopApi.Data;
using eShopApi.Interfaces;
using eShopApi.Models;
using Microsoft.EntityFrameworkCore;

namespace eShopApi.Repository
{
    public class AddressRepo : IAddress
    {
        // database context
        private readonly eShopDbContext _context;

        public AddressRepo(eShopDbContext context)
        {
            _context = context;
        }

        // Delete an address from the database using its addressId
        public async Task<string> DeleteAddress(int addressId)
        {
            string msg = "";
            // Find the address by id
            Address deleteAddress = await _context.Address.FindAsync(addressId);
            if (deleteAddress != null)
            {
                // remove the address from database and save the changes
                _context.Address.Remove(deleteAddress);
                await _context.SaveChangesAsync();
                msg = "Deleted";
            }
            return msg;
        }

        // Retrieve an address from the database using its addressId
        public async Task<Address> GetAddress(int addressId)
        {
            // Find the address by id
            Address address = await _context.Address.FindAsync(addressId);
            return address;
        }

        // Retrieve an address of a specific user from the database using userId
        public async Task<Address> GetUserAddress(int userId)
        {
            // Find the first matching address for the given userId
            Address address = await _context.Address.FirstOrDefaultAsync(a => a.UserId == userId);
            return address;
        }

        // Retrieve all addresses from the database
        public async Task<List<Address>> GetAllAddress()
        {
            List<Address> addressList = await _context.Address.ToListAsync();
            return addressList;
        }

        // Add a new address to the database
        public async Task<string> SaveAddress(Address address)
        {
            // Add the new address to the database and save the changes
            await _context.Address.AddAsync(address);
            await _context.SaveChangesAsync();
            return "Saved";
        }

        // Update an existing address in the database
        public async Task<string> UpdateAddress(Address address)
        {
            // Mark the address as modified and then save the changes
            _context.Entry(address).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return "Updated";
        }

        public Task<List<Address>> GetAllAddresses()
        {
            throw new NotImplementedException();
        }
    }
}
