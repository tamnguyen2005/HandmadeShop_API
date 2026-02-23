using HandmadeShop.Domain.Entities;

namespace HandmadeShop.Application.Interfaces
{
    public interface IProductBuilder
    {
        IProductBuilder WithBaseInfo(string name
                                   , string description
                                   , decimal basePrice
                                   , int stockQuantity
                                   , Guid categoryId);

        IProductBuilder AddOption(string optionName, List<string> value);

        IProductBuilder AddImageURL(string url);

        Product Build();
    }
}