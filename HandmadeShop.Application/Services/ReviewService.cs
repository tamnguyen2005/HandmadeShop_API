using HandmadeShop.Application.DTOs.Review;
using HandmadeShop.Application.Events.Reviews;
using HandmadeShop.Application.Interfaces;
using HandmadeShop.Application.Patterns.Observers;
using HandmadeShop.Domain.Entities;

namespace HandmadeShop.Application.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPhotoService _photoService;
        private readonly ICurrentUserService _currentUserService;
        private readonly EventDispatcher _eventDispatcher;

        public ReviewService(IUnitOfWork unitOfWork, IPhotoService photoService, ICurrentUserService currentUserService, EventDispatcher eventDispatcher)
        {
            _unitOfWork = unitOfWork;
            _photoService = photoService;
            _currentUserService = currentUserService;
            _eventDispatcher = eventDispatcher;
        }

        public async Task AddReviewAsync(CreateReviewRequest request)
        {
            var userIdString = _currentUserService.UserId;
            var product = await _unitOfWork.Products.GetByIdAsync(request.ProductId);
            if (string.IsNullOrEmpty(userIdString))
                throw new UnauthorizedAccessException("Please log in first !");
            if (product == null)
                throw new KeyNotFoundException("Product does not exist !");
            var userId = Guid.Parse(userIdString);
            var user = await _unitOfWork.Users.GetByIdAsync(userId);
            var review = new Review()
            {
                Rating = request.Rating,
                Content = request.Content,
                ProductId = request.ProductId,
                UserId = userId,
                UserName = user?.FullName ?? "Anonymus",
                ImageURL = request.ImageURL != null ? await _photoService.UploadAsync(request.ImageURL) : ""
            };
            await _unitOfWork.Reviews.AddAsync(review);
            var reviewEvent = new ReviewCreatedEvent(new ReviewEvent(request.ProductId, request.Rating));
            await _eventDispatcher.PublishAsync(reviewEvent);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteReviewAsync(Guid reviewId)
        {
            var userIdString = _currentUserService.UserId;
            if (string.IsNullOrEmpty(userIdString))
                throw new UnauthorizedAccessException("Please log in first !");
            var userId = Guid.Parse(userIdString);
            var review = await _unitOfWork.Reviews.GetByIdAsync(reviewId);
            if (review == null)
                throw new KeyNotFoundException("Review does not exist !");
            if (review.UserId != userId)
                throw new Exception("You can not delete other people's review !");
            _unitOfWork.Reviews.Delete(review);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}