using HandmadeShop.Application.DTOs.Review;
using HandmadeShop.Application.Patterns.Observers;

namespace HandmadeShop.Application.Events.Reviews
{
    public class ReviewCreatedEvent : IDomainEvent
    {
        public ReviewEvent review;

        public ReviewCreatedEvent(ReviewEvent review)
        {
            this.review = review;
        }
    }
}