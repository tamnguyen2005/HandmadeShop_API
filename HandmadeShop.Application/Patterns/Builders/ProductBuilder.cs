using HandmadeShop.Application.Interfaces;
using HandmadeShop.Domain.Entities;

namespace HandmadeShop.Application.Patterns.Builders
{
    public class ProductBuilder : IProductBuilder
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

        public IProductBuilder WithBaseInfo(string name
                                          , string description
                                          , decimal basePrice
                                          , int stockQuantity
                                          , string storyBehind
                                          , Guid categoryId)
        {
            _product.Name = name;
            _product.Description = description;
            _product.BasePrice = basePrice;
            _product.StockQuantity = stockQuantity;
            _product.CategoryId = categoryId;
            _product.StoryBehind = storyBehind;
            return this;
        }

        public IProductBuilder AddImageURL(string url)
        {
            _product.ImageURL = url;
            return this;
        }

        public IProductBuilder AddOption(string optionName, List<string> value)
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