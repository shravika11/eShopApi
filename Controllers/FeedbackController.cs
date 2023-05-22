using eShopApi.Models;
using eShopApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace ShoppingCartApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedbackController : ControllerBase
    {
        private readonly FeedbackService _feedbackService;

        public FeedbackController(FeedbackService feedbackService)
        {
            _feedbackService = feedbackService;
        }

        // POST: api/Feedback/SaveFeedDetails
        [HttpPost("SaveFeedDetails")]
        public async Task<IActionResult> SaveFeedDetails([FromBody] Feedback feedback)
        {
            bool result = await _feedbackService.SaveFeedDetailsAsync(feedback);
            if (result)
            {
                return Ok("Feedback added successfully");
            }
            else
            {
                return BadRequest("Failed to add feedback");
            }
        }

        // GET: api/Feedback/GetAllFeedDetails
        [HttpGet("GetAllFeedDetails")]
        public async Task<ActionResult<IEnumerable<Feedback>>> GetAllFeedDetails()
        {
            var feedbackList = await _feedbackService.GetAllFeedDetailsAsync();
            return Ok(feedbackList);
        }

        // GET: api/Feedback/GetFeedDetails/5
        [HttpGet("GetFeedDetails/{id}")]
        public async Task<ActionResult<Feedback>> GetFeedDetails(int id)
        {
            var feedback = await _feedbackService.GetFeedDetailsAsync(id);
            if (feedback == null)
            {
                return NotFound();
            }
            return Ok(feedback);
        }

        // DELETE: api/Feedback/DeleteFeedDetails/5
        [HttpDelete("DeleteFeedDetails/{id}")]
        public async Task<IActionResult> DeleteFeedDetails(int id)
        {
            bool result = await _feedbackService.DeleteFeedDetailsAsync(id);
            if (result)
            {
                return Ok("Feedback deleted successfully");
            }
            else
            {
                return BadRequest("Failed to delete feedback");
            }
        }
    }
}