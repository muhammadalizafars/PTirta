using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PTirta.DTOs;
using PTirta.Models;

namespace PTirta.Controllers.Api
{
    [ApiController]
    [Route("api/v1/auth")]
    [Produces("application/json")]
    public class AuthApiController : ControllerBase
    {
        private readonly SignInManager<Users> _signInManager;
        private readonly UserManager<Users> _userManager;

        public AuthApiController(SignInManager<Users> signInManager, UserManager<Users> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
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
    }
}

