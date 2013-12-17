using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ru.Imagio.Model
{
    [Table("Accounts")]
    public class Account: ModelBase
    {
        [Required]
        [Column("UserName")]
        [Display(Name = "Имя пользователя")]
        [StringLength(20, MinimumLength = 5)]
        public string UserName { get; set; }

        [Required]
        [Column("Password")]
        [Display(Name = "Пароль")]
        [StringLength(33,MinimumLength = 33)]
        public string Password { get; set; }

        [Required]
        [Column("IsActive")]
        [Display(Name = "Активный пользователь")]
        [DefaultValue(true)]
        public bool IsActive { get; set; }

        public override int Id { get; set; }
    }
}
