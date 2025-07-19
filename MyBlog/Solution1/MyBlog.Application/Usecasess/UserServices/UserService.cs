using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyBlog.Application.Dtos.UserDtos;
using MyBlog.Domain.Entites;
using MyBlog.Persistence.Context;
using MyBlog.Application.Services.Jwt;

namespace MyBlog.Application.Usecasess.UserServices;

public class UserService: IUserService
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly AppDbContext _context;
    private readonly IJwtService _jwtService;

    public UserService(UserManager<User> userManager, SignInManager<User> signInManager, AppDbContext context, IJwtService jwtService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _context = context;
        _jwtService = jwtService;
    }

    public async Task<ResultUserDto> RegisterAsync(CreateUserDto createUserDto)
    {
        var user = new User
        {
            UserName = createUserDto.Email,
            Email = createUserDto.Email,
            EmailConfirmed = true, // Güncellenecek
        };
        var result = await _userManager.CreateAsync(user, createUserDto.Password); ;
        if (!result.Succeeded)
        {
            throw new Exception(string.Join(", ", result.Errors.Select(e => e.Description)));
        }

        return new ResultUserDto
        {
            Id = user.Id,
            Email = user.Email,
            CreatedAt = DateTime.UtcNow
        };
    }

    public async Task<ResultUserDto> CompleteProfileAsync(CompleteProfileDto completeProfileDto)
    {
        var user = await _userManager.FindByIdAsync(completeProfileDto.UserId);
        if (user == null)
        {
            throw new Exception("User not found");
        }
        if (!string.IsNullOrWhiteSpace(completeProfileDto.NickName))
        {
            var existingNick = await _userManager.Users.FirstOrDefaultAsync(u => u.NickName == completeProfileDto.NickName && u.Id != user.Id);
            if (existingNick != null)
            {
                throw new Exception("Bu NickName zaten kullanılıyor. Lütfen başka bir tane seçin.");
            }
            user.NickName = completeProfileDto.NickName;
        }
        user.Job = completeProfileDto.Job;
        user.About = completeProfileDto.About;

        var result = await _userManager.UpdateAsync(user);
        if (!result.Succeeded)
        {
            throw new Exception(string.Join(", ", result.Errors.Select(e => e.Description)));
        }
        return new ResultUserDto
        {
            Id = user.Id,
            NickName = user.NickName,
            Email = user.Email,
            Job = user.Job,
            About = user.About,
            IsProfileCompleted = !string.IsNullOrWhiteSpace(user.NickName) && 
                                !string.IsNullOrWhiteSpace(user.Job) && 
                                !string.IsNullOrWhiteSpace(user.About),
            CreatedAt = user.CreatedAt
        };
    }

    public async Task<string> LoginAsync(LoginUserDto loginUserDto)
    {
        var user = await _userManager.FindByEmailAsync(loginUserDto.Email);
        if (user == null)
        {
            throw new Exception("Kullanıcı bulunamadı");
        }
        if(!user.EmailConfirmed)
        {
            throw new Exception("Email onaylanmamış");
        }
        var result = await _signInManager.CheckPasswordSignInAsync(user, loginUserDto.Password, false);
        if (!result.Succeeded)
        {
            throw new Exception("Geçersiz e-posta veya şifre");
        }
        var token = _jwtService.GenerateToken(user);
        return token;
    }

    public async Task<ResultUserDto> GetUserByIdAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        { 
            throw new Exception("Kullanıcı bulunamadı!");
        }
        var articleCount = await _context.Articles.CountAsync(a => a.UserId == user.Id);
        var commentCount = await _context.Comments.CountAsync(c => c.UserId == user.Id);
        return new ResultUserDto
        {
            Id = user.Id,
            NickName = user.NickName,
            Email = user.Email,
            Job = user.Job,
            About = user.About,
            IsProfileCompleted = !string.IsNullOrWhiteSpace(user.NickName) && 
                                !string.IsNullOrWhiteSpace(user.Job) && 
                                !string.IsNullOrWhiteSpace(user.About),
            ArticleCount = articleCount,
            CommentCount = commentCount,
            CreatedAt = user.CreatedAt,
        };
    }

    public async Task<List<ResultUserDto>> GetAllUsersAsync()
    {
        var users=await _userManager.Users.ToListAsync();
        var result = new List<ResultUserDto>();
        foreach (var user in users)
        {
            var articleCount = await _context.Articles.CountAsync(a => a.UserId == user.Id);
            var commentCount = await _context.Comments.CountAsync(c => c.UserId == user.Id);
            result.Add(new ResultUserDto
            {
                Id = user.Id,
                NickName = user.NickName,
                Email = user.Email,
                Job = user.Job,
                About = user.About,
                IsProfileCompleted = !string.IsNullOrWhiteSpace(user.NickName) && 
                                    !string.IsNullOrWhiteSpace(user.Job) && 
                                    !string.IsNullOrWhiteSpace(user.About),
                ArticleCount = articleCount,
                CommentCount = commentCount,
                CreatedAt = user.CreatedAt,
            });
        }
        return result;
    }

    public async Task UpdateAsync(UpdateUserDto updateUserDto)
    {
        var user = await _userManager.FindByIdAsync(updateUserDto.Id);
        if (user == null)
            throw new Exception("Kullanıcı bulunamadı!");

        if (!string.IsNullOrWhiteSpace(updateUserDto.NickName))
        {
            var existingNick = await _userManager.Users.FirstOrDefaultAsync(u => u.NickName == updateUserDto.NickName && u.Id != user.Id);
            if (existingNick != null)
            {
                throw new Exception("Bu NickName zaten kullanılıyor. Lütfen başka bir tane seçin.");
            }
            user.NickName = updateUserDto.NickName;
        }
        if (!string.IsNullOrWhiteSpace(updateUserDto.Job))
            user.Job = updateUserDto.Job;
        if (!string.IsNullOrWhiteSpace(updateUserDto.About))
            user.About = updateUserDto.About;
        if (!string.IsNullOrWhiteSpace(updateUserDto.Email))
        {
            user.Email = updateUserDto.Email;
            user.UserName = updateUserDto.Email; // Email değişirse UserName de güncellenir
        }

        // Şifre değişikliği
        if (!string.IsNullOrWhiteSpace(updateUserDto.NewPassword))
        {
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var result = await _userManager.ResetPasswordAsync(user, token, updateUserDto.NewPassword);
            if (!result.Succeeded)
                throw new Exception(string.Join(", ", result.Errors.Select(e => e.Description)));
        }

        var updateResult = await _userManager.UpdateAsync(user);
        if (!updateResult.Succeeded)
            throw new Exception(string.Join(", ", updateResult.Errors.Select(e => updateResult.Errors.Select(e => e.Description))));
    }

    public async Task DeleteAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
            throw new Exception("Kullanıcı bulunamadı!");

        var result = await _userManager.DeleteAsync(user);
        if (!result.Succeeded)
            throw new Exception(string.Join(", ", result.Errors.Select(e => e.Description)));
    }
}