using Microsoft.AspNetCore.Mvc;
using WebApp.Models.LoginViewModel;
using WebApp.Models.ProfileViewModel;
using WebApp.Services.UserApiService;

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

            var (success, error, nickName, userId) = await _userApiService.LoginAsync(model);
            if (success)
            {
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
            
            // Başarı mesajı göster
            TempData["LogoutSuccess"] = "Başarıyla çıkış yaptınız.";
            
            // Anasayfaya yönlendir
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> Profile()
        {
            // Kullanıcı giriş yapmış mı kontrol et (sadece UserId'ye bak)
            var userId = HttpContext.Session.GetString("UserId");
            
            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Login");
            }

            // Kullanıcının profil bilgilerini getir
            var (success, error, profile) = await _userApiService.GetUserProfileAsync(userId);
            
            if (success && profile != null)
            {
                ViewBag.UserProfile = profile;
            }
            else
            {
                // API çağrısı başarısız olursa, boş profil oluştur
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

            // Session'dan UserId'yi al
            var userId = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userId))
            {
                TempData["ProfileError"] = "Kullanıcı bilgisi bulunamadı. Lütfen tekrar giriş yapın.";
                return RedirectToAction("Login");
            }

            model.UserId = userId;

            var (success, error) = await _userApiService.CompleteProfileAsync(model);
            if (success)
            {
                // Session'ı güncelle
                HttpContext.Session.SetString("NickName", model.NickName);
                TempData["ProfileSuccess"] = "Profil başarıyla tamamlandı!";
                return RedirectToAction("Profile");
            }

            TempData["ProfileError"] = error ?? "Profil tamamlanırken bir hata oluştu.";
            return RedirectToAction("Profile");
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProfile(UpdateProfileViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["ProfileError"] = "Lütfen tüm alanları doğru şekilde doldurun.";
                return RedirectToAction("Profile");
            }

            // Session'dan UserId'yi al
            var userId = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userId))
            {
                TempData["ProfileError"] = "Kullanıcı bilgisi bulunamadı. Lütfen tekrar giriş yapın.";
                return RedirectToAction("Login");
            }

            model.Id = userId;

            var (success, error) = await _userApiService.UpdateProfileAsync(model);
            if (success)
            {
                // NickName güncellendiyse session'ı güncelle
                if (!string.IsNullOrEmpty(model.NickName))
                {
                    HttpContext.Session.SetString("NickName", model.NickName);
                }
                
                TempData["ProfileSuccess"] = "Profil başarıyla güncellendi!";
                return RedirectToAction("Profile");
            }

            TempData["ProfileError"] = error ?? "Profil güncellenirken bir hata oluştu.";
            return RedirectToAction("Profile");
        }
    }
} 