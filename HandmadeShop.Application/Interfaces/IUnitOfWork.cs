using HandmadeShop.Domain.Entities;

namespace HandmadeShop.Application.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<AppUser> Users { get; }
        ICategoryRepository Categories { get; }
        IOrderRepository Orders { get; }
        IGenericRepository<OrderHistory> OrderHistories { get; }
        IGenericRepository<OrderItem> OrderItems { get; }
        IProductRepository Products { get; }
        IGenericRepository<Coupon> Coupons { get; }
        IGenericRepository<ProductOption> ProductOptions { get; }
        IGenericRepository<ProductOptionValue> ProductOptionValues { get; }
        IGenericRepository<Review> Reviews { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}