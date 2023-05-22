using eShopApi.Data;
using eShopApi.Interfaces;
using eShopApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eShopApi.Repository
{
    public class PaymentRepo : IPayment
    {
        private readonly eShopDbContext _context;

        public PaymentRepo(eShopDbContext context)
        {
            _context = context;
        }


        // Deletes a payment transaction from the database.
        public async Task<string> DeleteTransactionAsync(int transactionId)
        {
            string msg = "";
            Payment deleteTransaction = await _context.Payment.FindAsync(transactionId);

            if (deleteTransaction != null)
            {
                _context.Payment.Remove(deleteTransaction);
                await _context.SaveChangesAsync();
                msg = "Deleted";
            }
            else
            {
                msg = "Transaction not found";
            }

            return msg;
        }

        // Gets all payment transactions from the database.
        public async Task<List<Payment>> GetAllTransactionAsync()
        {
            List<Payment> transactions = await _context.Payment.ToListAsync();
            return transactions;
        }
        //Gets a specific payment transaction from the database.
        public async Task<Payment> GetTransactionAsync(int transactionId)
        {
            Payment transaction = await _context.Payment.FindAsync(transactionId);
            return transaction;
        }

       
        // Saves a new payment transaction to the database.
        public async Task<string> SaveTransactionAsync(Payment payment)
        {
            _context.Payment.Add(payment);
            await _context.SaveChangesAsync();
            return "Saved";
        }
        /// Updates an existing payment transaction in the database.
        public async Task<string> UpdateTransactionAsync(Payment payment)
        {
            _context.Entry(payment).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return "Updated";
        }
    }
}