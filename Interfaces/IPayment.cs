using eShopApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eShopApi.Interfaces
{
    public interface IPayment
    {
        Task<List<Payment>> GetAllTransactionAsync();
        Task<Payment> GetTransactionAsync(int transactionId);
        Task<string> SaveTransactionAsync(Payment payment);
        Task<string> UpdateTransactionAsync(Payment payment);
        Task<string> DeleteTransactionAsync(int transactionId);
    }
}