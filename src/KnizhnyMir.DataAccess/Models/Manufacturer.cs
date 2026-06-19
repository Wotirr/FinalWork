using System.ComponentModel.DataAnnotations.Schema;

namespace KnizhnyMir.DataAccess.Models
{
    /// <summary>Производитель (издательство) товара.</summary>
    [Table("Manufacturer")]
    public class Manufacturer
    {
        /// <summary>Идентификатор производителя.</summary>
        public int Id { get; set; }

        /// <summary>Название производителя.</summary>
        public string Name { get; set; } = string.Empty;
    }
}
