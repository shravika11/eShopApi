using eShopApi.Models;
using eShopApi.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eShopApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : Controller
    {
        private readonly PaymentService _paymentService;

        public PaymentController(PaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Payment>>> GetAllTransactionAsync()
        {
            List<Payment> transactions = await _paymentService.GetAllTransactionAsync();
            return Ok(transactions);
        }

        [HttpGet("{transactionId}")]
        public async Task<ActionResult<Payment>> GetTransactionAsync(int transactionId)
        {
            Payment transaction = await _paymentService.GetTransactionAsync(transactionId);

            if (transaction == null)
            {
                return NotFound();
            }

            return Ok(transaction);
        }

        [HttpPost]
        public async Task<ActionResult<string>> SaveTransactionAsync([FromBody] Payment payment)
        {
            string result = await _paymentService.SaveTransactionAsync(payment);
            return Ok(result);
        }

        [HttpPut]
        public async Task<ActionResult<string>> UpdateTransactionAsync([FromBody] Payment payment)
        {
            string result = await _paymentService.UpdateTransactionAsync(payment);
            return Ok(result);
        }

        [HttpDelete("{transactionId}")]
        public async Task<ActionResult<string>> DeleteTransactionAsync(int transactionId)
        {
            string result = await _paymentService.DeleteTransactionAsync(transactionId);
            return Ok(result);
        }
    }
}