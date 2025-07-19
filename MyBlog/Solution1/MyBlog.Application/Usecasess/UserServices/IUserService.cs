using MyBlog.Application.Dtos.UserDtos;

namespace MyBlog.Application.Usecasess.UserServices;

public interface IUserService
{
    Task<ResultUserDto>RegisterAsync(CreateUserDto createUserDto);
    Task<ResultUserDto>CompleteProfileAsync(CompleteProfileDto completeProfileDto);
    Task <string> LoginAsync(LoginUserDto loginUserDto);
    Task<ResultUserDto> GetUserByIdAsync(string userId);
    Task<List<ResultUserDto>> GetAllUsersAsync();
    Task UpdateAsync(UpdateUserDto updateUserDto);
    Task DeleteAsync(string userId);
    
}