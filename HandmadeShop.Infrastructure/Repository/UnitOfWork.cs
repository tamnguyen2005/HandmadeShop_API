using HandmadeShop.Application.Interfaces;
using HandmadeShop.Domain.Entities;
using HandmadeShop.Infrastructure.Persistence;

namespace HandmadeShop.Infrastructure.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly HandmadeShopDBContext _context;
        private GenericRepository<AppUser>? users;
        private GenericRepository<Category>? categories;
        private GenericRepository<Order>? orders;
        private GenericRepository<OrderHistory>? orderHistories;
        private GenericRepository<Product>? products;
        private GenericRepository<OrderItem>? orderItems;
        private GenericRepository<ProductOption>? productOptions;
        private GenericRepository<ProductOptionValue>? productOptionValues;

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

        public IGenericRepository<Category> Categories
        {
            get
            {
                if (categories == null)
                    categories = new GenericRepository<Category>(_context);
                return categories;
            }
        }

        public IGenericRepository<Order> Orders
        {
            get
            {
                if (orders == null)
                {
                    orders = new GenericRepository<Order>(_context);
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

        public IGenericRepository<Product> Products
        {
            get
            {
                if (products == null)
                {
                    products = new GenericRepository<Product>(_context);
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