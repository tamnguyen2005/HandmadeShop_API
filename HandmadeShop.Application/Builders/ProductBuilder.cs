using HandmadeShop.Domain.Entities;

namespace HandmadeShop.Application.Builders
{
    public class ProductBuilder
    {
        private Product _product;

        public ProductBuilder()
        {
            _product = new Product()
            {
                Id = Guid.NewGuid(),
                Options = new List<ProductOption>()
            };
        }

        public ProductBuilder WithBaseInfo(string name
                                          , string description
                                          , decimal basePrice
                                          , int stockQuantity
                                          , Guid categoryId)
        {
            _product.Name = name;
            _product.Description = description;
            _product.BasePrice = basePrice;
            _product.StockQuantity = stockQuantity;
            _product.CategoryId = categoryId;
            return this;
        }

        public ProductBuilder AddOption(string optionName, List<string> value)
        {
            var option = new ProductOption()
            {
                Id = Guid.NewGuid(),
                Name = optionName,
                ProductId = _product.Id,
                Values = value.Select(v => new ProductOptionValue()
                {
                    Id = Guid.NewGuid(),
                    Value = v
                }).ToList()
            };
            _product.Options.Add(option);
            return this;
        }

        public Product Build()
        {
            return _product;
        }
    }
}