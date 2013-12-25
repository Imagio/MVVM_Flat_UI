using System.ComponentModel.DataAnnotations;

namespace Ru.Imagio.Model
{
    public interface IEntity
    {
        [Key]
        [Required]
        int Id { get; set; }
    }
}
