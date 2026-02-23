using HandmadeShop.Application.DTOs.Review;

namespace HandmadeShop.Application.Interfaces
{
    public interface IReviewService
    {
        Task AddReviewAsync(CreateReviewRequest request);

        Task DeleteReviewAsync(Guid reviewId);
    }
}