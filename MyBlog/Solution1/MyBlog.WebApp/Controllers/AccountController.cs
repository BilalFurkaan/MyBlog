using Microsoft.AspNetCore.Mvc;
using WebApp.Models.LoginViewModel;
using WebApp.Models.ProfileViewModel;
using WebApp.Services.UserApiService;
using WebApp.Helpers;

namespace MyBlog.WebApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserApiService _userApiService;

        public AccountController(IUserApiService userApiService)
        {
            _userApiService = userApiService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var (success, error, nickName, userId, token) = await _userApiService.LoginAsyncWithToken(model);
            if (success)
            {
                Response.Cookies.Append("access_token", token, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict,
                    Expires = DateTimeOffset.UtcNow.AddHours(10)
                });
                if (!string.IsNullOrEmpty(nickName))
                    HttpContext.Session.SetString("NickName", nickName);
                if (!string.IsNullOrEmpty(userId))
                    HttpContext.Session.SetString("UserId", userId);
                TempData["LoginSuccess"] = "Giriş başarılı!";
                return RedirectToAction("Index", "Home");
            }

            if (!string.IsNullOrEmpty(error) && error.Contains("Geçersiz e-posta veya şifre"))
                ViewBag.Error = "Kullanıcı adı veya şifre hatalı!";
            else
                ViewBag.Error = "Bir hata oluştu. Lütfen tekrar deneyin.";

            return View(model);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            if (model.Password != model.ConfirmPassword)
            {
                ViewBag.Error = "Şifreler eşleşmiyor!";
                return View(model);
            }

            var (success, error) = await _userApiService.RegisterAsync(model);
            if (success)
                return RedirectToAction("Login");

            ViewBag.Error = error;
            return View(model);
        }

        [HttpPost]
        public IActionResult Logout()
        {
            // Session'ı temizle
            HttpContext.Session.Clear();
            Response.Cookies.Delete("access_token");
            TempData["LogoutSuccess"] = "Çıkış başarılı!";
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> Profile()
        {
            var token = Request.Cookies["access_token"];
            string userId = null;
            string nickName = null;
            if (!string.IsNullOrEmpty(token))
            {
                userId = JwtHelper.GetClaimFromToken(token, "nameid");
                nickName = JwtHelper.GetClaimFromToken(token, "nickName");
            }
            ViewBag.NickName = nickName;
            ViewBag.UserId = userId;
            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Login");
            }
            var (success, error, profile) = await _userApiService.GetUserProfileAsync(userId);
            if (success && profile != null)
            {
                ViewBag.UserProfile = profile;
            }
            else
            {
                ViewBag.UserProfile = new UserProfileViewModel
                {
                    Id = userId,
                    IsProfileCompleted = false
                };
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CompleteProfile(CompleteProfileViewModel model)
        {
            var token = Request.Cookies["access_token"];
            string userId = null;
            if (!string.IsNullOrEmpty(token))
            {
                userId = JwtHelper.GetClaimFromToken(token, "nameid");
            }
            if (string.IsNullOrEmpty(userId))
            {
                TempData["ProfileError"] = "Kullanıcı bilgisi bulunamadı. Lütfen tekrar giriş yapın.";
                return RedirectToAction("Login");
            }
            model.UserId = userId;
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();
                var errorMessage = string.Join(", ", errors);
                TempData["ProfileError"] = $"Validation hatası: {errorMessage}";
                return RedirectToAction("Profile");
            }
            var (success, error) = await _userApiService.CompleteProfileAsync(model);
            if (success)
            {
                TempData["ProfileSuccess"] = "Profil başarıyla tamamlandı!";
                return RedirectToAction("Profile");
            }
            TempData["ProfileError"] = error ?? "Profil tamamlanırken bir hata oluştu.";
            return RedirectToAction("Profile");
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProfile(UpdateProfileViewModel model)
        {
            var token = Request.Cookies["access_token"];
            string userId = null;
            if (!string.IsNullOrEmpty(token))
            {
                userId = JwtHelper.GetClaimFromToken(token, "nameid");
            }
            if (string.IsNullOrEmpty(userId))
            {
                TempData["ProfileError"] = "Kullanıcı bilgisi bulunamadı. Lütfen tekrar giriş yapın.";
                return RedirectToAction("Login");
            }
            model.Id = userId;
            if (!ModelState.IsValid)
            {
                TempData["ProfileError"] = "Lütfen tüm alanları doğru şekilde doldurun.";
                return RedirectToAction("Profile");
            }
            var (success, error) = await _userApiService.UpdateProfileAsync(model);
            if (success)
            {
                TempData["ProfileSuccess"] = "Profil başarıyla güncellendi!";
                return RedirectToAction("Profile");
            }
            TempData["ProfileError"] = error ?? "Profil güncellenirken bir hata oluştu.";
            return RedirectToAction("Profile");
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            var token = Request.Cookies["access_token"];
            string userId = null;
            if (!string.IsNullOrEmpty(token))
            {
                userId = JwtHelper.GetClaimFromToken(token, "nameid");
            }
            if (string.IsNullOrEmpty(userId))
            {
                TempData["ProfileError"] = "Kullanıcı bilgisi bulunamadı. Lütfen tekrar giriş yapın.";
                return RedirectToAction("Login");
            }
            
            model.Id = userId;
            if (!ModelState.IsValid)
            {
                TempData["ProfileError"] = "Lütfen tüm alanları doğru şekilde doldurun.";
                return RedirectToAction("Profile");
            }
            
            var (success, error) = await _userApiService.ChangePasswordAsync(model);
            if (success)
            {
                TempData["ProfileSuccess"] = "Şifreniz başarıyla güncellendi!";
                return RedirectToAction("Profile");
            }
            TempData["ProfileError"] = error ?? "Şifre güncellenirken bir hata oluştu.";
            return RedirectToAction("Profile");
        }
    }
} 