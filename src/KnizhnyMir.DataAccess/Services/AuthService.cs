using KnizhnyMir.DataAccess.Models;
using KnizhnyMir.DataAccess.Repositories;

namespace KnizhnyMir.DataAccess.Services
{
    /// <summary>Бизнес-логика авторизации пользователей.</summary>
    public class AuthService(UserRepository userRepository)
    {
        private readonly UserRepository _userRepository = userRepository;

        /// <summary>
        /// Выполняет вход в систему по логину и паролю.
        /// </summary>
        /// <returns>Пользователь при успешном входе или <c>null</c>, если данные неверны.</returns>
        public User? Login(string login, string password)
        {
            if (string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(password))
            {
                return null;
            }

            return _userRepository.FindByCredentials(login.Trim(), password);
        }
    }
}
