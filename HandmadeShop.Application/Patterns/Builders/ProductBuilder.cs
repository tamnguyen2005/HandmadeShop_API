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

        public IProductBuilder AddBasePrice(decimal basePrice)
        {
            _product.BasePrice = basePrice;
            return this;
        }

        public IProductBuilder AddCategoryId(Guid categoryId)
        {
            _product.CategoryId = categoryId;
            return this;
        }

        public IProductBuilder AddDescription(string description)
        {
            _product.Description = description;
            return this;
        }

        public IProductBuilder AddImageURL(string url)
        {
            _product.ImageURL = url;
            return this;
        }

        public IProductBuilder AddName(string name)
        {
            _product.Name = name;
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

        public IProductBuilder AddStockQuantity(int quantity)
        {
            _product.StockQuantity = quantity;
            return this;
        }

        public IProductBuilder AddStoryBehind(string storyBehind)
        {
            _product.StoryBehind = storyBehind;
            return this;
        }

        public IProductBuilder AddSubImage(List<string> url)
        {
            List<SubImage> subImages = new List<SubImage>();
            foreach (var i in url)
            {
                SubImage subImage = new SubImage()
                {
                    Id = Guid.NewGuid(),
                    Url = i,
                    ProductId = _product.Id
                };
                subImages.Add(subImage);
            }
            _product.SubImages = subImages;
            return this;
        }

        public Product Build()
        {
            return _product;
        }
    }
}