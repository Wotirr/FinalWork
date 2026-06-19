using System.ComponentModel.DataAnnotations.Schema;

namespace KnizhnyMir.DataAccess.Models
{
    /// <summary>Роль пользователя системы (администратор, менеджер, клиент).</summary>
    [Table("Role")]
    public class Role
    {
        /// <summary>Идентификатор роли.</summary>
        public int Id { get; set; }

        /// <summary>Название роли.</summary>
        public string Name { get; set; } = string.Empty;
    }
}
