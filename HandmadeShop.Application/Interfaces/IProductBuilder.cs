using HandmadeShop.Domain.Entities;

namespace HandmadeShop.Application.Interfaces
{
    public interface IProductBuilder
    {
        IProductBuilder AddName(string name);

        IProductBuilder AddDescription(string description);

        IProductBuilder AddBasePrice(decimal basePrice);

        IProductBuilder AddStockQuantity(int quantity);

        IProductBuilder AddStoryBehind(string storyBehind);

        IProductBuilder AddCategoryId(Guid categoryId);

        IProductBuilder AddOption(string optionName, List<string> value);

        IProductBuilder AddImageURL(string url);

        IProductBuilder AddSubImage(List<string> url);

        Product Build();
    }
}