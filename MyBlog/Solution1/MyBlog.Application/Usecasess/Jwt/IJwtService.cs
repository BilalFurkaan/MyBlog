using MyBlog.Domain.Entites;

namespace MyBlog.Application.Services.Jwt;

public interface IJwtService
{
    string GenerateToken(User user);
} 