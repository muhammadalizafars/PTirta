using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PTirta.DTOs;
using PTirta.Models;
using PTirta.Services;
using System.Text;

namespace PTirta.Controllers.Api
{
    [ApiController]
    [Route("api/v1/auth")]
    [Produces("application/json")]
    public class AuthApiController : ControllerBase
    {
        private readonly SignInManager<Users> _signInManager;
        private readonly UserManager<Users> _userManager;
        private readonly IEmailService _emailService;
        private static readonly Dictionary<string, (string Email, DateTime ExpiresAt)> _resetTokens = new();

        public AuthApiController(SignInManager<Users> signInManager, UserManager<Users> userManager, IEmailService emailService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _emailService = emailService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new AuthResponseDto
                {
                    Success = false,
                    Message = "Validasi data gagal.",
                    Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList()
                });
            }

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return Unauthorized(new AuthResponseDto
                {
                    Success = false,
                    Message = "Email atau kata sandi tidak valid."
                });
            }

            var result = await _signInManager.PasswordSignInAsync(user.UserName!, model.Password, model.RememberMe, false);

            if (result.Succeeded)
            {
                return Ok(new AuthResponseDto
                {
                    Success = true,
                    Message = "Login berhasil.",
                    User = new UserDto
                    {
                        Id = user.Id,
                        FullName = user.FullName ?? user.UserName ?? string.Empty,
                        Email = user.Email ?? string.Empty,
                        UserName = user.UserName ?? string.Empty
                    }
                });
            }

            return Unauthorized(new AuthResponseDto
            {
                Success = false,
                Message = "Email atau kata sandi tidak valid."
            });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new AuthResponseDto
                {
                    Success = false,
                    Message = "Validasi data gagal.",
                    Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList()
                });
            }

            if (model.Password != model.ConfirmPassword)
            {
                return BadRequest(new AuthResponseDto
                {
                    Success = false,
                    Message = "Konfirmasi kata sandi tidak cocok."
                });
            }

            var existingUser = await _userManager.FindByEmailAsync(model.Email);
            if (existingUser != null)
            {
                return Conflict(new AuthResponseDto
                {
                    Success = false,
                    Message = "Email sudah terdaftar."
                });
            }

            var user = new Users
            {
                FullName = model.Name,
                Email = model.Email,
                UserName = model.Email
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                return Ok(new AuthResponseDto
                {
                    Success = true,
                    Message = "Registrasi akun berhasil.",
                    User = new UserDto
                    {
                        Id = user.Id,
                        FullName = user.FullName,
                        Email = user.Email,
                        UserName = user.UserName
                    }
                });
            }

            return BadRequest(new AuthResponseDto
            {
                Success = false,
                Message = "Gagal membuat akun.",
                Errors = result.Errors.Select(e => e.Description).ToList()
            });
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok(new { success = true, message = "Logout berhasil." });
        }

        [HttpGet("me")]
        public async Task<IActionResult> GetCurrentUser()
        {
            if (_signInManager.IsSignedIn(User))
            {
                var user = await _userManager.GetUserAsync(User);
                if (user != null)
                {
                    return Ok(new AuthResponseDto
                    {
                        Success = true,
                        Message = "Pengguna terautentikasi.",
                        User = new UserDto
                        {
                            Id = user.Id,
                            FullName = user.FullName ?? user.UserName ?? string.Empty,
                            Email = user.Email ?? string.Empty,
                            UserName = user.UserName ?? string.Empty
                        }
                    });
                }
            }

            return Ok(new AuthResponseDto
            {
                Success = false,
                Message = "Pengguna belum login."
            });
        }

        private static string Base64UrlEncode(string input)
        {
            var bytes = Encoding.UTF8.GetBytes(input);
            return Convert.ToBase64String(bytes).TrimEnd('=').Replace('+', '-').Replace('/', '_');
        }

        private static string Base64UrlDecode(string input)
        {
            var padded = input.Replace('-', '+').Replace('_', '/');
            switch (padded.Length % 4)
            {
                case 2: padded += "=="; break;
                case 3: padded += "="; break;
            }
            var bytes = Convert.FromBase64String(padded);
            return Encoding.UTF8.GetString(bytes);
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordRequestDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new AuthResponseDto
                {
                    Success = false,
                    Message = "Email tidak valid."
                });
            }

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return Ok(new AuthResponseDto
                {
                    Success = true,
                    Message = "Jika email terdaftar, instruksi reset kata sandi telah dikirim."
                });
            }

            var rawToken = Guid.NewGuid().ToString("N");
            var emailEncoded = Base64UrlEncode(model.Email);
            var token = emailEncoded + "." + rawToken;
            _resetTokens[token] = (model.Email, DateTime.UtcNow.AddMinutes(15));

            var resetLink = $"http://localhost:5173/reset-password/{token}";
            await _emailService.SendPasswordResetEmailAsync(model.Email, token, resetLink);

            return Ok(new AuthResponseDto
            {
                Success = true,
                Message = "Jika email terdaftar, instruksi reset kata sandi telah dikirim."
            });
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequestDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new AuthResponseDto
                {
                    Success = false,
                    Message = "Data tidak valid."
                });
            }

            string email;
            try
            {
                var parts = model.Token.Split('.');
                if (parts.Length != 2)
                    return BadRequest(new AuthResponseDto { Success = false, Message = "Token tidak valid." });
                email = Base64UrlDecode(parts[0]);
            }
            catch
            {
                return BadRequest(new AuthResponseDto { Success = false, Message = "Token tidak valid." });
            }

            if (!_resetTokens.TryGetValue(model.Token, out var tokenData))
            {
                return BadRequest(new AuthResponseDto
                {
                    Success = false,
                    Message = "Token tidak valid atau sudah kadaluarsa."
                });
            }

            if (tokenData.ExpiresAt < DateTime.UtcNow)
            {
                _resetTokens.Remove(model.Token);
                return BadRequest(new AuthResponseDto
                {
                    Success = false,
                    Message = "Token sudah kadaluarsa."
                });
            }

            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return BadRequest(new AuthResponseDto
                {
                    Success = false,
                    Message = "Pengguna tidak ditemukan."
                });
            }

            var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
            var result = await _userManager.ResetPasswordAsync(user, resetToken, model.NewPassword);

            if (!result.Succeeded)
            {
                return BadRequest(new AuthResponseDto
                {
                    Success = false,
                    Message = "Gagal mengatur ulang kata sandi.",
                    Errors = result.Errors.Select(e => e.Description).ToList()
                });
            }

            _resetTokens.Remove(model.Token);

            return Ok(new AuthResponseDto
            {
                Success = true,
                Message = "Kata sandi berhasil diatur ulang. Silakan masuk dengan kata sandi baru."
            });
        }
    }
}

