using ClinicApplication.Application.Services.Base;
using ClinicApplication.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Concurrent;

namespace ClinicApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _db;
        private readonly IEmailService _emailService;

        // تخزين مؤقت للأكواد مرتبطة بالبريد الإلكتروني
        private static ConcurrentDictionary<string, string> verificationCodes = new ConcurrentDictionary<string, string>();

        public AuthController(AppDbContext db, IEmailService emailService)
        {
            _db = db;
            _emailService = emailService;
        }

        // 1. الخطوة الأولى: استقبال الإيميل والباسورد، التحقق منهما، وإرسال الكود
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto model)
        {
            var user = _db.Users.FirstOrDefault(x => x.Email == model.Email);

            if (user == null)
            {
                return BadRequest(new { message = "Invalid Email or Password" });
            }

            bool isPasswordCorrect = BCrypt.Net.BCrypt.Verify(model.Password, user.Password);

            if (!isPasswordCorrect)
            {
                return BadRequest(new { message = "Invalid Email or Password" });
            }

            if (user.IsLocked)
            {
                return BadRequest(new { message = "Your account is locked" });
            }

            // توليد كود عشوائي من 4 أرقام
            Random random = new Random();
            string code = random.Next(1000, 9999).ToString();

            // حفظ الكود مؤقتاً مرتبطاً بالبريد
            verificationCodes[model.Email] = code;

            // إرسال الكود عبر الإيميل
            string subject = "Your Verification Code";
            string message = $"<h3>Your 4-digit verification code is: <b>{code}</b></h3>";

            try
            {
                await _emailService.SendEmailAsync(model.Email, subject, message);
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "Failed to send verification email. Please try again." });
            }

            // الرد بأن الود أُرسل بنجاح ليفتح تطبيق الفلاتر شاشة إدخال الكود
            return Ok(new { message = "Verification code sent to your email", success = true, email = model.Email });
        }

        // 2. الخطوة الثانية: استقبال الكود من تطبيق الفلاتر والتحقق منه لتمكين الدخول النهائي
        [HttpPost("VerifyCode")]
        public IActionResult VerifyCode([FromBody] VerifyCodeDto model)
        {
            if (verificationCodes.TryGetValue(model.Email, out string savedCode))
            {
                if (savedCode == model.Code)
                {
                    // تم التحقق بنجاح، نحذف الكود المؤقت
                    verificationCodes.TryRemove(model.Email, out _);

                    var user = _db.Users.FirstOrDefault(x => x.Email == model.Email);

                    // إرجاع بيانات المستخدم لتطبيق الفلاتر لينتقل للصفحة الرئيسية
                    return Ok(new
                    {
                        message = "Login successful",
                        success = true,
                        username = user?.Email, // أو اسم المستخدم حسب جدولك
                        id = user?.Id
                    });
                }
            }

            return BadRequest(new { message = "Invalid or expired verification code", success = false });
        }
    }

    // الـ DTOs الخاصة باستقبال البيانات من تطبيق الفلاتر
    public class LoginRequestDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class VerifyCodeDto
    {
        public string Email { get; set; }
        public string Code { get; set; }
    }
}