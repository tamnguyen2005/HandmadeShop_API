using HandmadeShop.Application.DTOs.Coupon;
using HandmadeShop.Application.Interfaces;
using HandmadeShop.Domain.Entities;

namespace HandmadeShop.Application.Services
{
    public class CouponService : ICouponService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CouponService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task AddNewCoupon(CreateCouponRequest request)
        {
            var coupon = new Coupon()
            {
                Id = new Guid(),
                Code = request.Code,
                Type = request.Type,
                Value = request.Value,
                MinOrderAmount = request.MinOrderAmount.HasValue ? request.MinOrderAmount : 0,
                ExpiryDate = DateTime.UtcNow.AddDays(request.ExpireAfter),
                UsageLimit = request.UsageLimit,
                UsageCount = 0
            };
            await _unitOfWork.Coupons.AddAsync(coupon);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteCoupon(Guid Id)
        {
            var coupon = await _unitOfWork.Coupons.GetByIdAsync(Id);
            if (coupon == null)
                throw new KeyNotFoundException("Coupon does not exist !");
            coupon.IsDeleted = true;
            _unitOfWork.Coupons.Update(coupon);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<List<CouponResponse>> GetCoupons()
        {
            var coupons = await _unitOfWork.Coupons.GetAllAsync();
            var validCoupons = coupons.Where(c => c.IsDeleted == false && c.ExpiryDate > DateTime.Now).ToList();
            var result = validCoupons.Select(c => new CouponResponse()
            {
                Id = c.Id,
                Value = c.Value,
                MinOrderAmount = c.MinOrderAmount ?? 0,
            }).ToList();
            return result;
        }

        public async Task UpdateCoupon(Guid Id, UpdateCouponRequest request)
        {
            var coupon = await _unitOfWork.Coupons.GetByIdAsync(Id);
            if (coupon == null)
                throw new KeyNotFoundException("Coupon does not exist !");
            if (string.IsNullOrEmpty(request.Code))
            {
                coupon.Code = request.Code;
            }
            if (string.IsNullOrEmpty(request.Type))
            {
                coupon.Type = request.Type;
            }
            if (request.Value.HasValue)
            {
                coupon.Value = request.Value.Value;
            }
            if (request.MinOrderAmount.HasValue)
            {
                coupon.MinOrderAmount = request.MinOrderAmount.Value;
            }
            if (request.ExpiryDate.HasValue)
            {
                coupon.ExpiryDate = request.ExpiryDate.Value;
            }
            if (request.UsageLimit.HasValue)
            {
                coupon.UsageLimit = request.UsageLimit.Value;
            }
            _unitOfWork.Coupons.Update(coupon);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}