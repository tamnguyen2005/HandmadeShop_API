using HandmadeShop.Application.DTOs.Product;
using HandmadeShop.Domain.Entities;

namespace HandmadeShop.Application.Specifications
{
    public class ProductSpecification : BaseSpecification<Product>
    {
        public ProductSpecification(QueryProductRequest request)
            : base(x =>
                  (string.IsNullOrEmpty(request.Name) || x.Name.Contains(request.Name)) &&
                  (!request.CategoryId.HasValue || x.CategoryId == request.CategoryId))
        {
            Includes.Add(x => x.Category);
            switch (request.Sort)
            {
                case "priceAsc": AddOrderBy(x => x.BasePrice); break;
                case "priceDesc": AddOrderByDescending(x => x.BasePrice); break;
                default: AddOrderBy(x => x.Id); break;
            }
            var skip = (request.PageNumber - 1) * request.PageSize;
            ApplyPaging(skip ?? 0, request.PageSize ?? 10);
        }
    }
}