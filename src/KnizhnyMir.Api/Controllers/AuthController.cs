using KnizhnyMir.Api.Dtos;
using KnizhnyMir.DataAccess.Models;
using KnizhnyMir.DataAccess.Services;
using Microsoft.AspNetCore.Mvc;

namespace KnizhnyMir.Api.Controllers
{
    /// <summary>API авторизации пользователей.</summary>
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController(AuthService authService) : ControllerBase
    {
        private readonly AuthService _authService = authService;

        /// <summary>Выполняет вход по логину и паролю.</summary>
        [HttpPost("login")]
        public ActionResult<User> Login(LoginRequest request)
        {
            var user = _authService.Login(request.Login, request.Password);
            return user is null ? Unauthorized("Неверный логин или пароль.") : Ok(user);
        }
    }
}
