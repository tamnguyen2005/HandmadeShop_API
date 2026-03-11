//using HandmadeShop.Application.DTOs.Review;
//using HandmadeShop.Application.Interfaces;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;

//namespace HandmadeShop.API.Controllers
//{
//    [ApiController]
//    [Route("api/Review")]
//    public class ReviewController : ControllerBase
//    {
//        private readonly IReviewService _reviewService;

// public ReviewController(IReviewService reviewService) { _reviewService = reviewService; }

// [HttpPost] [Authorize] public async Task<IActionResult> CreateReview([FromForm]
// CreateReviewRequest request) { await _reviewService.AddReviewAsync(request); return NoContent(); }

//        [HttpDelete("{id}")]
//        [Authorize]
//        public async Task<IActionResult> DeleteReview(Guid reviewId)
//        {
//            await _reviewService.DeleteReviewAsync(reviewId);
//            return NoContent();
//        }
//    }
//}