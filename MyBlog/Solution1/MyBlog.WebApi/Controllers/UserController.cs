using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyBlog.Application.Dtos.UserDtos;
using MyBlog.Application.Usecasess.UserServices;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace MyBlog.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync([FromBody] CreateUserDto createUserDto)
        {
            if (createUserDto == null)
            {
                return BadRequest("Invalid user data.");
            }

            var result = await _userService.RegisterAsync(createUserDto);
            return Ok(result);
        }
        [Authorize]
        [HttpPost("complete-profile")]
        public async Task<IActionResult> CompleteProfileAsync([FromBody] CompleteProfileDto completeProfileDto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (completeProfileDto == null)
                return BadRequest("Invalid profile data.");

            completeProfileDto.UserId = userId;

            var result = await _userService.CompleteProfileAsync(completeProfileDto);
            return Ok(result);
        }
        [Authorize]
        [HttpGet("profile")]
        public async Task<IActionResult> GetProfileAsync()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            var profile = await _userService.GetUserByIdAsync(userId);
            if (profile == null)
                return NotFound("User not found.");
            return Ok(profile);
        }
        [HttpGet("all")]
        public async Task<IActionResult> GetAllAsync()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }
        [Authorize]
        [HttpPut("update")]
        public async Task<IActionResult> UpdateAsync([FromBody] UpdateUserDto updateUserDto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (updateUserDto == null || string.IsNullOrEmpty(updateUserDto.Id))
                return BadRequest("Invalid user data.");

            if (updateUserDto.Id != userId)
                return Forbid("Sadece kendi profilinizi güncelleyebilirsiniz.");

            await _userService.UpdateAsync(updateUserDto);
            return NoContent();
        }
        [Authorize]
        [HttpDelete("delete/{userId}")]
        public async Task<IActionResult> DeleteAsync(string userId)
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId != currentUserId)
                return Forbid("Sadece kendi hesabınızı silebilirsiniz.");

            await _userService.DeleteAsync(userId);
            return NoContent();
        }
        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginUserDto loginUserDto)
        {
            if (loginUserDto == null)
                return BadRequest("Invalid login data.");

            var token = await _userService.LoginAsync(loginUserDto);
            // Kullanıcıyı email ile bul ve bilgilerini al
            var allUsers = await _userService.GetAllUsersAsync();
            var user = allUsers.FirstOrDefault(u => u.Email == loginUserDto.Email);
            
            if (user != null)
            {
                return Ok(new { 
                    token, 
                    nickName = user.NickName,
                    userId = user.Id 
                });
            }
            
            return Ok(new { token, nickName = "", userId = "" });
        }
        [Authorize]
        [HttpPut("update-password")]
        public async Task<IActionResult> UpdatePasswordAsync([FromBody] ChangePasswordDto changePasswordDto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (changePasswordDto == null || string.IsNullOrEmpty(changePasswordDto.Id))
                return BadRequest("Invalid password data.");

            if (changePasswordDto.Id != userId)
                return Forbid("Sadece kendi şifrenizi güncelleyebilirsiniz.");

            await _userService.UpdatePasswordAsync(changePasswordDto);
            return NoContent();
        }
        
        
        
    }
}
