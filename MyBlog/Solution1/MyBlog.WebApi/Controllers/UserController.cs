using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyBlog.Application.Dtos.UserDtos;
using MyBlog.Application.Usecasess.UserServices;

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
        [HttpPost("complete-profile")]
        public async Task<IActionResult> CompleteProfileAsync([FromBody] CompleteProfileDto completeProfileDto)
        {
            if (completeProfileDto == null || string.IsNullOrEmpty(completeProfileDto.UserId))
            {
                return BadRequest("Invalid profile data.");
            }

            var result = await _userService.CompleteProfileAsync(completeProfileDto);
            return Ok(result);
        }
        [HttpGet("profile/{userId}")]
        public async Task<IActionResult> GetProfileAsync(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return BadRequest("User ID is required.");
            }

            var profile = await _userService.GetUserByIdAsync(userId);
            if (profile == null)
            {
                return NotFound("User not found.");
            }
            return Ok(profile);
        }
        [HttpGet("all")]
        public async Task<IActionResult> GetAllAsync()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }
        [HttpPut("update")]
        public async Task<IActionResult> UpdateAsync([FromBody] UpdateUserDto updateUserDto)
        {
            if (updateUserDto == null || string.IsNullOrEmpty(updateUserDto.Id))
                return BadRequest("Invalid user data.");

            await _userService.UpdateAsync(updateUserDto);
            return NoContent();
        }
        [HttpDelete("delete/{userId}")]
        public async Task<IActionResult> DeleteAsync(string userId)
        {
            if (string.IsNullOrEmpty(userId))
                return BadRequest("User ID is required.");

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
        
        
        
    }
}
