using System.ComponentModel.DataAnnotations.Schema;

namespace KnizhnyMir.DataAccess.Models
{
    /// <summary>Пользователь системы. Поле <see cref="RoleName"/> заполняется соединением с ролью.</summary>
    [Table("Users")]
    public class User
    {
        /// <summary>Идентификатор пользователя.</summary>
        public int Id { get; set; }

        /// <summary>Фамилия, имя и отчество пользователя.</summary>
        public string FullName { get; set; } = string.Empty;

        /// <summary>Логин для входа в систему.</summary>
        public string Login { get; set; } = string.Empty;

        /// <summary>Пароль пользователя.</summary>
        public string Password { get; set; } = string.Empty;

        /// <summary>Идентификатор роли пользователя.</summary>
        public int RoleId { get; set; }

        /// <summary>Название роли (заполняется при выборке).</summary>
        [NotMapped]
        public string? RoleName { get; set; }
    }
}
