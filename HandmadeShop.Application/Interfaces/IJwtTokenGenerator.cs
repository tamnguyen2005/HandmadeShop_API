using HandmadeShop.Domain.Entities;

namespace HandmadeShop.Application.Interfaces
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(AppUser user);
    }
}