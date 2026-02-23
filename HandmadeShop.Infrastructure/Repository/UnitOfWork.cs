using HandmadeShop.Application.Interfaces;
using HandmadeShop.Domain.Entities;
using HandmadeShop.Infrastructure.Persistence;

namespace HandmadeShop.Infrastructure.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly HandmadeShopDBContext _context;
        private GenericRepository<AppUser>? users;
        private CategoryRepository? categories;
        private OrderRepository? orders;
        private GenericRepository<OrderHistory>? orderHistories;
        private ProductRepository? products;
        private GenericRepository<OrderItem>? orderItems;
        private GenericRepository<ProductOption>? productOptions;
        private GenericRepository<ProductOptionValue>? productOptionValues;
        private GenericRepository<Coupon>? coupons;
        private GenericRepository<Review> reviews;

        public UnitOfWork(HandmadeShopDBContext context)
        {
            _context = context;
        }

        public IGenericRepository<AppUser> Users
        {
            get
            {
                if (users == null)
                    users = new GenericRepository<AppUser>(_context);
                return users;
            }
        }

        public ICategoryRepository Categories
        {
            get
            {
                if (categories == null)
                    categories = new CategoryRepository(_context);
                return categories;
            }
        }

        public IOrderRepository Orders
        {
            get
            {
                if (orders == null)
                {
                    orders = new OrderRepository(_context);
                }
                return orders;
            }
        }

        public IGenericRepository<OrderHistory> OrderHistories
        {
            get
            {
                if (orderHistories == null)
                {
                    orderHistories = new GenericRepository<OrderHistory>(_context);
                }
                return orderHistories;
            }
        }

        public IGenericRepository<OrderItem> OrderItems
        {
            get
            {
                if (orderItems == null)
                {
                    orderItems = new GenericRepository<OrderItem>(_context);
                }
                return orderItems;
            }
        }

        public IProductRepository Products
        {
            get
            {
                if (products == null)
                {
                    products = new ProductRepository(_context);
                }
                return products;
            }
        }

        public IGenericRepository<ProductOption> ProductOptions
        {
            get
            {
                if (productOptions == null)
                {
                    productOptions = new GenericRepository<ProductOption>(_context);
                }
                return productOptions;
            }
        }

        public IGenericRepository<ProductOptionValue> ProductOptionValues
        {
            get
            {
                if (productOptionValues == null)
                {
                    productOptionValues = new GenericRepository<ProductOptionValue>(_context);
                }
                return productOptionValues;
            }
        }

        public IGenericRepository<Coupon> Coupons
        {
            get
            {
                if (coupons == null)
                {
                    coupons = new GenericRepository<Coupon>(_context);
                }
                return coupons;
            }
        }

        public IGenericRepository<Review> Reviews
        {
            get
            {
                if (reviews == null)
                    reviews = new GenericRepository<Review>(_context);
                return reviews;
            }
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }
    }
}