//using HandmadeShop.Application.Events.Reviews;
//using HandmadeShop.Application.Interfaces;
//using HandmadeShop.Application.Patterns.Observers;

//namespace HandmadeShop.Application.Handlers.Rating
//{
//    public class RatingHandler : IHandmadeObserver<ReviewCreatedEvent>
//    {
//        private readonly IUnitOfWork _unitOfWork;

// public RatingHandler(IUnitOfWork unitOfWork) { _unitOfWork = unitOfWork; }

//        public async Task HandleAsync(ReviewCreatedEvent domainEvent)
//        {
//            var product = await _unitOfWork.Products.GetByIdAsync(domainEvent.review.ProductId);
//            var totalScore = (product.ReviewCount * product.AverageRating) + domainEvent.review.Rating;
//            product.ReviewCount++;
//            product.AverageRating = Math.Round(totalScore / product.ReviewCount, 1);
//            _unitOfWork.Products.Update(product);
//        }
//    }
//}